using System;
using System.Collections.Generic;

namespace TradingBook.Infrastructure.Persistence.Entities;

public class PortfolioEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Strategy { get; set; } = string.Empty;
    public string BaseCurrency { get; set; } = "EUR";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    // Navigazione
    public ICollection<MovementEntity> Movements { get; set; } = new List<MovementEntity>();
}