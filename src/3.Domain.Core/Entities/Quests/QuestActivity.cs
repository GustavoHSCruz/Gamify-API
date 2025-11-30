using Domain.Core.Enums;
using Domain.Shared.Entities;

namespace Domain.Core.Entities.Quests;

public class QuestActivity : Entity
{
    public QuestActivity(DateTime scheduleStart, DateTime? scheduleEnd, int experience, int gold)
    {
        ScheduleStart = scheduleStart;
        ScheduleEnd = scheduleEnd;
        Status = EActivityStatus.Scheduled;
        Experience = experience;
        Gold = gold;
    }
    
    public Quest Quest { get; set; }
    public Guid QuestId { get; set; }
    public DateTime ScheduleStart { get; set; }
    public DateTime? ScheduleEnd { get; set; }
    public EActivityStatus Status { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? Observations { get; set; }
    public int Experience { get; set; }
    public int Gold { get; set; }
}