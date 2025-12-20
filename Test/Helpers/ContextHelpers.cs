using Bogus;
using Domain.Core.Entities;
using Domain.Core.Requests.Public;
using Domain.Shared.Entities;
using Domain.Shared.Interfaces;
using Test.Fakes;

namespace Test.Helpers
{
    public static class ContextHelpers
    {
        public static async Task<TEntity> RegisterAsync<TEntity>(IWriteRepository repository, IUnitOfWork uow, TEntity entity) where TEntity : Entity, IAggregateRoot
        {
            await repository.AddAsync(entity);

            await uow.SaveChangesAsync();

            return entity;
        }

        public static async Task<List<TEntity>> RegisterAsync<TEntity>(IWriteRepository repository, IUnitOfWork uow, List<TEntity> entities) where TEntity : Entity, IAggregateRoot
        {
            foreach (var entity in entities)
            {
                await repository.AddAsync((dynamic)entity);
            }

            await uow.SaveChangesAsync();

            return entities;
        }

        public static async Task<User> RegisterUserAsync(IWriteRepository repository, IUnitOfWork uow, Faker faker)
        {
            var email = faker.Person.Email;
            var password = faker.Internet.Password();
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            
            var user = new UserFake(email, hashedPassword, salt).Generate();
            
            return await RegisterAsync(repository, uow, user);
        }
    }
}
