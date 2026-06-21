using System;
using System.Diagnostics.CodeAnalysis;

namespace AribONE.Models.Entities;

public class ProductBarcode
{
    public ProductBarcode()
    {
    }

    [SetsRequiredMembers]
    public ProductBarcode(string code)
    {
        Code = code;
    }

    public Guid Id { get; set; }
    public Guid UnitOfMeasureId { get; set; }
    public UnitOfMeasure UnitOfMeasure { get; set; } = null!;
    public required string Code { get; set; }
}
