using System;

namespace TradingBook.Infrastructure.Persistence.Entities;

public class ExchangeRateEntity
{
    public int Id { get; set; }
    public string FromCurrency { get; set; } = string.Empty;
    public string ToCurrency { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Rate { get; set; }
}