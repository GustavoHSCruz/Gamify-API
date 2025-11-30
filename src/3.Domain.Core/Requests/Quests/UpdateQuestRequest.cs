using System.ComponentModel.DataAnnotations;
using Domain.Core.Enums;
using Domain.Core.Responses.Quests;
using Domain.Shared.Requests;

namespace Domain.Core.Requests.Quests;

public class UpdateQuestRequest : CommandRequest<UpdateQuestResponse>
{
    /// <summary>
    /// Quest Title
    /// </summary>
    /// <example>Go to the gym</example>
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Quest Title is required")]
    public string Title { get; set; }
    
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
    [Required(ErrorMessage = "Quest Attribute Type is required")]
    public EAttributeType AttributeType { get; set; }
    
    /// <summary>
    /// Quest Difficulty - Enum
    /// </summary>
    /// <example>Easy</example>
    [Required(ErrorMessage = "Quest Difficulty is required")]
    public EDifficulty Difficulty { get; set; }
    
    /// <summary>
    /// Quest Type - Enum
    /// </summary>
    /// <example>Habit</example>
    [Required(ErrorMessage = "Quest Type is required")]
    public EQuestType QuestType { get; set; }
    
    /// <summary>
    /// Quest Recurrence - Enum
    /// </summary>
    /// <example>Weekly</example>
    [Required(ErrorMessage = "Quest Recurrence is required")]
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
    [Required(ErrorMessage = "Quest Start Date is required")]
    [DataType(DataType.DateTime)]
    public DateTime QuestStart { get; set; }
    
    /// <summary>
    /// Quest DateTime to End - Nullable
    /// If null, the quest will never end
    /// </summary>
    [DataType(DataType.DateTime)]
    public DateTime? QuestEnd { get; set; }
    
    /// <summary>
    /// Quest Experience Points
    /// </summary>
    [Required(ErrorMessage = "Quest Experience is required")]
    [Range(0, int.MaxValue)]
    public int Experience { get; set; }
    
    /// <summary>
    /// Quest Gold
    /// </summary>
    [Required(ErrorMessage = "Quest Gold is required")]
    [Range(0, int.MaxValue)]
    public int Gold { get; set; }
}