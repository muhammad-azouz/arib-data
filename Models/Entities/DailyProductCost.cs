using System;

namespace AribONE.Models.Entities;

public class DailyProductCost
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public Guid WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; }
    public decimal Qty { get; set; }
    public decimal Cost { get; set; }
    public decimal Price { get; set; }
    public int BatchNumber { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public DateTime ReceivedDate { get; set; }
}
