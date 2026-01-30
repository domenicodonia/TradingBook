using System.Collections.Generic;

namespace TradingBook.Infrastructure.Persistence.Entities;

public class InstrumentEntity
{
    public int Id { get; set; }

    // Identificatori
    public string Isin { get; set; } = string.Empty;
    public string Ticker { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    // Metadati
    public InstrumentType Type { get; set; }
    public string Currency { get; set; } = "EUR";
    public string Region { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public ReplicaType Replica { get; set; } = ReplicaType.None;
    public decimal? LeverageFactor { get; set; }
    public bool IsAccumulating { get; set; }

    // Navigazione
    public ICollection<MovementEntity> Movements { get; set; } = new List<MovementEntity>();
}