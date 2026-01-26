using Microsoft.EntityFrameworkCore;
using TradingBook.Infrastructure.Persistence.Entities;


namespace TradingBook.Infrastructure.Persistence
{
    public class TradingBookDbContext : DbContext
    {
        public TradingBookDbContext(DbContextOptions<TradingBookDbContext> options)
            : base(options)
        {
        }
        public DbSet<AppInfoEntity> AppInfo => Set<AppInfoEntity>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppInfoEntity>().ToTable("AppInfo");
            modelBuilder.Entity<AppInfoEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<AppInfoEntity>().Property(x => x.StatusText).IsRequired();
            modelBuilder.Entity<AppInfoEntity>().HasData(new AppInfoEntity
            {
                Id = 1,
                StatusText = "Application started."
            });

            base.OnModelCreating(modelBuilder);


        }
    }
}
