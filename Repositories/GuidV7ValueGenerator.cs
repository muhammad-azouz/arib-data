using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace AribONE.Repositories;

/// <summary>
/// Generates client-side GUID v7 (time-ordered) values for primary keys.
/// Used so that offline branches can assign collision-free, sortable ids at
/// <c>Add</c>-time without a round-trip to the database (Dotmim.Sync friendly).
/// </summary>
public class GuidV7ValueGenerator : ValueGenerator<Guid>
{
    /// <summary>These are real, permanent keys — not temporary placeholders.</summary>
    public override bool GeneratesTemporaryValues => false;

    public override Guid Next(EntityEntry entry) => Guid.CreateVersion7();
}
