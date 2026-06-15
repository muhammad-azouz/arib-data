using System;

namespace AribONE.Models.Entities;

public class ProductExpiry
{
    public int ExpireId { get; set; }
    public int ProductId { get; set; }
    public DateTime Expr { get; set; }
    public decimal Value { get; set; }
}