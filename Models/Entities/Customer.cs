using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class Customer
{
    public Guid Id { get; set; }
    public int Num { get; set; }
    public CustomerType Type { get; set; }
    [MaxLength(100)] [MinLength(3)] public string Name { get; set; }

    public Guid? ImageId { get; set; }
    public Image? Image { get; set; }
    public Guid? GroupId { get; set; }
    public virtual CustomerGroup? Group { get; set; }
    public DateTime? CreatedAt { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public decimal Balance { get; set; }
    [MaxLength(12)] public string Phone1 { get; set; }
    [MaxLength(12)] public string? Phone2 { get; set; }
    [MaxLength(12)] public string? Phone3 { get; set; }
    [MaxLength(50)] public string? Mail { get; set; }
    [MaxLength(50)] public string? Company { get; set; }
    [MaxLength(50)] public string? WebSite { get; set; }
    public bool IsActive { get; set; }
    public bool IsDoubleType { get; set; }
    public int? PriceTier { get; set; }

    [MaxLength(100)] public string? Note { get; set; }
    public Guid AccountId { get; set; }
    [MaxLength(50)] public string? BankNum { get; set; }
    [MaxLength(50)] public string? BankName { get; set; }
    [MaxLength(50)] public string? BankBrunch { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal OpenBalance { get; set; }
    public bool IsCredit { get; set; }
    public Guid? RegNum { get; set; }
    public Guid FromId { get; set; }
    public Guid BranchId { get; set; }
    public Guid? AreaId { get; set; }
    public virtual Area? Area { get; set; }
    [MaxLength(200)] public string? Address { get; set; }
}
