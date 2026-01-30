using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NUnit.Framework;
using TradingBook.Infrastructure.Persistence;
using TradingBook.Infrastructure.Repositories;

namespace TradingBook.IntegrationTests
{
    public class EfStatusRepositoryIntegrationTests
    {
        [Test]
        public async Task GetStatusTextAsync_ReturnsSeededValue()
        {
            // connessione in-memory che resta aperta per la durata del test
            await using var connection = new SqliteConnection("Data Source=:memory:");
            await connection.OpenAsync();

            var options = new DbContextOptionsBuilder<TradingBookDbContext>()
                .UseSqlite(connection)
                .Options;

            var factory = new PooledDbContextFactory<TradingBookDbContext>(options);

            // crea schema e dati seed (usa EnsureCreated per test rapidi)
            await using (var db = factory.CreateDbContext())
            {
                await db.Database.EnsureCreatedAsync();
            }

            var repo = new EfStatusRepository(factory);
            var text = await repo.GetStatusTextAsync();

            Assert.AreEqual("Application started.", text);
        }
    }
}