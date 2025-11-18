using System.Linq.Expressions;
using Domain.Shared.Entities;
using Domain.Shared.Interfaces;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class WriteRepository : IWriteRepository
    {
        private readonly DbContext _context;

        public WriteRepository(WriteContext context)
        {
            _context = context;
        }

        public async Task AddAsync<T>(T entity) where T : Entity, IAggregateRoot
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public Task AddRangeAsync<T>(IEnumerable<T> entities) where T : Entity, IAggregateRoot
        {
            return _context.Set<T>().AddRangeAsync(entities);
        }

        public IQueryable<T> AsQueryable<T>() where T : Entity, IAggregateRoot
        {
            return _context.Set<T>().AsQueryable();
        }

        public Task<bool> ExistAsync<T>(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes) where T : Entity, IAggregateRoot
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes) query = query.Include(include);

            return query.AnyAsync(predicate);
        }

        public async Task<T?>? SingleAsync<T>(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes) where T : Entity, IAggregateRoot
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