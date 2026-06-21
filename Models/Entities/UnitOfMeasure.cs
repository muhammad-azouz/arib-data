using System;
using System.Collections.Generic;

namespace AribONE.Models.Entities;

public class UnitOfMeasure
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public required string Name { get; set; }
    public decimal ValSub { get; set; }
    public bool MasterBuy { get; set; } = true;
    public bool MasterSale { get; set; } = true;
    public decimal Buy { get; set; }
    public decimal Sale { get; set; }
    public int Level { get; set; }
    public decimal Price1 { get; set; }
    public decimal Price2 { get; set; }
    public decimal Price3 { get; set; }
    public decimal Price4 { get; set; }
    public decimal Price5 { get; set; }
    public decimal Price6 { get; set; }
    public decimal Price7 { get; set; }
    public decimal Price8 { get; set; }
    public decimal Price9 { get; set; }
    public ICollection<ProductBarcode> Barcodes { get; set; } = [];
}
