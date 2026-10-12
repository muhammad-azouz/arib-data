using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class AddSellOnCreditPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("00000003-0000-7000-a000-000000000062"), "يمكنه بيع الفواتير آجلاً للعملاء (ترك مبلغ متبقي على حساب العميل)، وبدونها يجب سداد الفاتورة بالكامل", "البيع الآجل" });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[] { new Guid("00000004-0000-7000-a000-000000000090"), new Guid("00000003-0000-7000-a000-000000000062"), new Guid("00000002-0000-7000-a000-000000000001") });

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
                WHERE p.Id = '00000003-0000-7000-a000-000000000062'
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
                WHERE rp.PermissionId = '00000003-0000-7000-a000-000000000062'
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
                keyValue: new Guid("00000004-0000-7000-a000-000000000090"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-7000-a000-000000000062"));
        }
    }
}
