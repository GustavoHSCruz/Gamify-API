using Bogus;
using Domain.Core.Enums;

namespace Test.Helpers.Quests;

public static class QuestHelper
{
    public static EWeekDays GetWeekDaysByRecurrence(ERecurrence recurrence, Faker faker)
    {
        return recurrence switch
        {
            ERecurrence.None => (EWeekDays)DateTime.UtcNow.DayOfWeek,
            ERecurrence.Daily => (EWeekDays)127,
            ERecurrence.Weekly => (EWeekDays)faker.Random.Int(1, 127),
            ERecurrence.Monthly => (EWeekDays)faker.Random.Int(1, 127),
            ERecurrence.BusinessDays => (EWeekDays)62,
            ERecurrence.Weekends => (EWeekDays)65,
            ERecurrence.Custom => (EWeekDays)faker.Random.Int(1, 127)
        };
    }
}