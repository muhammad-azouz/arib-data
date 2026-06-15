using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class Currency
{
    public Guid Id { get; set; }
    [Required] [StringLength(5)] public string Code { get; set; }
    [StringLength(20)] public string ArabicName { get; set; }
    [StringLength(20)] public string EnglishName { get; set; }

    public decimal Value { get; set; }
    public bool IsDefault { get; set; }
}
