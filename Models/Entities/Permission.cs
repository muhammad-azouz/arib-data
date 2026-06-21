using System;
using System.Collections.Generic;

namespace AribONE.Models.Entities;

public class Permission
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }

    // Navigation properties
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = null!;
}
