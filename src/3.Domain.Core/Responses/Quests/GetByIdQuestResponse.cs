using System.ComponentModel.DataAnnotations;
using Domain.Core.Enums;
using Domain.Shared.Responses;

namespace Domain.Core.Responses.Quests;

public class GetByIdQuestResponse : GetByIdResponse
{
    public new GetByIdQuestDto? Data { get; set; }
}

public class GetByIdQuestDto
{
    /// <summary>
    /// Quest Id - Guid
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Quest Title
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Quest Description - Can be null
    /// </summary>
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
    /// Quest Week Days
    /// </summary>
    /// <example>21</example>
    public EWeekDays WeekDays { get; set; }
    
    /// <summary>
    /// Quest DateTime to Start
    /// </summary>
    /// <example>2022-01-01T00:00:00Z</example>
    [DataType(DataType.DateTime)]
    public DateTime QuestStart { get; set; }
    
    /// <summary>
    /// Quest DateTime to End - Nullable
    /// </summary>
    /// <example>2022-01-01T00:00:00Z</example>
    [DataType(DataType.DateTime)]
    public DateTime? QuestEnd { get; set; }
    
    /// <summary>
    /// Quest Active Status
    /// </summary>
    /// <example>true</example>
    public bool IsActive { get; set; }
}