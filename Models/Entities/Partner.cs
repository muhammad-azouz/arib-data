using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class Partner
{
    public Guid Id { get; set; }
    public int Num { get; set; }
    public PartnerType Type { get; set; }
    [MaxLength(100)] [MinLength(3)] public required string Name { get; set; }

    public Guid? ImageId { get; set; }
    public Image? Image { get; set; }
    public Guid? GroupId { get; set; }
    public virtual PartnerGroup? Group { get; set; }
    public DateTime? CreatedAt { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public decimal Balance { get; set; }
    [MaxLength(12)] public required string Phone1 { get; set; }
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

    /// <summary>"This customer always pays this for delivery" — the top layer of D4's three-layer
    /// resolution (tasks/spec-delivery-couriers.md), beating the zone tariff and the branch default.
    /// <b>Null is the only miss</b>: a stored <c>0</c> means free delivery for this customer and
    /// deliberately wins over a priced zone. Money — global decimal(18,2) convention, no override.
    /// Not master-gated (D5): it rides the customer record, which branches already edit.</summary>
    public decimal? DeliveryFee { get; set; }
    [MaxLength(200)] public string? Address { get; set; }
}
