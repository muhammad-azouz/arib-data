using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AribONE.Models.Entities;

[PrimaryKey(nameof(Role))]
public class PostingAccount
{
    [MaxLength(30)] public string Role { get; set; } = string.Empty;
    public Guid AccountId { get; set; }
    [MaxLength(100)] public string LabelAr { get; set; } = string.Empty;
    [MaxLength(100)] public string LabelEn { get; set; } = string.Empty;
}
