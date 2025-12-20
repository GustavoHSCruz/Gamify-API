using System.Diagnostics;
using Domain.Core.Entities;
using Domain.Core.Entities.Quests;
using Domain.Core.Enums;
using Domain.Core.Requests.Quests;
using Test.Common;
using Test.Fakes;
using Test.Fakes.Quests;
using Test.Helpers;
using Test.Helpers.Quests;

namespace Test.Integration.Quests;

public class CreateQuestTest : IntegrationTestCommand
{
    [Fact(DisplayName = "Create Quest Successfully")]
    public async Task Create_Quest_Successfully()
    {
        var user = await ContextHelpers.RegisterUserAsync(_repository, _uow, _faker);

        var recurrence = _faker.Random.Enum<ERecurrence>();

        var weekdays = QuestHelper.GetWeekDaysByRecurrence(recurrence, _faker);

        var request = new CreateQuestRequest
        {
            Title = _faker.Random.Words(3),
            Description = _faker.Lorem.Paragraphs(2),
            AttributeType = _faker.Random.Enum<EAttributeType>(),
            Difficulty = _faker.Random.Enum<EDifficulty>(),
            QuestType = _faker.Random.Enum<EQuestType>(),
            Recurrence = recurrence,
            WeekDays = weekdays,
            QuestStart = DateTime.UtcNow.AddDays(_faker.Random.Int(1, 30)),
            QuestEnd = _faker.Random.Bool() ? DateTime.UtcNow.AddDays(_faker.Random.Int(30, 120)) : null,
            Experience = _faker.Random.Int(1, 100),
            Gold = _faker.Random.Int(1, 100)
        };
        request.SetUserId(user.Id);

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.Equal(1, _writeContext.Set<Quest>().Count());
        Assert.True(response.Errors?.Any() == false && string.IsNullOrEmpty(response.InternalMessage), $"Errors: {string.Join(", ", response.Errors)} - InternalMessage: {response.InternalMessage}");
    }

    [Fact(DisplayName = "Create Quest with same title")]
    public async Task Create_Quest_With_Same_Title()
    {
        var user = await ContextHelpers.RegisterUserAsync(_repository, _uow, _faker);

        var quest = new QuestFake(user.Person).Generate();
        ContextHelpers.RegisterAsync(_repository,_uow, quest);
        
        var recurrence = _faker.Random.Enum<ERecurrence>();

        var weekdays = QuestHelper.GetWeekDaysByRecurrence(recurrence, _faker);

        var request = new CreateQuestRequest
        {
            Title = quest.Title,
            Description = _faker.Lorem.Paragraphs(2),
            AttributeType = _faker.Random.Enum<EAttributeType>(),
            Difficulty = _faker.Random.Enum<EDifficulty>(),
            QuestType = _faker.Random.Enum<EQuestType>(),
            Recurrence = recurrence,
            WeekDays = weekdays,
            QuestStart = DateTime.UtcNow.AddDays(_faker.Random.Int(1, 30)),
            QuestEnd = _faker.Random.Bool() ? DateTime.UtcNow.AddDays(_faker.Random.Int(30, 120)) : null,
            Experience = _faker.Random.Int(1, 100),
            Gold = _faker.Random.Int(1, 100)
        };
        request.SetUserId(user.Id);
        
        var response = await _mediator.Send(request);
        
        Assert.NotNull(response);
        Assert.Equal(2, _writeContext.Set<Quest>().Count());
        Assert.True(response.Errors?.Any() == false && string.IsNullOrEmpty(response.InternalMessage), $"Errors: {string.Join(", ", response.Errors)} - InternalMessage: {response.InternalMessage}");
    }

    [Fact(DisplayName = "Create Quest with database populated")]
    public async Task Create_Quest_With_Database_Populated()
    {
        var otherUser = await ContextHelpers.RegisterUserAsync(_repository, _uow, _faker);
        var otherQuest = new QuestFake(otherUser.Person).Generate();
        await ContextHelpers.RegisterAsync(_repository,_uow, otherQuest);
        
        var user = await ContextHelpers.RegisterUserAsync(_repository, _uow, _faker);
        
        var recurrence = _faker.Random.Enum<ERecurrence>();

        var weekdays = QuestHelper.GetWeekDaysByRecurrence(recurrence, _faker);

        var request = new CreateQuestRequest
        {
            Title = _faker.Random.Words(3),
            Description = _faker.Lorem.Paragraphs(2),
            AttributeType = _faker.Random.Enum<EAttributeType>(),
            Difficulty = _faker.Random.Enum<EDifficulty>(),
            QuestType = _faker.Random.Enum<EQuestType>(),
            Recurrence = recurrence,
            WeekDays = weekdays,
            QuestStart = DateTime.UtcNow.AddDays(_faker.Random.Int(1, 30)),
            QuestEnd = _faker.Random.Bool() ? DateTime.UtcNow.AddDays(_faker.Random.Int(30, 120)) : null,
            Experience = _faker.Random.Int(1, 100),
            Gold = _faker.Random.Int(1, 100)
        };
        request.SetUserId(user.Id);
        
        var response = await _mediator.Send(request);
        
        Assert.NotNull(response);
        Assert.Equal(2, _writeContext.Set<Quest>().Count());
        Assert.True(response.Errors?.Any() == false && string.IsNullOrEmpty(response.InternalMessage), $"Errors: {string.Join(", ", response.Errors)} - InternalMessage: {response.InternalMessage}");
    }

    [Fact(DisplayName = "Create Quest with no data")]
    public async Task Create_Quest_With_Invalid_Data()
    {
        var user = await ContextHelpers.RegisterUserAsync(_repository, _uow, _faker);

        var request = new CreateQuestRequest();
        request.SetUserId(user.Id);

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.Equal(0, _writeContext.Set<Quest>().Count());
        Assert.True(response.Errors?.Any() == true, "Errors: " + string.Join(", ", response.Errors));
        Assert.True(string.IsNullOrEmpty(response.InternalMessage), $"InternalMessage must be empty, but found: {response.InternalMessage}");
    }

    [Fact(DisplayName = "Create Quest with invalid weekday")]
    public async Task Create_Quest_With_Invalid_WeekDay()
    {
        var user = await ContextHelpers.RegisterUserAsync(_repository, _uow, _faker);

        var request = new CreateQuestRequest
        {
            Title = _faker.Random.Words(3),
            Description = _faker.Lorem.Paragraphs(2),
            AttributeType = _faker.Random.Enum<EAttributeType>(),
            Difficulty = _faker.Random.Enum<EDifficulty>(),
            QuestType = _faker.Random.Enum<EQuestType>(),
            Recurrence = ERecurrence.None,
            WeekDays = (EWeekDays)21,
            QuestStart = DateTime.UtcNow.AddDays(_faker.Random.Int(1, 30)),
            QuestEnd = _faker.Random.Bool() ? DateTime.UtcNow.AddDays(_faker.Random.Int(30, 120)) : null,
            Experience = _faker.Random.Int(1, 100),
            Gold = _faker.Random.Int(1, 100)
        };
        request.SetUserId(user.Id);

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.Equal(0, _writeContext.Set<Quest>().Count());
        Assert.True(response.Errors?.Any() == true, "Errors: " + string.Join(", ", response.Errors));
        Assert.True(string.IsNullOrEmpty(response.InternalMessage), $"InternalMessage must be empty, but found: {response.InternalMessage}");
    }
}