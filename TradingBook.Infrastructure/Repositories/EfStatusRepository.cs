using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TradingBook.ApplicationLayer.Repositories;
using TradingBook.Infrastructure.Persistence;

namespace TradingBook.Infrastructure.Repositories
{
    public sealed class EfStatusRepository : IStatusRepository
    {
        private readonly DbContextOptions<TradingBookDbContext> _options;

        public EfStatusRepository(DbContextOptions<TradingBookDbContext> options)
        {
            _options = options;
        }

        public async Task<string> GetStatusTextAsync()
        {
            using var db = new TradingBookDbContext(_options);
            var row = await db.AppInfo.SingleOrDefaultAsync(x => x.Id == 1);

            return row?.StatusText ?? string.Empty;
        }

    }

}
