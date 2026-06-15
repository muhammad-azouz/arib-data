using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class AddBranchFKToWarehouseProductInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                table: "WarehousesProductInventories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("019EC6A5-D2F0-728E-A8EF-08B247391242"));

            migrationBuilder.CreateIndex(
                name: "IX_WarehousesProductInventories_BranchId",
                table: "WarehousesProductInventories",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehousesProductInventories_Branches_BranchId",
                table: "WarehousesProductInventories",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehousesProductInventories_Branches_BranchId",
                table: "WarehousesProductInventories");

            migrationBuilder.DropIndex(
                name: "IX_WarehousesProductInventories_BranchId",
                table: "WarehousesProductInventories");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "WarehousesProductInventories");
        }
    }
}
