using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.IO;


namespace TradingBook.Infrastructure.Persistence
{
    public class TradingBookDbContextFactory : IDesignTimeDbContextFactory<TradingBookDbContext>
    {
        public TradingBookDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TradingBookDbContext>();
            var basePath = AppContext.BaseDirectory;
            var dbPath = Path.Combine(basePath, "TradingBook.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");

            return new TradingBookDbContext(optionsBuilder.Options);
        }
    }
}
