using System.Runtime.InteropServices;
using Domain.Core.Enums;
using Domain.Core.Requests.Quests;
using Test.Common;
using Test.Fakes.Quests;
using Test.Helpers;

namespace Test.Integration.Quests;

public class GetQuestTest : IntegrationTestCommand
{
    [Fact(DisplayName = "Get All Quests Successfully")]
    public async Task Get_All_Quests_Successfully()
    {
        var user = await ContextHelpers.RegisterUserAsync(_repository, _uow, _faker);

        var questList = new QuestFake(user.Person).Generate(10);

        await ContextHelpers.RegisterAsync(_repository, _uow, questList);

        var request = new GetQuestRequest();
        request.SetUserId(user.Id);

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.NotNull(response.Data);
        Assert.Equal(10, response.Data.Count());
        Assert.True(response.Errors?.Any() == false && string.IsNullOrEmpty(response.InternalMessage), $"Errors: {string.Join(", ", response.Errors)} - InternalMessage: {response.InternalMessage}");
    }

    [Fact(DisplayName = "Get Quests with pagination")]
    public async Task Get_Quests_With_Pagination()
    {
        var user = await ContextHelpers.RegisterUserAsync(_repository, _uow, _faker);

        var questList = new QuestFake(user.Person).Generate(30);

        await ContextHelpers.RegisterAsync(_repository, _uow, questList);

        var request = new GetQuestRequest();
        request.SetUserId(user.Id);
        request.SetPageItems(2, 5);

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.NotNull(response.Data);
        Assert.Equal(5, response.Items);
        Assert.Equal(6, response.Pages);
        Assert.True(response.Errors?.Any() == false && string.IsNullOrEmpty(response.InternalMessage), $"Errors: {string.Join(", ", response.Errors)} - InternalMessage: {response.InternalMessage}");
    }

    [Fact(DisplayName = "Get Quests with title filter")]
    public async Task Get_Quests_With_Title_Filter()
    {
        var user = await ContextHelpers.RegisterUserAsync(_repository, _uow, _faker);
        var questList = new QuestFake(user.Person).Generate(10);
        await ContextHelpers.RegisterAsync(_repository, _uow, questList);

        var title = _faker.Random.ListItem(questList.Select(q => q.Title).ToList())[..5];

        var request = new GetQuestRequest()
        {
            Title = title
        };
        request.SetUserId(user.Id);

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.NotNull(response.Data);
        Assert.NotEmpty(response.Data);
        Assert.True(response.Errors?.Any() == false && string.IsNullOrEmpty(response.InternalMessage), $"Errors: {string.Join(", ", response.Errors)} - InternalMessage: {response.InternalMessage}");
    }

    [Fact(DisplayName = "Get Quests with description filter")]
    public async Task Get_Quest_With_Description_Filer()
    {
        var user = await ContextHelpers.RegisterUserAsync(_repository, _uow, _faker);
        var questList = new QuestFake(user.Person).Generate(10);
        await ContextHelpers.RegisterAsync(_repository, _uow, questList);

        var description = _faker.Random.ListItem(questList.Select(q => q.Description).ToList())[..5];

        var request = new GetQuestRequest()
        {
            Description = description
        };
        request.SetUserId(user.Id);

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.NotNull(response.Data);
        Assert.NotEmpty(response.Data);
        Assert.True(response.Errors?.Any() == false && string.IsNullOrEmpty(response.InternalMessage), $"Errors: {string.Join(", ", response.Errors)} - InternalMessage: {response.InternalMessage}");
    }

    [Theory(DisplayName = "Get Quests With Attribute Filter")]
    [InlineData(EAttributeType.Charisma)]
    [InlineData(EAttributeType.Constitution)]
    [InlineData(EAttributeType.Intelligence)]
    [InlineData(EAttributeType.Strength)]
    public async Task Get_Quests_With_Attribute_Filter(EAttributeType attributeType)
    {
        var user = await ContextHelpers.RegisterUserAsync(_repository, _uow, _faker);
        var questList = new QuestFake(user.Person).Generate(40);
        await ContextHelpers.RegisterAsync(_repository, _uow, questList);

        //It's gonna blow some time...
        var request = new GetQuestRequest()
        {
            AttributeType = attributeType
        };

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.NotNull(response.Data);
        Assert.NotEmpty(response.Data);
        Assert.True(response.Errors?.Any() == false && string.IsNullOrEmpty(response.InternalMessage), $"Errors: {string.Join(", ", response.Errors)} - InternalMessage: {response.InternalMessage}");
    }

    [Theory(DisplayName = "Get Quests With Difficulty Filter")]
    [InlineData(EDifficulty.Easy)]
    [InlineData(EDifficulty.Medium)]
    [InlineData(EDifficulty.Hard)]
    public async Task Get_Quests_With_Difficulty_Filter(EDifficulty difficulty)
    {
        var user = await ContextHelpers.RegisterUserAsync(_repository, _uow, _faker);
        var questList = new QuestFake(user.Person).Generate(40);
        await ContextHelpers.RegisterAsync(_repository, _uow, questList);

        var request = new GetQuestRequest()
        {
            Difficulty = difficulty
        };

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.NotNull(response.Data);
        Assert.NotEmpty(response.Data);
        Assert.True(response.Errors?.Any() == false && string.IsNullOrEmpty(response.InternalMessage), $"Errors: {string.Join(", ", response.Errors)} - InternalMessage: {response.InternalMessage}");
    }

    [Theory(DisplayName = "Get Quests With Quest Type Filter")]
    [InlineData(EQuestType.Daily)]
    [InlineData(EQuestType.Habit)]
    [InlineData(EQuestType.Boss)]
    public async Task Get_Quests_With_Quest_Type_Filter(EQuestType questType)
    {
        var user = await ContextHelpers.RegisterUserAsync(_repository, _uow, _faker);
        var questList = new QuestFake(user.Person).Generate(40);
        await ContextHelpers.RegisterAsync(_repository, _uow, questList);

        var request = new GetQuestRequest()
        {
            QuestType = questType
        };

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.NotNull(response.Data);
        Assert.NotEmpty(response.Data);
        Assert.True(response.Errors?.Any() == false && string.IsNullOrEmpty(response.InternalMessage), $"Errors: {string.Join(", ", response.Errors)} - InternalMessage: {response.InternalMessage}");
    }

    [Theory(DisplayName = "Get Quests With Recurrence Filter")]
    [InlineData(ERecurrence.None)]
    [InlineData(ERecurrence.Daily)]
    [InlineData(ERecurrence.Weekly)]
    [InlineData(ERecurrence.Monthly)]
    [InlineData(ERecurrence.BusinessDays)]
    [InlineData(ERecurrence.Weekends)]
    [InlineData(ERecurrence.Custom)]
    public async Task Get_Quests_With_Recurrence_Filter(ERecurrence recurrence)
    {
        var user = await ContextHelpers.RegisterUserAsync(_repository, _uow, _faker);
        var questList = new QuestFake(user.Person).Generate(40);
        await ContextHelpers.RegisterAsync(_repository, _uow, questList);

        var request = new GetQuestRequest()
        {
            Recurrence = recurrence
        };

        var response = await _mediator.Send(request);

        Assert.NotNull(response);
        Assert.NotNull(response.Data);
        Assert.NotEmpty(response.Data);
        Assert.True(response.Errors?.Any() == false && string.IsNullOrEmpty(response.InternalMessage), $"Errors: {string.Join(", ", response.Errors)} - InternalMessage: {response.InternalMessage}");
    }
}