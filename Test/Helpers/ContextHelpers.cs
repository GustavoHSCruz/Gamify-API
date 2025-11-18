using Domain.Shared.Entities;
using Domain.Shared.Interfaces;

namespace Test.Helpers
{
    public static class ContextHelpers
    {
        public static async Task<TEntity> Register<TEntity>(IWriteRepository repository, IUnitOfWork uow, TEntity entity) where TEntity : Entity, IAggregateRoot
        {
            await repository.AddAsync(entity);

            await uow.SaveChangesAsync();

            return entity;
        }

        public static async Task<IAggregateRoot[]> Register(IWriteRepository repository, IUnitOfWork uow, params IAggregateRoot[] entities)
        {
            foreach (var entity in entities)
            {
                await repository.AddAsync((dynamic)entity);
            }

            await uow.SaveChangesAsync();

            return entities;
        }
    }
}
