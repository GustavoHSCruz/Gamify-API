using Domain.Shared.Entities;
using Domain.Shared.Interfaces;

namespace Domain.Core.Entities;

public class User : Entity, IAggregateRoot
{
    public User(Person person, string email, string password, string salt)
    {
        Person = person;
        Email = email;
        Password = password;
        Salt = salt;
    }

    public User(Guid personId, string email, string password, string salt)
    {
        PersonId = personId;
        Email = email;
        Password = password;
        Salt = salt;
    }

    public Person Person { get; set; }
    public Guid PersonId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
}