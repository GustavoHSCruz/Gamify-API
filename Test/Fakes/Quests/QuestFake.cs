using System.Reflection.Metadata;
using Bogus;
using Domain.Core.Entities.Quests;
using Domain.Core.Enums;
using Test.Helpers.Quests;
using Person = Domain.Core.Entities.Person;

namespace Test.Fakes.Quests;

public class QuestFake : Faker<Quest>
{
    public QuestFake(Person person)
    {
        CustomInstantiator(f =>
        {
            var recurrence = f.Random.Enum<ERecurrence>();

            var weekdays = QuestHelper.GetWeekDaysByRecurrence(recurrence, f);
            
            return new(
                f.Random.Words(3),
                f.Random.Words(3),
                f.Random.Enum<EAttributeType>(),
                f.Random.Enum<EDifficulty>(),
                f.Random.Enum<EQuestType>(),
                recurrence,
                weekdays,
                f.Date.Future(0),
                f.Date.Future(1))
            {
                Person = person
            };
        });
    }
}