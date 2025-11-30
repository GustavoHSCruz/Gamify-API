using Domain.Core.Entities.Quests;
using Domain.Core.Enums;
using Infrastructure.Configs.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs.Quests;

public class QuestActivityConfig : EntityConfig<QuestActivity>
{
    public override void Configure(EntityTypeBuilder<QuestActivity> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("tb_quest_activity");
        
        builder
            .Property(x => x.QuestId)
            .HasColumnName("ID_QUEST")
            .IsRequired();
        
        builder
            .Property(x => x.ScheduleStart)
            .HasColumnName("DT_SCHEDULE_START")
            .IsRequired();

        builder
            .Property(x => x.ScheduleEnd)
            .HasColumnName("DT_SCHEDULE_END");
        
        builder
            .Property(x => x.Status)
            .HasColumnName("ST_STATUS")
            .HasConversion(x => (byte)x, x=> (EActivityStatus)x)
            .IsRequired();
        
        builder
            .Property(x => x.CompletedAt)
            .HasColumnName("DT_COMPLETED_AT");
        
        builder
            .Property(x => x.Observations)
            .HasColumnName("DS_OBSERVATIONS");
        
        builder
            .Property(x => x.Experience)
            .HasColumnName("NR_EXPERIENCE")
            .IsRequired();

        builder
            .Property(x => x.Gold)
            .HasColumnName("NR_GOLD")
            .IsRequired();
    }
}