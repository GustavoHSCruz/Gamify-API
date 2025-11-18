using Infrastructure.Configs.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public class WriteContext : DbContext
    {
        public WriteContext(DbContextOptions<WriteContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntityConfig<>).Assembly);

            //BaseContexts.Base.CreateSystemBase(modelBuilder);

            modelBuilder.Model.GetEntityTypes()
                .SelectMany(x => x.GetProperties())
                .Where(x => x.ClrType == typeof(string))
                .ToList() /*.ForEach(x => x.SetColumnType("varchar(255)"))*/;
        }

        //public DbSet<>
    }
}