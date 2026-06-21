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
}
