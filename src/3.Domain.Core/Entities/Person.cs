
using System.Text.Json.Serialization;
using Domain.Core.Entities.Quests;
using Domain.Shared.Entities;

namespace Domain.Core.Entities;

public class Person : Entity
{
    public Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
    
    private List<Quest> _quests = new();
    
    [JsonIgnore]
    public User User { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public IReadOnlyCollection<Quest> Quests => _quests;
    
    public void AddQuest(Quest quest) => _quests.Add(quest);
    public void RemoveQuest(Quest quest) => quest.SetDeleted();
}