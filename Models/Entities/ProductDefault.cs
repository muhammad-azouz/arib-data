using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class ProductDefault
{
    [Key] public Guid Id { get; set; }

    public ProductKind ProductKind { get; set; }

    [StringLength(50)] public string Unit { get; set; } = "قطعة";

    public decimal Buy { get; set; }

    public decimal Sale { get; set; }

    public double Order { get; set; }

    public double ReOrder { get; set; }

    public double RecessionPeriod { get; set; }

    public double ExpirationDate { get; set; }

    public Guid StIdSale { get; set; }

    public Guid StIdSaleCost { get; set; }

    public Guid StIdStock { get; set; }

    public Guid SrAccount { get; set; }

    public Guid SrIdSaleCost { get; set; }

    public Guid SrIdSale { get; set; }
}
