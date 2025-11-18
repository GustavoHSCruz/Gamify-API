using Domain.Shared.Entities;
using Domain.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs.Base
{
    public class EntityConfig<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            ConfigureKey(builder);

            builder
                .Property(x => x.Id)
                .HasColumnName("ID")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("DT_CREATED")
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("DT_UPDATED")
                .IsRequired();

            builder
                .Property(x => x.DeletedAt)
                .HasColumnName("DT_DELETED");

            builder
                .Property(x => x.IsActive)
                .HasColumnName("IS_ACTIVE")
                .IsRequired();

            builder
                .Property(x => x.IsDeleted)
                .HasColumnName("IS_DELETED")
                .IsRequired();
        }

        private void ConfigureKey(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
        }
        
        protected string GetEnumComments<TEnum>() where TEnum : Enum
        {
            var values = Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(x => $"{x} = {Convert.ToByte(x)}");

            return string.Join(", ", values);
        }
    }
}
