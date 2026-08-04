using System;

namespace AribONE.Models.Entities;

/// <summary>One product/qty row on an <see cref="Order"/> (tasks/spec-order-management.md).
/// <see cref="Price"/> is snapshotted at order time — a quote, not a fact (D6).</summary>
public class OrderLine
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;

    /// <summary>Denormalized for its own sync filter, as <see cref="StockTransferLine"/>
    /// does — deliberately no FK, just an indexed scalar.</summary>
    public Guid BranchId { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public Guid UnitId { get; set; }
    public UnitOfMeasure Unit { get; set; } = null!;

    public decimal Qty { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }
}
