using System.Threading.Tasks;
using TradingBook.ApplicationLayer.Repositories;

namespace TradingBook.ApplicationLayer.UseCases
{
    public sealed class GetStatusTextUseCase
    {
        private readonly IStatusRepository _statusRepository;

        public GetStatusTextUseCase(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public Task<string> ExecuteAsync()
        {
            return _statusRepository.GetStatusTextAsync();
        }

    }
}
