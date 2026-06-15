using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public abstract class Bill
{
    public Guid Id { get; set; }
    [StringLength(16)] public string Num { get; set; }
    public int ShiftId { get; set; }
    public BillType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime IssuedAt { get; set; }
    public Guid? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; }
    public ICollection<BillEntry> BillEntries { get; set; }

    [MaxLength(50)] public string warehouse { get; set; }
    [MaxLength(50)] public string? Ship { get; set; }
    [MaxLength(50)] public string? ShipAddress { get; set; }
    [MaxLength(12)] public string? ShipPhone1 { get; set; }
    [MaxLength(12)] public string? ShipPhone2 { get; set; }
    public decimal ItemTotal { get; set; }
    public decimal BillDiscount { get; set; }
    public Guid BillDiscountId { get; set; }
    public decimal ItemDiscount { get; set; }
    public Guid ItemDiscountId { get; set; }
    public decimal BillTax { get; set; }
    public decimal BillTaxPercentage { get; set; }
    public Guid BillTaxId { get; set; }
    public decimal Money { get; set; }
    public Guid MoneyId { get; set; }
    public decimal Total { get; set; }
    public decimal Remain { get; set; }
    public decimal TotalMoney { get; set; }
    public Guid BillExtraId { get; set; }
    public decimal TotalExtra { get; set; }
    public decimal TotalDiscount { get; set; }
    public Guid RegNum { get; set; }
    public int ItemCount { get; set; }
    public bool IsCash { get; set; }
    public bool IsPaid { get; set; }
    public DateTime? PaidDate { get; set; }
    public decimal? PaidValue { get; set; }
    public decimal? PaidDiscount { get; set; }
    public decimal? MoneyTotalPaid { get; set; }
    public Guid? PaidRegNum { get; set; }
    [MaxLength(500)] public string? InternalNote { get; set; }
    [MaxLength(500)] public string? Note { get; set; }
    public bool IsDeleted { get; set; }
}
