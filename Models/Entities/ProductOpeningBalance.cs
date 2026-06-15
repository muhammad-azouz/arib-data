using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class ProductOpeningBalance
{
    public Guid Id { get; set; }

    public Guid FromId { get; set; }

    public Guid ToId { get; set; }

    public Guid WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public string UnitName { get; set; }

    public DateTime IssueDate { get; set; }

    public decimal Qty { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }
    public Guid RegNum { get; set; }

    [StringLength(30)] public string User { get; set; } = string.Empty;
}
