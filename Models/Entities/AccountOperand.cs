using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AribONE.Models.Entities;

[PrimaryKey(nameof(Operand))]
public class AccountOperand
{
    [MaxLength(30)] public string Operand { get; set; } = string.Empty;
    public Guid AccountId { get; set; }
    [MaxLength(100)] public string LabelAr { get; set; } = string.Empty;
    [MaxLength(100)] public string LabelEn { get; set; } = string.Empty;
}
