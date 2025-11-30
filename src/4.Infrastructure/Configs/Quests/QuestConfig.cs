using Domain.Core.Entities.Quests;
using Domain.Core.Enums;
using Infrastructure.Configs.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs.Quests;

public class QuestConfig : EntityConfig<Quest>
{
    public override void Configure(EntityTypeBuilder<Quest> builder)
    {
        base.Configure(builder);

        builder.ToTable("tb_quest");

        builder
            .Property(x => x.Title)
            .HasColumnName("DS_TITLE")
            .IsRequired();

        builder
            .Property(x => x.Description)
            .HasColumnName("DS_DESCRIPTION");

        builder
            .Property(x => x.AttributeType)
            .HasColumnName("ST_ATTRIBUTE_TYPE")
            .HasConversion(x => (byte)x, x => (EAttributeType)x)
            .IsRequired();

        builder
            .Property(x => x.Difficulty)
            .HasColumnName("ST_DIFFICULTY")
            .HasConversion(x => (byte)x, x => (EDifficulty)x)
            .IsRequired();
        
        builder
            .Property(x => x.QuestType)
            .HasColumnName("ST_QUEST_TYPE")
            .HasConversion(x => (byte)x, x => (EQuestType)x)
            .IsRequired();

        builder
            .Property(x => x.Recurrence)
            .HasColumnName("ST_RECURRENCE")
            .HasConversion(x => (byte)x, x => (ERecurrence)x)
            .IsRequired();

        builder
            .Property(x => x.WeekDays)
            .HasColumnName("ST_WEEK_DAYS")
            .HasConversion(x => (byte)x, x => (EWeekDays)x)
            .IsRequired();
        
        builder
            .Property(X => X.QuestStart)
            .HasColumnName("DT_QUEST_START")
            .IsRequired();

        builder
            .Property(x => x.QuestEnd)
            .HasColumnName("DT_QUEST_END");

        builder
            .Property(x => x.PersonId)
            .HasColumnName("ID_PERSON")
            .IsRequired();
    }
}