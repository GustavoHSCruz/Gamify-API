using Domain.Core.Entities.Quests;
using Domain.Core.Requests.Quests;

namespace Domain.Core.Mapper;

public class MappedEntities : AutoMapper.Profile
{
    public MappedEntities()
    {
        CreateMap<UpdateQuestRequest, Quest>();
    }
}