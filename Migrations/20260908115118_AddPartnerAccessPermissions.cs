using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class AddPartnerAccessPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("00000003-0000-7000-a000-000000000058"), "يمكنه رؤية العملاء في قوائم اختيار العملاء والموردين", "التعامل مع العملاء" },
                    { new Guid("00000003-0000-7000-a000-000000000059"), "يمكنه رؤية الموردين في قوائم اختيار العملاء والموردين", "التعامل مع الموردين" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("00000004-0000-7000-a000-000000000086"), new Guid("00000003-0000-7000-a000-000000000058"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000087"), new Guid("00000003-0000-7000-a000-000000000059"), new Guid("00000002-0000-7000-a000-000000000001") }
                });

            // Every other role — seeded (Manager/Cashier) and any custom role a tenant
            // created itself — must also get both permissions on upgrade, so nobody's
            // pickers silently narrow to nothing (spec's opt-in-by-default requirement).
            // HasData above only reaches Administrator; it can't reach rows this migration
            // doesn't know exist.
            //
            // The new RolePermission.Id must be deterministic and byte-identical whether
            // this backfill runs on SQL Server or on Postgres (the matching migration in
            // AribONE.Data.Migrations.Postgres) for the same (RoleId, PermissionId) pair.
            // RolePermissions is a Tier A table under ServerWins sync — an id minted
            // independently and randomly on each engine invents a distinct row for the
            // same logical grant, and they multiply centrally instead of converging. This
            // is exactly the FiscalYear duplicate-key collision from 2026-07-28
            // (tasks/spec-fiscal-year-sync-fix.md) — do not use NEWID() here.
            //
            // Id = MD5(RoleId text || PermissionId text), reformatted as a GUID. The input
            // is pure ASCII (hex digits + hyphens), so the varchar-vs-UTF8 encoding
            // difference between the two engines can't change the hash. SQL Server's
            // CAST(uniqueidentifier AS CHAR(36)) and Postgres's uuid::text disagree on
            // hex-letter casing, so both the input and the hash output are lower-cased
            // before use — see the Postgres migration for the mirrored query.
            migrationBuilder.Sql("""
                INSERT INTO RolePermissions (Id, RoleId, PermissionId)
                SELECT
                    CAST(
                        SUBSTRING(hashed.Hex, 1, 8) + '-' +
                        SUBSTRING(hashed.Hex, 9, 4) + '-' +
                        SUBSTRING(hashed.Hex, 13, 4) + '-' +
                        SUBSTRING(hashed.Hex, 17, 4) + '-' +
                        SUBSTRING(hashed.Hex, 21, 12)
                    AS uniqueidentifier),
                    r.Id,
                    p.Id
                FROM Roles r
                CROSS JOIN Permissions p
                CROSS APPLY (
                    SELECT LOWER(CONVERT(CHAR(32), HASHBYTES('MD5',
                        LOWER(CAST(r.Id AS CHAR(36))) + LOWER(CAST(p.Id AS CHAR(36)))
                    ), 2)) AS Hex
                ) AS hashed
                WHERE p.Id IN ('00000003-0000-7000-a000-000000000058', '00000003-0000-7000-a000-000000000059')
                  AND NOT EXISTS (
                      SELECT 1 FROM RolePermissions rp
                      WHERE rp.RoleId = r.Id AND rp.PermissionId = p.Id
                  );
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Undo the backfill first (before the Permission rows it references are
            // deleted below). Only deletes rows whose Id still matches the deterministic
            // formula recomputed from that row's own (RoleId, PermissionId) — a role
            // whose permission was later toggled off/on by hand through the UI gets a
            // fresh GuidV7 id there and is correctly left alone.
            migrationBuilder.Sql("""
                DELETE rp
                FROM RolePermissions rp
                CROSS APPLY (
                    SELECT LOWER(CONVERT(CHAR(32), HASHBYTES('MD5',
                        LOWER(CAST(rp.RoleId AS CHAR(36))) + LOWER(CAST(rp.PermissionId AS CHAR(36)))
                    ), 2)) AS Hex
                ) AS hashed
                WHERE rp.PermissionId IN ('00000003-0000-7000-a000-000000000058', '00000003-0000-7000-a000-000000000059')
                  AND rp.Id = CAST(
                      SUBSTRING(hashed.Hex, 1, 8) + '-' +
                      SUBSTRING(hashed.Hex, 9, 4) + '-' +
                      SUBSTRING(hashed.Hex, 13, 4) + '-' +
                      SUBSTRING(hashed.Hex, 17, 4) + '-' +
                      SUBSTRING(hashed.Hex, 21, 12)
                  AS uniqueidentifier);
                """);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-7000-a000-000000000086"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-7000-a000-000000000087"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-7000-a000-000000000058"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-7000-a000-000000000059"));
        }
    }
}
