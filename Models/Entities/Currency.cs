using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class Currency
{
    public Guid Id { get; set; }
    [Required] [StringLength(5)] public required string Code { get; set; }
    [StringLength(20)] public required string ArabicName { get; set; }
    [StringLength(20)] public required string EnglishName { get; set; }

    public decimal Value { get; set; }
    public bool IsDefault { get; set; }
}
