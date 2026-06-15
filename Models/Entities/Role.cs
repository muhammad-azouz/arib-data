using System;
using System.Collections.Generic;

namespace AribONE.Models.Entities;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;

    // Navigation properties
    public virtual ICollection<RolePermission> RolePermissions { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
}
