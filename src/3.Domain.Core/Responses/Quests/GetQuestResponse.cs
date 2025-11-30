using System.ComponentModel.DataAnnotations;
using Domain.Core.Enums;
using Domain.Shared.Responses;

namespace Domain.Core.Responses.Quests;

public class GetQuestResponse : GetResponse
{
    public new IEnumerable<GetQuestDto>? Data { get; set; }
}

public class GetQuestDto
{
    /// <summary>
    /// Quest Id - Guid
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Quest Title
    /// </summary>
    [DataType(DataType.Text)]
    public string Title { get; set; }
    
    /// <summary>
    /// Quest Description - Can be null
    /// </summary>
    [DataType(DataType.MultilineText)]
    public string? Description { get; set; }
    
    /// <summary>
    /// Quest Attribute - Enum
    /// </summary>
    /// <example>Strength</example>
    public EAttributeType AttributeType { get; set; }
    
    /// <summary>
    /// Quest Difficulty - Enum
    /// </summary>
    /// <example>Easy</example>
    public EDifficulty Difficulty { get; set; }
    
    /// <summary>
    /// Quest Type - Enum
    /// </summary>
    /// <example>Habit</example>
    public EQuestType QuestType { get; set; }
    
    /// <summary>
    /// Quest Recurrence - Enum
    /// </summary>
    /// <example>Weekly</example>
    public ERecurrence Recurrence { get; set; }
    
    /// <summary>
    /// Quest Active Status
    /// </summary>
    /// <example>true</example>
    public bool IsActive { get; set; }
}