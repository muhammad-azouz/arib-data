using System;

namespace AribONE.Models.Entities;

public class ProductBarcode
{
    public ProductBarcode()
    {
    }

    public ProductBarcode(string code)
    {
        Code = code;
    }

    public Guid Id { get; set; }
    public Guid UnitOfMeasureId { get; set; }
    public UnitOfMeasure UnitOfMeasure { get; set; }
    public string Code { get; set; }
}
