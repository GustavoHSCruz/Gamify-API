using Application.Common;
using Application.Services;
using Domain.Core.Entities.Quests;
using Domain.Core.Requests.Quests;
using Domain.Core.Responses.Quests;
using Domain.Shared.Interfaces;

namespace Application.Queries.Quests;

public class GetByIdQuestQuery : Query<GetByIdQuestRequest, GetByIdQuestResponse>
{
    public GetByIdQuestQuery(IReadRepository repository) : base(repository)
    {
    }

    protected override Task<GetByIdQuestResponse> GetData(GetByIdQuestRequest request)
    {
        var predicate = PredicateBuilder.New<Quest>();

        predicate = predicate.And(x => x.Id == request.GetId() && !x.IsDeleted);

        var result = _repository.AsQueryable<Quest>()
            .Where(predicate)
            .Select(x => new GetByIdQuestDto()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                AttributeType = x.AttributeType,
                Difficulty = x.Difficulty,
                QuestType = x.QuestType,
                Recurrence = x.Recurrence,
                WeekDays = x.WeekDays,
                QuestStart = x.QuestStart,
                QuestEnd = x.QuestEnd,
                IsActive = x.IsActive
            }).FirstOrDefault();

        return Task.FromResult(new GetByIdQuestResponse { Data = result });
    }
}