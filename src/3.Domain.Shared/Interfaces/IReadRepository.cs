using Domain.Shared.Entities;
using System.Linq.Expressions;

namespace Domain.Shared.Interfaces
{
    public interface IReadRepository
    {
        IQueryable<T> AsQueryable<T>() where T : Entity, IAggregateRoot;

        Task<bool> ExistAsync<T>(Expression<Func<T, bool>> predicate) where T : Entity, IAggregateRoot;

        Task<T?>? SingleAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : Entity, IAggregateRoot;
    }
}
