using System;

namespace AribONE.Models.Entities;

public class InventoryAdjustment : IShiftScoped
{
    public Guid Id { get; set; }

    public Guid AccountId { get; set; }

    public int Num { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid ProductId { get; set; }
    public Guid UnitId { get; set; }

    public decimal NowQty { get; set; }
    public decimal NowPrice { get; set; }

    public decimal NewQty { get; set; }
    public decimal NewPrice { get; set; }

    public decimal DiffQty { get; set; }
    public decimal DiffVal { get; set; }

    public string? Note { get; set; }

    public Guid UserId { get; set; }

    public Guid RegNum { get; set; }

    public Guid WarehouseId { get; set; }

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;

    public decimal Total { get; set; }

    public decimal UniteCost { get; set; }

    public DateTime ExprDt { get; set; }

    public decimal ItemCost { get; set; }

    /// <summary>Owning shift in Shift Mode; null in Open Safe mode.
    /// Stamped by ShiftIdInterceptor.</summary>
    public Guid? ShiftId { get; set; }
}
