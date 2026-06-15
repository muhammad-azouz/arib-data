using System;

namespace AribONE.Repositories;

/// <summary>
/// Deterministic GUID generation for built-in (seeded) rows.
///
/// Every seeded row's GUID is derived from its legacy integer id plus a
/// per-table code, so that:
///   * a fresh install (EF <c>HasData</c>) and
///   * an in-place SQL conversion of an existing customer database
/// produce the <b>same</b> GUIDs for the same built-in rows.
///
/// The SQL conversion script (Inkdrop note Sueh5zOP) MUST reproduce this exact
/// formula. Keep the two in lockstep — changing the format here silently breaks
/// alignment with already-migrated databases.
///
/// Format: <c>{tableCode:D8}-0000-7000-a000-{oldId:D12}</c>
/// (decimal digits are valid hex; the <c>(tableCode, oldId)</c> pair is unique
/// across all tables, so generated GUIDs never collide). Legacy id <c>0</c>
/// (root parents, "none" sentinels) maps to <see cref="Guid.Empty"/>.
/// </summary>
public static class SeedIds
{
    /// <summary>
    /// Per-table codes used as the GUID prefix. These values are part of the
    /// cross-system contract with the SQL conversion script — never reuse or
    /// renumber an existing code.
    /// </summary>
    public static class TableCodes
    {
        public const int Account = 1;
        public const int Role = 2;
        public const int Permission = 3;
        public const int RolePermission = 4;
        public const int User = 5;
        public const int UserRole = 6;
        public const int ProductDefault = 7;
        public const int Currency = 8;
        public const int Branch = 9;
        public const int Company = 10;
    }

    /// <summary>
    /// Maps a legacy (tableCode, integer id) pair to its deterministic GUID.
    /// Legacy id <c>0</c> → <see cref="Guid.Empty"/>.
    /// </summary>
    public static Guid SeedGuid(int tableCode, int oldId)
        => oldId == 0
            ? Guid.Empty
            : new Guid($"{tableCode:D8}-0000-7000-a000-{oldId:D12}");

    /// <summary>Convenience: deterministic GUID for a built-in Account row.</summary>
    public static Guid Account(int oldId) => SeedGuid(TableCodes.Account, oldId);
}
