using System;

namespace AribONE.Models.Entities;

public class InvoiceLine
{
    public Guid Id { get; set; }
    public int Num { get; set; }
    public Guid InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public Guid WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = null!;
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public Guid UnitId { get; set; }
    public UnitOfMeasure Unit { get; set; } = null!;
    public Guid? PartnerId { get; set; }
    public Partner? Partner { get; set; }
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
    // Precision lives in AribContext.OnModelCreating, not in [Precision] attributes:
    // the pre-convention decimal(18,2) money default outranks data annotations, so an
    // attribute here is silently ignored. Qty/TotalQty are 18,3 and ItemCost 18,4;
    // everything else takes the (18,2) money default.
    public decimal Qty { get; set; }
    public decimal TotalQty { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }
    public decimal Tax { get; set; }

    public Guid RegNum { get; set; }
    public bool IsPaid { get; set; }
    public decimal ItemCost { get; set; }
    public bool FromOrder { get; set; }
    public DateTime? ExpireDt { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal Discount { get; set; }
    public bool IsDelete { get; set; } = false;
}
