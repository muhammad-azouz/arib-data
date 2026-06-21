using System;

namespace AribONE.Models.Entities;

public class SafeTransform
{
    public int Id { get; set; }
    public int FromId { get; set; }
    public int ToId { get; set; }

    public required string FromName { get; set; }
    public required string ToName { get; set; }
    public decimal Value { get; set; }

    public required string Note { get; set; }
    public DateTime Dt { get; set; }
    public int Num { get; set; }
    public required string MyUser { get; set; }
    public int RegNum { get; set; }
    public decimal Total { get; set; }
    public required string Ship { get; set; }
}