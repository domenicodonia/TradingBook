namespace TradingBook.Infrastructure.Persistence.Entities;

public enum InstrumentType
{
    ETF,
    Stock,
    Bond,
    Cash
}

public enum ReplicaType
{
    Physical,
    Synthetic,
    None
}

public enum OperationType
{
    Buy,
    Sell,
    Dividend,
    Coupon,
    Commission,
    CurrencyExchange
}