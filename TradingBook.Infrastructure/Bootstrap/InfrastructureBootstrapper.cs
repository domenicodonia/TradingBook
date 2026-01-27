using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TradingBook.Infrastructure.Persistence;


namespace TradingBook.Infrastructure.Bootstrap;

public static class InfrastructureBootstrapper
{
    public static IDbContextFactory<TradingBookDbContext> CreateDbContextFactory(string dbPath)
    {
        var options = new DbContextOptionsBuilder<TradingBookDbContext>()
            .UseSqlite($"Data Source={dbPath}")
            .Options;

        return new PooledDbContextFactory<TradingBookDbContext>(options);
    }

    public static void Migrate(IDbContextFactory<TradingBookDbContext> dbContextFactory)
    {
        using var db = dbContextFactory.CreateDbContext();
        db.Database.Migrate();
    }
}
