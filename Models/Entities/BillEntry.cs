using System;
using Microsoft.EntityFrameworkCore;

namespace AribONE.Models.Entities;

public class BillEntry
{
    public Guid Id { get; set; }
    public int Num { get; set; }
    public Guid BillId { get; set; }
    public Bill Bill { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public Guid WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = null!;
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public Guid UnitId { get; set; }
    public UnitOfMeasure Unit { get; set; } = null!;
    public Guid? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
    [Precision(18, 3)] public decimal Qty { get; set; }
    [Precision(18, 3)] public decimal TotalQty { get; set; }
    [Precision(18, 2)] public decimal Price { get; set; }
    public decimal Total { get; set; }
    public decimal Tax { get; set; }

    public Guid RegNum { get; set; }
    public bool IsPaid { get; set; }
    [Precision(18, 4)] public decimal ItemCost { get; set; }
    public bool FromOrder { get; set; }
    public DateTime? ExpireDt { get; set; }
    [Precision(4, 2)] public decimal DiscountPercentage { get; set; }
    [Precision(4, 2)] public decimal Discount { get; set; }
    public bool IsDelete { get; set; } = false;
}
