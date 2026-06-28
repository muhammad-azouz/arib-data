using System;

namespace AribONE.Models.Entities;

public class WeightedAverageCost
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public Guid WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = null!;
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
    public decimal Qty { get; set; }
    public decimal Cost { get; set; }
    public decimal Price { get; set; }
    public DateTime ReceivedDate { get; set; }
}
