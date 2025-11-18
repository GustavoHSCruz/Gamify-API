using Domain.Shared.Entities;
using Domain.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WriteContext _context;

        public UnitOfWork(WriteContext context) => _context = context;

        public void Dispose() => _context.Dispose();

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            TouchTimestamps();
            return await _context.SaveChangesAsync(cancellationToken);
        }

        private void TouchTimestamps()
        {
            var now = DateTime.UtcNow;

            foreach (var e in _context.ChangeTracker.Entries<Entity>())
            {
                if (e.State == EntityState.Added)
                {
                    e.Property(x => x.CreatedAt).CurrentValue = now;
                    e.Property(x => x.UpdatedAt).CurrentValue = now;
                }
                else if (e.State == EntityState.Modified)
                {
                    if (e.Property(x => x.IsDeleted).IsModified && e.Property(x => x.IsDeleted).CurrentValue)
                    {
                        e.Property(x => x.DeletedAt).CurrentValue = now;
                    }

                    if (e.Property(x => x.IsDeleted).IsModified && !e.Property(x => x.IsDeleted).CurrentValue)
                    {
                        e.Property(x => x.DeletedAt).CurrentValue = null;
                    }

                    e.Property(x => x.UpdatedAt).CurrentValue = now;
                    e.Property(x => x.CreatedAt).IsModified = false; // não mexe no CreatedAt
                }
            }
        }
    }
}
