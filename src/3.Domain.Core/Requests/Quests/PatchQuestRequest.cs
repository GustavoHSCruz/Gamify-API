using Domain.Core.Responses.Quests;
using Domain.Shared.Requests;
using Domain.Shared.ValueObjects;

namespace Domain.Core.Requests.Quests;

public class PatchQuestRequest : CommandRequest<PatchQuestResponse>
{
    public List<PatchModel> Patch { get; set; }
}