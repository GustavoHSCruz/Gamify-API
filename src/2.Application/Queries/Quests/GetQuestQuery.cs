using Application.Common;
using Application.Services;
using Domain.Core.Entities.Quests;
using Domain.Core.Requests.Quests;
using Domain.Core.Responses.Quests;
using Domain.Shared.Interfaces;

namespace Application.Queries.Quests;

public class GetQuestQuery : Query<GetQuestRequest, GetQuestResponse>
{
    public GetQuestQuery(IReadRepository repository) : base(repository)
    {
    }

    protected override Task<GetQuestResponse> GetData(GetQuestRequest request)
    {
        var predicate = PredicateBuilder.New<Quest>();

        predicate = predicate.And(x => !x.IsDeleted);

        //TODO: Unify Title and Description
        if (!string.IsNullOrEmpty(request.Title)) predicate = predicate.And(x => x.Title.Contains(request.Title));
        if (!string.IsNullOrEmpty(request.Description)) predicate = predicate.And(x => x.Description.Contains(request.Description));
        if (!request.AttributeType.IsNullOrDontExist()) predicate = predicate.And(x => x.AttributeType == request.AttributeType);
        if (!request.Difficulty.IsNullOrDontExist()) predicate = predicate.And(x => x.Difficulty == request.Difficulty);
        if (!request.QuestType.IsNullOrDontExist()) predicate = predicate.And(x => x.QuestType == request.QuestType);
        if (!request.Recurrence.IsNullOrDontExist()) predicate = predicate.And(x => x.Recurrence == request.Recurrence);

        var query = _repository.AsQueryable<Quest>()
            .Where(predicate)
            .Select(x => new GetQuestDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = string.IsNullOrEmpty(x.Description) ? null : x.Description.Substring(0, 25),
                AttributeType = x.AttributeType,
                Difficulty = x.Difficulty,
                QuestType = x.QuestType,
                Recurrence = x.Recurrence,
                IsActive = x.IsActive,
            });

        var queryCount = query.Count();

        var result = query.Skip(Skip).Take(Take).ToList();

        return Task.FromResult(new GetQuestResponse { Data = result, Pages = request.GetTotalPages(queryCount), Items = request.GetItems() });
    }
}