using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class Company
{
    public Guid Id { get; set; }

    [StringLength(100)] [Required] public string Name { get; set; }
    [StringLength(100)] public string? Field { get; set; }
    [StringLength(12)] public string? Phone1 { get; set; }
    [StringLength(12)] public string? Phone2 { get; set; }
    [StringLength(30)] public string? Phone3 { get; set; }
    [StringLength(150)] public string? Street { get; set; }
    [StringLength(50)] public string? City { get; set; }
    [StringLength(50)] public string? Country { get; set; }
    [StringLength(50)] public string? EMail { get; set; }
    [StringLength(50)] public int Num { get; set; }

    public Guid? LogoId { get; set; }
    public Image? Logo { get; set; }

    [StringLength(50)] public string? TaxCard { get; set; }
    [StringLength(50)] public string? CommercialRegister { get; set; }

    public Guid CurrencyId { get; set; }
    [StringLength(50)] public Currency Currency { get; set; }

    public ICollection<Branch>? Branches { get; set; }
}
