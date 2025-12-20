using Bogus;
using Domain.Core.Entities;
using Person = Domain.Core.Entities.Person;

namespace Test.Fakes;

public class UserFake : Faker<User>
{
    public UserFake(string email, string password, string salt, Person? person = null)
    {
        person ??= new PersonFake().Generate();

        CustomInstantiator(f => new(person, email, password, salt));
    }
}