using System;

namespace TradingBook.Infrastructure.Persistence.Entities;

public class MovementEntity
{
    public int Id { get; set; }

    public DateTime OperationDate { get; set; }
    public OperationType OperationType { get; set; }

    // quantità e prezzi
    public decimal Quantity { get; set; }
    public decimal Price { get; set; } // price per unit in operation currency
    public string Currency { get; set; } = "EUR";
    public decimal ExchangeRate { get; set; } = 1m; // rate to portfolio base currency
    public decimal Commission { get; set; } = 0m;

    // riferimenti
    public int? InstrumentId { get; set; }
    public InstrumentEntity? Instrument { get; set; }

    public int PortfolioId { get; set; }
    public PortfolioEntity Portfolio { get; set; } = null!;

    public string BrokerAccount { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}