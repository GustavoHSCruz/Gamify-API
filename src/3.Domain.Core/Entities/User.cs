using Domain.Shared.Entities;

namespace Domain.Core.Entities;

public class User : Entity
{
    public User(Person person, string email, string password)
    {
        Person = person;
        Email = email;
        Password = password;
    }

    public User(Guid personId, string email, string password)
    {
        PersonId = personId;
        Email = email;
        Password = password;
    }

    public Person Person { get; set; }
    public Guid PersonId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}