using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class Warehouse
{
    public Guid Id { get; set; }

    public int Num { get; set; }

    [Required] public string Name { get; set; }

    public bool IsActive { get; set; }

    public int ProductsCount { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public Guid BranchId { get; set; }
    public virtual Branch Branch { get; set; }

    public ICollection<Product> Products { get; set; }
    public ICollection<WarehouseProductInventory> WarehouseProductInventories { get; set; }
}
