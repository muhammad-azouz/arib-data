using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class IntroduceCurrencyIsDefualtCulomn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Currencies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("00000008-0000-7000-a000-000000000001"),
                column: "IsDefault",
                value: true);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("00000008-0000-7000-a000-000000000002"),
                column: "IsDefault",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Currencies");
        }
    }
}
