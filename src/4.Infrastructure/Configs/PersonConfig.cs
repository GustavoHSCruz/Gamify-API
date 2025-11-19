using Domain.Core.Entities;
using Infrastructure.Configs.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs;

public class PersonConfig : EntityConfig<Person>
{
    public override void Configure(EntityTypeBuilder<Person> builder)
    {
        base.Configure(builder);

        builder.ToTable("tb_person");

        builder
            .Property(x => x.FirstName)
            .HasColumnName("DS_FIRST_NAME")
            .IsRequired();

        builder
            .Property(x => x.LastName)
            .HasColumnName("DS_LAST_NAME")
            .IsRequired();

        builder
            .HasOne(x => x.User)
            .WithOne(x => x.Person)
            .HasForeignKey<User>(x => x.PersonId);
    }
}