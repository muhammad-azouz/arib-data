using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class AddInventoryBatches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ProductTransaction -> InventoryMovement and DailyProductCost ->
            // WeightedAverageCost are renames: the new tables are created, existing
            // rows are copied across by column name, then the old tables are dropped.
            // (Scaffolding emitted a destructive drop+create; this keeps existing data.)
            migrationBuilder.CreateTable(
                name: "InventoryBatches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BatchNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InitialQty = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    RemainingQty = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    SourceRegNum = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryBatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryBatches_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryBatches_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryBatches_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryMovements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Dealing = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InQty = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    InPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    InTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OutQty = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    OutPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OutTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegNum = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pc = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryMovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryMovements_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryMovements_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryMovements_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeightedAverageCosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightedAverageCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeightedAverageCosts_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WeightedAverageCosts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeightedAverageCosts_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryBatchConsumptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegNum = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryBatchConsumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryBatchConsumptions_InventoryBatches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "InventoryBatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBatchConsumptions_BatchId",
                table: "InventoryBatchConsumptions",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBatchConsumptions_RegNum",
                table: "InventoryBatchConsumptions",
                column: "RegNum");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBatches_BranchId",
                table: "InventoryBatches",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBatches_ExpiryDate",
                table: "InventoryBatches",
                column: "ExpiryDate");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBatches_ProductId_WarehouseId_RemainingQty",
                table: "InventoryBatches",
                columns: new[] { "ProductId", "WarehouseId", "RemainingQty" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBatches_SourceRegNum",
                table: "InventoryBatches",
                column: "SourceRegNum");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBatches_WarehouseId",
                table: "InventoryBatches",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovements_CustomerId",
                table: "InventoryMovements",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovements_ProductId",
                table: "InventoryMovements",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovements_WarehouseId",
                table: "InventoryMovements",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_WeightedAverageCosts_BranchId",
                table: "WeightedAverageCosts",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_WeightedAverageCosts_ProductId",
                table: "WeightedAverageCosts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WeightedAverageCosts_WarehouseId",
                table: "WeightedAverageCosts",
                column: "WarehouseId");

            // Copy existing rows into the renamed tables (preserving Ids and all
            // logical references such as RegNum), then drop the originals.
            migrationBuilder.Sql(@"
                INSERT INTO InventoryMovements
                    (Id, IssueDate, ProductId, CustomerId, WarehouseId, Dealing, ExpirationDate,
                     InQty, InPrice, InTotal, OutQty, OutPrice, OutTotal, Cost, Unit, IsDeleted,
                     BranchId, RegNum, UserId, Pc)
                SELECT
                    Id, IssueDate, ProductId, CustomerId, WarehouseId, Dealing, ExpirationDate,
                    InQty, InPrice, InTotal, OutQty, OutPrice, OutTotal, Cost, Unit, IsDeleted,
                    BranchId, RegNum, UserId, Pc
                FROM ProductTransactions;");

            migrationBuilder.Sql(@"
                INSERT INTO WeightedAverageCosts
                    (Id, ProductId, WarehouseId, BranchId, Qty, Cost, Price, ReceivedDate)
                SELECT
                    Id, ProductId, WarehouseId, BranchId, Qty, Cost, Price, ReceivedDate
                FROM DailyProductCosts;");

            migrationBuilder.DropTable(name: "ProductTransactions");
            migrationBuilder.DropTable(name: "DailyProductCosts");

            // Backfill: seed one opening cost+expiry layer from current on-hand stock
            // for every batch-tracked product (FIFO/LIFO, or expiry-tracked) so existing
            // inventory is immediately sellable under batch costing. WA-without-expiry
            // products are intentionally skipped (InventoryValuationMethod 0 = WA).
            migrationBuilder.Sql(@"
                INSERT INTO InventoryBatches
                    (Id, ProductId, WarehouseId, BranchId, BatchNumber, ExpiryDate, ReceivedDate,
                     InitialQty, RemainingQty, UnitCost, SourceRegNum, CreatedAt)
                SELECT
                    NEWID(), wpi.ProductId, wpi.WarehouseId, wpi.BranchId, N'OPENING', NULL, SYSDATETIME(),
                    wpi.TotalQty, wpi.TotalQty, wpi.UnitCost, '00000000-0000-0000-0000-000000000000', SYSDATETIME()
                FROM WarehousesProductInventories wpi
                INNER JOIN Products p ON p.Id = wpi.ProductId
                WHERE wpi.TotalQty <> 0
                  AND (p.IsExpire = 1 OR p.InventoryValuationMethod <> 0);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyProductCosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BatchNumber = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyProductCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyProductCosts_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyProductCosts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyProductCosts_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Dealing = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    InQty = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    InTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OutPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OutQty = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    OutTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Pc = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RegNum = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTransactions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductTransactions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTransactions_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyProductCosts_BranchId",
                table: "DailyProductCosts",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyProductCosts_ProductId",
                table: "DailyProductCosts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyProductCosts_WarehouseId",
                table: "DailyProductCosts",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTransactions_CustomerId",
                table: "ProductTransactions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTransactions_ProductId",
                table: "ProductTransactions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTransactions_WarehouseId",
                table: "ProductTransactions",
                column: "WarehouseId");

            // Copy rows back into the original tables (BatchNumber/ExpirationDate were
            // dropped on the way up, so they take their defaults here).
            migrationBuilder.Sql(@"
                INSERT INTO ProductTransactions
                    (Id, IssueDate, ProductId, CustomerId, WarehouseId, Dealing, ExpirationDate,
                     InQty, InPrice, InTotal, OutQty, OutPrice, OutTotal, Cost, Unit, IsDeleted,
                     BranchId, RegNum, UserId, Pc)
                SELECT
                    Id, IssueDate, ProductId, CustomerId, WarehouseId, Dealing, ExpirationDate,
                    InQty, InPrice, InTotal, OutQty, OutPrice, OutTotal, Cost, Unit, IsDeleted,
                    BranchId, RegNum, UserId, Pc
                FROM InventoryMovements;");

            migrationBuilder.Sql(@"
                INSERT INTO DailyProductCosts
                    (Id, ProductId, WarehouseId, BranchId, BatchNumber, Cost, ExpirationDate, Price, Qty, ReceivedDate)
                SELECT
                    Id, ProductId, WarehouseId, BranchId, 0, Cost, NULL, Price, Qty, ReceivedDate
                FROM WeightedAverageCosts;");

            migrationBuilder.DropTable(name: "InventoryBatchConsumptions");
            migrationBuilder.DropTable(name: "InventoryMovements");
            migrationBuilder.DropTable(name: "WeightedAverageCosts");
            migrationBuilder.DropTable(name: "InventoryBatches");
        }
    }
}
