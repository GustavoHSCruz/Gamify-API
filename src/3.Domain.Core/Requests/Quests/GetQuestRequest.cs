using System.ComponentModel.DataAnnotations;
using Domain.Core.Enums;
using Domain.Core.Responses.Quests;
using Domain.Shared.Requests;

namespace Domain.Core.Requests.Quests;

public class GetQuestRequest : QueryRequest<GetQuestResponse>
{
    /// <summary>
    /// Quest Title
    /// </summary>
    /// <example>Go to the gym</example>
    [DataType(DataType.Text)]
    public string? Title { get; set; }

    /// <summary>
    /// Quest Description
    /// </summary>
    /// <example> Make 5 pushups</example>
    [DataType(DataType.MultilineText)]
    public string? Description { get; set; }

    /// <summary>
    /// Quest Attribute - Enum
    /// </summary>
    /// <example>Strength</example>
    public EAttributeType? AttributeType { get; set; }

    /// <summary>
    /// Quest Difficulty - Enum
    /// </summary>
    /// <example>Easy</example>
    public EDifficulty? Difficulty { get; set; }

    /// <summary>
    /// Quest Type - Enum
    /// </summary>
    /// <example>Habit</example>
    public EQuestType? QuestType { get; set; }

    /// <summary>
    /// Quest Recurrence - Enum
    /// </summary>
    /// <example>Weekly</example>
    public ERecurrence? Recurrence { get; set; }
}