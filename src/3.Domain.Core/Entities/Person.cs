using Domain.Shared.Entities;

namespace Domain.Core.Entities;

public class Person : Entity
{
    public Person(string  firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
    
    public User User { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}