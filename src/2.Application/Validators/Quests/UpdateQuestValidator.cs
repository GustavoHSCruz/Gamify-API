using Domain.Core.Enums;
using Domain.Core.Requests.Quests;
using Domain.Shared.Enums;
using FluentValidation;

namespace Application.Validators.Quests;

public class UpdateQuestValidator : AbstractValidator<UpdateQuestRequest>
{
    public UpdateQuestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.AttributeType).IsInEnum().NotEmpty();
        RuleFor(x => x.Difficulty).IsInEnum().NotEmpty();
        RuleFor(x => x.QuestType).IsInEnum().NotEmpty();
        RuleFor(x => x.Recurrence).IsInEnum().NotNull();
        RuleFor(x => x.WeekDays).IsInEnum().NotNull();
        RuleFor(x => x.QuestStart).NotEmpty().GreaterThanOrEqualTo(DateTime.UtcNow);
        RuleFor(x => x.Experience).NotNull();
        RuleFor(x => x.Gold).NotNull();

        When(x => x.Recurrence == ERecurrence.None, () =>
        {
            RuleFor(x => x.WeekDays).Must(wd => wd != 0 && ((int)wd & ((int)wd - 1)) == 0).WithMessage(EErrorCode.OnlyOneWeekDay.ToString());
        });

        When(x => x.Recurrence == ERecurrence.BusinessDays, () =>
        {
            RuleFor(x => x.WeekDays).Must(x => x == (EWeekDays)62).WithMessage(EErrorCode.MustBeBusinessDays.ToString());
        });

        When(x => x.Recurrence == ERecurrence.Weekends, () =>
        {
            RuleFor(x => x.WeekDays).Must(x => x == (EWeekDays)65).WithMessage(EErrorCode.MustBeWeekend.ToString());
        });
        
        When(x => x.QuestEnd != null, () =>
        {
            RuleFor(x => x.QuestEnd).GreaterThanOrEqualTo(x => x.QuestStart);
        });
    }
}