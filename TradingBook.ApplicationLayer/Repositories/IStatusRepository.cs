using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBook.ApplicationLayer.Repositories;

public interface IStatusRepository
{
    Task<string> GetStatusTextAsync();
}
