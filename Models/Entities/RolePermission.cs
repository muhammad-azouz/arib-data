using System;

namespace AribONE.Models.Entities;

public class RolePermission
{
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }

    // Navigation properties
    public virtual Role Role { get; set; }
    public virtual Permission Permission { get; set; }
}
