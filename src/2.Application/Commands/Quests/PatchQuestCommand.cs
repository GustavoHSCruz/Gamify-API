using Application.Common;
using Application.Services;
using AutoMapper;
using Domain.Core.Entities.Quests;
using Domain.Core.Events.Quests;
using Domain.Core.Requests.Quests;
using Domain.Core.Responses.Quests;
using Domain.Shared.Enums;
using Domain.Shared.Interfaces;
using MediatR;

namespace Application.Commands.Quests;

public class PatchQuestCommand : Command<Quest, PatchQuestRequest, PatchQuestResponse, QuestPatchedEvent>
{
    private Quest _quest;

    public PatchQuestCommand(IMediator mediator, IWriteRepository repository, IUnitOfWork uow, IMapper mapper) : base(mediator, repository, uow, mapper)
    {
    }

    protected override async Task BeforeChanges(PatchQuestRequest request)
    {
        _quest = await _repository.SingleAsync<Quest>(x => x.Id == request.GetId() && !x.IsDeleted);

        if (_quest == null)
        {
            _response.AddError(EErrorCode.QuestNotFound);

            return;
        }
    }

    protected override Task<Quest> Changes(PatchQuestRequest request)
    {
        PatcherService.ApplyPatch(_quest, request.Patch);
        
        return Task.FromResult(_quest);
    }
}