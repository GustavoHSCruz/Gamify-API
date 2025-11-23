using Domain.Core.Entities;
using Infrastructure.Configs.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs;

public class UserConfig : EntityConfig<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("tb_user");
        
        builder
            .Property(x => x.PersonId)
            .HasColumnName("PERSON_ID")
            .IsRequired();
        
        builder
            .Property(x => x.Email)
            .HasColumnName("DS_EMAIL")
            .IsRequired();
        
        builder
            .Property(x => x.Password)
            .HasColumnName("DS_PASSWORD")
            .IsRequired();
        
        builder
            .Property(x => x.Salt)
            .HasColumnName("DS_SALT")
            .IsRequired();
        
        builder
            .Property(x => x.RefreshToken)
            .HasColumnName("DS_REFRESH_TOKEN");
    }
}