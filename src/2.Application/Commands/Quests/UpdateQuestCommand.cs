using Application.Common;
using Application.Services;
using AutoMapper;
using Domain.Core.Entities.Quests;
using Domain.Core.Enums;
using Domain.Core.Events.Quests;
using Domain.Core.Requests.Quests;
using Domain.Core.Responses.Quests;
using Domain.Shared.Enums;
using Domain.Shared.Interfaces;
using MediatR;

namespace Application.Commands.Quests;

public class UpdateQuestCommand : Command<Quest, UpdateQuestRequest, UpdateQuestResponse, QuestUpdatedEvent>
{
    private Quest _quest;

    public UpdateQuestCommand(IMediator mediator, IWriteRepository repository, IUnitOfWork uow, IMapper mapper) : base(mediator, repository, uow, mapper)
    {
    }

    protected override async Task BeforeChanges(UpdateQuestRequest request)
    {
        _quest = await _repository.SingleAsync<Quest>(x => x.Id == request.GetId() && !x.IsDeleted,
            x => x.QuestActivities.Where(x => x.ScheduleStart >= DateTime.UtcNow && x.IsActive && !x.IsDeleted));

        if (_quest == null)
        {
            _response.AddError(EErrorCode.QuestNotFound);

            return;
        }
    }

    protected override Task<Quest> Changes(UpdateQuestRequest request)
    {
        TimeSpan difference = new(0, 0, 0, 0, 0, 0);

        if (_quest.Recurrence != request.Recurrence || request.QuestEnd != _quest.QuestEnd || request.WeekDays != _quest.WeekDays)
        {
            _quest.QuestActivities.ToList().ForEach(x => x.SetDeleted());

            if (request.QuestEnd != null)
            {
                difference = (DateTime)request.QuestEnd - request.QuestStart;
            }

            IEnumerable<DateTime> dates = request.Recurrence switch
            {
                ERecurrence.None => new[] { request.QuestStart },
                _ => DateService.GetAllDaysByWeekDay(request.WeekDays, request.QuestStart, request.QuestEnd)
            };

            foreach (var day in dates)
            {
                _quest.AddQuestActivity(new QuestActivity(day, day.Add(difference), request.Experience, request.Gold));
            }
        }

        _mapper.Map(request, _quest);

        return Task.FromResult(_quest);
    }
}