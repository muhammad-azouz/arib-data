using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class PermissionEditMasterData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("00000003-0000-7000-a000-000000000042"), "تعديل المنتجات والأسعار والحسابات (يُسمح به للإدارة الرئيسية فقط على الفروع السحابية)", "تعديل البيانات الرئيسية" });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[] { new Guid("00000004-0000-7000-a000-000000000071"), new Guid("00000003-0000-7000-a000-000000000042"), new Guid("00000002-0000-7000-a000-000000000001") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-7000-a000-000000000071"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-7000-a000-000000000042"));
        }
    }
}
