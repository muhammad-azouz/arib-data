using System;

namespace AribONE.Models.Entities;

public class WarehouseProductInventory
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public Guid WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; }
    public decimal TotalQty { get; set; }
    public decimal TotalCost { get; set; }
    public decimal UnitCost { get; set; }

    public decimal LastInPrice { get; set; }
    public decimal LastInQty { get; set; }
    public DateTime? LastInDate { get; set; }
    public decimal LastOutPrice { get; set; }
    public decimal LastOutQty { get; set; }
    public DateTime? LastOutDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
