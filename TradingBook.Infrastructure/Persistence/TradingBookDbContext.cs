using Microsoft.EntityFrameworkCore;
using TradingBook.Infrastructure.Persistence.Entities;

namespace TradingBook.Infrastructure.Persistence;

public class TradingBookDbContext : DbContext
{
    public TradingBookDbContext(DbContextOptions<TradingBookDbContext> options)
        : base(options)
    {
    }

    public DbSet<AppInfoEntity> AppInfo => Set<AppInfoEntity>();
    public DbSet<InstrumentEntity> Instruments => Set<InstrumentEntity>();
    public DbSet<PortfolioEntity> Portfolios => Set<PortfolioEntity>();
    public DbSet<MovementEntity> Movements => Set<MovementEntity>();
    public DbSet<ExchangeRateEntity> ExchangeRates => Set<ExchangeRateEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // AppInfo (esistente)
        modelBuilder.Entity<AppInfoEntity>().ToTable("AppInfo");
        modelBuilder.Entity<AppInfoEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<AppInfoEntity>().Property(x => x.StatusText).IsRequired();
        modelBuilder.Entity<AppInfoEntity>().HasData(new AppInfoEntity
        {
            Id = 1,
            StatusText = "Application started."
        });

        // Instruments
        modelBuilder.Entity<InstrumentEntity>(b =>
        {
            b.ToTable("Instruments");
            b.HasKey(x => x.Id);
            b.Property(x => x.Isin).HasMaxLength(32).IsRequired();
            b.Property(x => x.Ticker).HasMaxLength(64);
            b.Property(x => x.Name).HasMaxLength(256).IsRequired();
            b.Property(x => x.Currency).HasMaxLength(8).IsRequired();
            b.HasIndex(x => x.Isin).IsUnique();
            b.HasIndex(x => x.Ticker);
        });

        // Portfolios
        modelBuilder.Entity<PortfolioEntity>(b =>
        {
            b.ToTable("Portfolios");
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).HasMaxLength(200).IsRequired();
            b.Property(x => x.BaseCurrency).HasMaxLength(8).IsRequired();
            b.Property(x => x.CreatedAt).IsRequired();
            b.HasIndex(x => x.Name);
        });

        // Movements
        modelBuilder.Entity<MovementEntity>(b =>
        {
            b.ToTable("Movements");
            b.HasKey(x => x.Id);
            b.Property(x => x.OperationDate).IsRequired();
            b.Property(x => x.OperationType).IsRequired();
            b.Property(x => x.Quantity).HasPrecision(18, 6).IsRequired();
            b.Property(x => x.Price).HasPrecision(18, 6).IsRequired();
            b.Property(x => x.ExchangeRate).HasPrecision(18, 8);
            b.Property(x => x.Commission).HasPrecision(18, 6);

            b.HasOne(x => x.Instrument)
                .WithMany(i => i.Movements)
                .HasForeignKey(x => x.InstrumentId)
                .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(x => x.Portfolio)
                .WithMany(p => p.Movements)
                .HasForeignKey(x => x.PortfolioId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ExchangeRates
        modelBuilder.Entity<ExchangeRateEntity>(b =>
        {
            b.ToTable("ExchangeRates");
            b.HasKey(x => x.Id);
            b.Property(x => x.FromCurrency).HasMaxLength(8).IsRequired();
            b.Property(x => x.ToCurrency).HasMaxLength(8).IsRequired();
            b.Property(x => x.Date).IsRequired();
            b.Property(x => x.Rate).HasPrecision(18, 8).IsRequired();
            b.HasIndex(x => new { x.FromCurrency, x.ToCurrency, x.Date });
        });

        base.OnModelCreating(modelBuilder);
    }
}
