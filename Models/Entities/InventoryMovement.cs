using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class InventoryMovement
{
    public Guid Id { get; set; }
    public DateTime IssueDate { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public Guid? PartnerId { get; set; }
    public Partner? Partner { get; set; }

    public Guid WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = null!;

    public Dealing Dealing { get; set; }

    public DateTime? ExpirationDate { get; set; }
    public decimal InQty { get; set; }
    public decimal InPrice { get; set; }
    public decimal InTotal { get; set; }

    public decimal OutQty { get; set; }
    public decimal OutPrice { get; set; }
    public decimal OutTotal { get; set; }

    public decimal Cost { get; set; }
    [MaxLength(20)] public string Unit { get; set; } = string.Empty;

    public bool IsDeleted { get; set; }
    public Guid BranchId { get; set; }
    public Guid RegNum { get; set; }
    public Guid UserId { get; set; }
    [MaxLength(30)] public required string Pc { get; set; }
}
