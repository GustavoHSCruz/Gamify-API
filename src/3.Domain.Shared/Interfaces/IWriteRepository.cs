using System.Linq.Expressions;
using Domain.Shared.Entities;

namespace Domain.Shared.Interfaces
{
    public interface IWriteRepository
    {
        IQueryable<T> AsQueryable<T>() where T : Entity, IAggregateRoot;

        Task<bool> ExistAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : Entity, IAggregateRoot;

        Task<T> SingleAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : Entity, IAggregateRoot;

        Task AddAsync<T>(T entity) where T : Entity, IAggregateRoot;

        Task AddRangeAsync<T>(IEnumerable<T> entities) where T : Entity, IAggregateRoot;
    }
}