using System;
using System.Collections.Generic;

namespace AribONE.Models.Entities;

public class Product
{
    public Product()
    {
        UnitOfMeasure = new List<UnitOfMeasure>();
        WarehouseProductInventories = new List<WarehouseProductInventory>();
    }

    public Guid Id { get; set; }

    public ProductKind ProductKind { get; set; }

    public required string Name { get; set; }
    public Guid? ImageId { get; set; }

    public Guid? GroupId { get; set; }
    public ProductGroup? Group { get; set; }

    public ICollection<Warehouse> Warehouses { get; set; } = null!;
    public ICollection<WarehouseProductInventory> WarehouseProductInventories { get; set; }


    public string Vendor { get; set; } = string.Empty;
    public string Customer { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsActive { get; set; }

    public double ReOrder { get; set; }
    public double MaxOrder { get; set; }
    public double TargetSales { get; set; }

    public bool IsExpire { get; set; }
    public Guid SalesAccountId { get; set; }
    public Guid StockAccountId { get; set; }
    public Guid SalesCostAccountId { get; set; }

    public ICollection<UnitOfMeasure> UnitOfMeasure { get; set; }

    // public ProductStock Stock { get; set; }

    // public ICollection<WarehouseProductInventory> WarehouseProductInventories { get; set; }

    public InventoryEvaluateMethod InventoryValuationMethod { get; set; }
}
