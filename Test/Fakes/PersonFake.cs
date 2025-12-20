using Bogus;
using Person = Domain.Core.Entities.Person;

namespace Test.Fakes;

public class PersonFake : Faker<Person>
{
    public PersonFake()
    {
        CustomInstantiator(f => new(f.Person.FirstName, f.Person.LastName));
    }
}