using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TradingBook.ApplicationLayer.Repositories;
using TradingBook.Infrastructure.Persistence;

namespace TradingBook.Infrastructure.Repositories;

public sealed class EfStatusRepository : IStatusRepository
{
    private readonly IDbContextFactory<TradingBookDbContext> _dbContextFactory;

    public EfStatusRepository(IDbContextFactory<TradingBookDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<string> GetStatusTextAsync()
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();
        var row = await db.AppInfo.SingleOrDefaultAsync(x => x.Id == 1);

        return row?.StatusText ?? string.Empty;
    }

}
