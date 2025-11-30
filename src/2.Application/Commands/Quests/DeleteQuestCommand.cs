using Application.Common;
using AutoMapper;
using Domain.Core.Entities;
using Domain.Core.Events.Quests;
using Domain.Core.Requests.Quests;
using Domain.Core.Responses.Quests;
using Domain.Shared.Enums;
using Domain.Shared.Interfaces;
using MediatR;

namespace Application.Commands.Quests;

public class DeleteQuestCommand : Command<User, DeleteQuestRequest, DeleteQuestResponse, QuestDeletedEvent>
{
    private User _user;

    public DeleteQuestCommand(IMediator mediator, IWriteRepository repository, IUnitOfWork uow, IMapper mapper) : base(mediator, repository, uow, mapper)
    {
    }

    protected override async Task BeforeChanges(DeleteQuestRequest request)
    {
        _user = await _repository.SingleAsync<User>(x => x.Id == request.GetUserId() && x.Person.Quests.Any(x => x.Id == request.GetId() && !x.IsDeleted) && x.IsActive && !x.IsDeleted,
            x => x.Person.Quests.Where(x => x.Id == request.GetId() && !x.IsDeleted));

        if (_user == null || _user.Person.Quests.Any())
        {
            _response.AddError(EErrorCode.QuestNotFound);

            return;
        }
    }

    protected override Task<User> Changes(DeleteQuestRequest request)
    {
        _user.Person.Quests.FirstOrDefault()!.SetDeleted();
        
        return Task.FromResult(_user);
    }
}