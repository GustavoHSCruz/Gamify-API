using Domain.Core.Entities.Quests;
using Domain.Core.Enums;
using Domain.Core.Requests.Quests;
using Test.Common;
using Test.Fakes.Quests;
using Test.Helpers;
using Test.Helpers.Quests;

namespace Test.Integration.Quests;

public class UpdateQuestTest : IntegrationTestCommand
{
    [Fact(DisplayName = "Update Quest Successfully")]
    public async Task Update_Quest_Successfully()
    {
        var user = await ContextHelpers.RegisterUserAsync(_repository, _uow, _faker);

        var quest = new QuestFake(user.Person).Generate();
        await ContextHelpers.RegisterAsync(_repository, _uow, quest);

        var recurrence = _faker.Random.Enum<ERecurrence>();
        var weekdays = QuestHelper.GetWeekDaysByRecurrence(recurrence, _faker);
        var request = new UpdateQuestRequest
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
        request.SetId(quest.Id);

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.Equal(quest.Id, response.Id);
        Assert.Equal(1, _writeContext.Set<Quest>().Count());
        Assert.True(response.Errors?.Any() == false && string.IsNullOrEmpty(response.InternalMessage), $"Errors: {string.Join(", ", response.Errors)} - InternalMessage: {response.InternalMessage}");
    }

    [Fact(DisplayName = "Update Quest with same Title as other Quest")]
    public async Task Update_Quest_With_Same_Title()
    {
        var user = await ContextHelpers.RegisterUserAsync(_repository, _uow, _faker);
        var quest = new QuestFake(user.Person).Generate();
        await ContextHelpers.RegisterAsync(_repository, _uow, quest);

        var recurrence = _faker.Random.Enum<ERecurrence>();
        var weekdays = QuestHelper.GetWeekDaysByRecurrence(recurrence, _faker);

        var request = new UpdateQuestRequest
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
        request.SetId(quest.Id);

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.Equal(quest.Id, response.Id);
        Assert.Equal(1, _writeContext.Set<Quest>().Count());
        Assert.True(response.Errors?.Any() == false && string.IsNullOrEmpty(response.InternalMessage), $"Errors: {string.Join(", ", response.Errors)} - InternalMessage: {response.InternalMessage}");
    }

    [Fact(DisplayName = "Update Quest with database already populated")]
    public async Task Update_Quest_With_Database_Populated()
    {
        var user = await ContextHelpers.RegisterUserAsync(_repository, _uow, _faker);
        var quests = new QuestFake(user.Person).Generate(3);
        await ContextHelpers.RegisterAsync(_repository, _uow, quests);

        var recurrence = _faker.Random.Enum<ERecurrence>();
        var weekdays = QuestHelper.GetWeekDaysByRecurrence(recurrence, _faker);

        var request = new UpdateQuestRequest
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
        var pickedQuestId = _faker.PickRandom(quests).Id;
        request.SetId(pickedQuestId);

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.Equal(3, _writeContext.Set<Quest>().Count());
        Assert.Equal(pickedQuestId, response.Id);
        Assert.True(response.Errors?.Any() == false && string.IsNullOrEmpty(response.InternalMessage), $"Errors: {string.Join(", ", response.Errors)} - InternalMessage: {response.InternalMessage}");
    }

    [Fact(DisplayName = "Update Quest with invalid Id")]
    public async Task Update_Quest_With_Invalid_Id()
    {
        var user = await ContextHelpers.RegisterUserAsync(_repository, _uow, _faker);
        var quest = new QuestFake(user.Person).Generate();
        await ContextHelpers.RegisterAsync(_repository, _uow, quest);

        var recurrence = _faker.Random.Enum<ERecurrence>();
        var weekdays = QuestHelper.GetWeekDaysByRecurrence(recurrence, _faker);

        var request = new UpdateQuestRequest
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
        request.SetId(Guid.NewGuid());

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.Equal(1, _writeContext.Set<Quest>().Count());
        Assert.True(response.Errors?.Any(), "Errors: " + string.Join(", ", response.Errors));
        Assert.True(string.IsNullOrEmpty(response.InternalMessage), $"InternalMessage must be empty, but found: {response.InternalMessage}");
    }

    [Fact(DisplayName = "Update Quest with no data")]
    public async Task Update_Quest_With_Invalid_Data()
    {
        var user = await ContextHelpers.RegisterUserAsync(_repository, _uow, _faker);
        var quest = new QuestFake(user.Person).Generate();
        await ContextHelpers.RegisterAsync(_repository, _uow, quest);

        var request = new UpdateQuestRequest();
        
        var response = await _mediator.Send(request);
        
        Assert.NotNull(response);
        Assert.Equal(1, _writeContext.Set<Quest>().Count());
        Assert.True(response.Errors?.Any() == true, "Errors: " + string.Join(", ", response.Errors));
        Assert.True(string.IsNullOrEmpty(response.InternalMessage), $"InternalMessage must be empty, but found: {response.InternalMessage}");
    }
}