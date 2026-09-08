using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class User
{
    public Guid Id { get; set; }
    [MaxLength(50)] public required string LoginName { get; set; }
    public required string PasswordHash { get; set; }
    [MaxLength(50)] public required string Name { get; set; }
    public bool IsActive { get; set; }
    public Guid BranchId { get; set; }
    public Guid CompanyId { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; } = null!;

    /// <summary>AribLink terminal PIN (tasks/spec-ariblink-gateway.md D10) — <c>{salt}.{hash}</c>,
    /// PBKDF2-SHA256, 100 000 iterations. Null means no PIN: the user cannot sign in on a terminal.
    /// Never the POS password (<see cref="PasswordHash"/> is unsalted SHA-256 and stays that way) —
    /// a touchscreen PIN needs its own, stronger primitive.</summary>
    [MaxLength(200)] public string? PinHash { get; set; }

    /// <summary>Consecutive PIN failures; 5 ⇒ <see cref="PinLockedUntil"/> is set (D10).</summary>
    public int PinFailedCount { get; set; }

    /// <summary>Set on the 5th consecutive failure, 15 minutes out; cleared on a successful login
    /// or by an admin from the users screen (T7).</summary>
    public DateTime? PinLockedUntil { get; set; }
}
