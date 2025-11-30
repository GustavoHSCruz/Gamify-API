using System.Security.Cryptography.Xml;
using Application.Common;
using Application.Services;
using AutoMapper;
using Domain.Core.Entities;
using Domain.Core.Entities.Quests;
using Domain.Core.Enums;
using Domain.Core.Events.Quests;
using Domain.Core.Requests.Quests;
using Domain.Core.Responses.Quests;
using Domain.Shared.Enums;
using Domain.Shared.Interfaces;
using MediatR;

namespace Application.Commands.Quests;

public class CreateQuestCommand : Command<User, CreateQuestRequest, CreateQuestResponse, QuestCreatedEvent>
{
    private User _user;

    public CreateQuestCommand(IMediator mediator, IWriteRepository repository, IUnitOfWork uow, IMapper mapper) : base(mediator, repository, uow, mapper)
    {
    }

    protected override async Task BeforeChanges(CreateQuestRequest request)
    {
        _user = await _repository.SingleAsync<User>(x => x.Id == request.GetUserId() && x.IsActive && !x.IsDeleted, x=> x.Person);

        if (_user == null)
        {
            _response.AddError(EErrorCode.UserNotFound);

            return;
        }

        var anyQuestAtDateRange = await _repository.ExistAsync<Quest>(x => x.QuestActivities.Any(x => (x.ScheduleStart >= request.QuestStart && x.ScheduleStart <= request.QuestEnd) || (x.ScheduleEnd >= request.QuestStart && x.ScheduleEnd <= request.QuestEnd)),
            x => x.QuestActivities.Where(x => (x.ScheduleStart >= request.QuestStart && x.ScheduleStart <= request.QuestEnd) || (x.ScheduleEnd >= request.QuestStart && x.ScheduleEnd <= request.QuestEnd)));

        if (anyQuestAtDateRange)
        {
            _response.AddError(EErrorCode.QuestActivitieAlreadyScheduled);
            
            return;
        }
    }

    protected override async Task<User> Changes(CreateQuestRequest request)
    {
        var quest = new Quest(request.Title, request.Description, request.AttributeType, request.Difficulty, request.QuestType,request.Recurrence, request.WeekDays, request.QuestStart, request.QuestEnd);
        
        TimeSpan difference = new(0,0,0,0,0,0);

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
            quest.AddQuestActivity(new QuestActivity(day, day.Add(difference), request.Experience, request.Gold));
        }
        
        _user.Person.AddQuest(quest);

        return await Task.FromResult(_user);
    }
}