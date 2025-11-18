using Domain.Shared.Entities;

namespace Domain.Core.Entities;

public class Person : Entity
{
    public User User { get; set; }
    public string FullName { get; set; }
}