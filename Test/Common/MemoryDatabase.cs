using Infrastructure.Contexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Test.Common
{
    public abstract class MemoryDatabase : IAsyncLifetime
    {
        private SqliteConnection _conn = default!;
        protected DbContextOptions<WriteContext> _writeOptions { get; private set; } = default!;
        protected DbContextOptions<ReadContext> _readOptions { get; private set; } = default!;
        protected WriteContext _writeContext { get; private set; } = default!;
        protected ReadContext _readContext { get; private set; } = default!;

        public virtual async Task InitializeAsync()
        {
            SQLitePCL.Batteries.Init();

            _conn = new SqliteConnection("DataSource=:memory:");
            await _conn.OpenAsync();

            _writeOptions = new DbContextOptionsBuilder<WriteContext>().UseSqlite(_conn).Options;
            _readOptions = new DbContextOptionsBuilder<ReadContext>().UseSqlite(_conn).Options;

            _writeContext = new WriteContext(_writeOptions);
            _readContext = new ReadContext(_readOptions);

            await _writeContext.Database.EnsureCreatedAsync();
            await _readContext.Database.EnsureCreatedAsync();

            await SeedAsync();
        }

        public virtual async Task DisposeAsync()
        {
            await _writeContext.DisposeAsync();
            await _readContext.DisposeAsync();
            await _conn.DisposeAsync();
        }

        protected virtual Task SeedAsync() => Task.CompletedTask;
    }
}
