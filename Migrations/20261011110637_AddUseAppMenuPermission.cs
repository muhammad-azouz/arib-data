using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class AddUseAppMenuPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("00000003-0000-7000-a000-000000000061"), "يمكنه رؤية قائمة التطبيق في أعلى الشاشة (الإعدادات، قاعدة البيانات، بيانات الشركة والفرع، الاستيراد، حول)", "استخدام قائمة التطبيق" });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[] { new Guid("00000004-0000-7000-a000-000000000089"), new Guid("00000003-0000-7000-a000-000000000061"), new Guid("00000002-0000-7000-a000-000000000001") });

            // Backfill every other role (Manager/Cashier and custom) so upgrading hides the menu
            // from nobody. Id = MD5(RoleId || PermissionId) as a GUID, byte-identical to the
            // Postgres migration — same rationale as AddPartnerAccessPermissions (no NEWID()).
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
                WHERE p.Id = '00000003-0000-7000-a000-000000000061'
                  AND NOT EXISTS (
                      SELECT 1 FROM RolePermissions rp
                      WHERE rp.RoleId = r.Id AND rp.PermissionId = p.Id
                  );
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                DELETE rp
                FROM RolePermissions rp
                CROSS APPLY (
                    SELECT LOWER(CONVERT(CHAR(32), HASHBYTES('MD5',
                        LOWER(CAST(rp.RoleId AS CHAR(36))) + LOWER(CAST(rp.PermissionId AS CHAR(36)))
                    ), 2)) AS Hex
                ) AS hashed
                WHERE rp.PermissionId = '00000003-0000-7000-a000-000000000061'
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
                keyValue: new Guid("00000004-0000-7000-a000-000000000089"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-7000-a000-000000000061"));
        }
    }
}
