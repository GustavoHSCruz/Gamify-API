namespace Domain.Core.Enums;

public enum EActivityStatus : byte
{
    Scheduled = 1,
    Completed = 2,
    Reschedule = 3,
    Failed = 4,
    Canceled = 5
}