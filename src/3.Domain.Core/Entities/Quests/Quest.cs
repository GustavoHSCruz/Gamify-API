using Domain.Core.Enums;
using Domain.Shared.Entities;
using Domain.Shared.Interfaces;

namespace Domain.Core.Entities.Quests;

public class Quest : Entity, IAggregateRoot
{
    public Quest(string title, string? description, EAttributeType attributeType, EDifficulty difficulty, EQuestType questType, ERecurrence recurrence, EWeekDays weekDays, DateTime questStart, DateTime? questEnd)
    {
        Title = title;
        Description = description;
        AttributeType = attributeType;
        Difficulty = difficulty;
        QuestType = questType;
        Recurrence = recurrence;
        WeekDays = weekDays;
        QuestStart = questStart;
        QuestEnd = questEnd;
    }
    
    private List<QuestActivity> _questActivities = new();

    public string Title { get; set; }
    public string? Description { get; set; }
    public EAttributeType AttributeType { get; set; }
    public EDifficulty Difficulty { get; set; }
    public EQuestType QuestType { get; set; }
    public ERecurrence Recurrence { get; set; }
    public EWeekDays WeekDays { get; set; }
    public DateTime QuestStart { get; set; }
    public DateTime? QuestEnd { get; set; }
    public Person Person { get; set; }
    public Guid PersonId { get; set; }

    public IReadOnlyList<QuestActivity> QuestActivities => _questActivities;

    public void AddQuestActivity(QuestActivity questActivity) => _questActivities.Add(questActivity);

    public void RemoveQuestActivity(QuestActivity questActivity) => questActivity.SetDeleted();
}