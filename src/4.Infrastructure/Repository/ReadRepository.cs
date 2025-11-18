using Domain.Shared.Entities;
using Domain.Shared.Interfaces;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public class ReadRepository : IReadRepository
    {
        private readonly DbContext _context;

        public ReadRepository(ReadContext context)
        {
            _context = context;
        }

        public IQueryable<T> AsQueryable<T>() where T : Entity, IAggregateRoot
        {
            return _context.Set<T>().AsQueryable();
        }

        public Task<bool> ExistAsync<T>(Expression<Func<T, bool>> predicate) where T : Entity, IAggregateRoot
        {
            return _context.Set<T>().AnyAsync(predicate);
        }

        public async Task<T?>? SingleAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : Entity, IAggregateRoot
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.SingleOrDefaultAsync(predicate);
        }
    }
}
