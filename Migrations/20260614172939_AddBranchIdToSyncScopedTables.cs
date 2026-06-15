using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class AddBranchIdToSyncScopedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehousesProductInventories_Branches_BranchId",
                table: "WarehousesProductInventories");

            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                table: "ProductOpeningBalances",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                table: "OrderFulfillments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                table: "InventoryAdjustments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                table: "DailyProductCosts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            // Backfill the new column before the FK is enforced. The warehouse-scoped
            // rows take their branch from their warehouse; OrderFulfillments take it
            // from the order entry's bill (D13). Empty-guid leftovers (orphaned rows,
            // if any) would fail the FK below — by design, surfacing bad data.
            //
            // Wrapped in EXEC: the idempotent script (sync-gateway/baseline.sql) emits
            // the whole migration as one batch, and DML can't reference a column added
            // by an ALTER earlier in the same batch ("Invalid column name"). Dynamic
            // SQL runs in its own scope, after the ADD has executed.
            migrationBuilder.Sql(
                "EXEC(N'UPDATE t SET t.BranchId = w.BranchId FROM [DailyProductCosts] t " +
                "JOIN [Warehouses] w ON w.Id = t.WarehouseId;');");
            migrationBuilder.Sql(
                "EXEC(N'UPDATE t SET t.BranchId = w.BranchId FROM [ProductOpeningBalances] t " +
                "JOIN [Warehouses] w ON w.Id = t.WarehouseId;');");
            migrationBuilder.Sql(
                "EXEC(N'UPDATE t SET t.BranchId = w.BranchId FROM [InventoryAdjustments] t " +
                "JOIN [Warehouses] w ON w.Id = t.WarehouseId;');");
            migrationBuilder.Sql(
                "EXEC(N'UPDATE t SET t.BranchId = be.BranchId FROM [OrderFulfillments] t " +
                "JOIN [BillEntries] be ON be.Id = t.OrderEntryId;');");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOpeningBalances_BranchId",
                table: "ProductOpeningBalances",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderFulfillments_BranchId",
                table: "OrderFulfillments",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAdjustments_BranchId",
                table: "InventoryAdjustments",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyProductCosts_BranchId",
                table: "DailyProductCosts",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyProductCosts_Branches_BranchId",
                table: "DailyProductCosts",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryAdjustments_Branches_BranchId",
                table: "InventoryAdjustments",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderFulfillments_Branches_BranchId",
                table: "OrderFulfillments",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOpeningBalances_Branches_BranchId",
                table: "ProductOpeningBalances",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_DailyProductCosts_Branches_BranchId",
                table: "DailyProductCosts");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryAdjustments_Branches_BranchId",
                table: "InventoryAdjustments");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderFulfillments_Branches_BranchId",
                table: "OrderFulfillments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOpeningBalances_Branches_BranchId",
                table: "ProductOpeningBalances");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehousesProductInventories_Branches_BranchId",
                table: "WarehousesProductInventories");

            migrationBuilder.DropIndex(
                name: "IX_ProductOpeningBalances_BranchId",
                table: "ProductOpeningBalances");

            migrationBuilder.DropIndex(
                name: "IX_OrderFulfillments_BranchId",
                table: "OrderFulfillments");

            migrationBuilder.DropIndex(
                name: "IX_InventoryAdjustments_BranchId",
                table: "InventoryAdjustments");

            migrationBuilder.DropIndex(
                name: "IX_DailyProductCosts_BranchId",
                table: "DailyProductCosts");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "ProductOpeningBalances");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "OrderFulfillments");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "InventoryAdjustments");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "DailyProductCosts");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehousesProductInventories_Branches_BranchId",
                table: "WarehousesProductInventories",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
