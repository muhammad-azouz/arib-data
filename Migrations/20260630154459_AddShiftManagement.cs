using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class AddShiftManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShiftId",
                table: "TreasuriesTransactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShiftId",
                table: "RevenueExpenses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShiftId",
                table: "InventoryAdjustments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShiftId",
                table: "EWalletTransactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShiftId",
                table: "CustomerTransactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShiftModeEnabled",
                table: "Branches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            // Bills.ShiftId was a dead pre-GUID int (always 0). SQL Server cannot
            // cast int -> uniqueidentifier, so drop the meaningless int and add the
            // nullable Guid afresh (all existing bills become ShiftId = NULL =
            // "pre-shift era", which is exactly what we want).
            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "Bills");

            migrationBuilder.AddColumn<Guid>(
                name: "ShiftId",
                table: "Bills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShiftId",
                table: "BillPayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShiftId",
                table: "BankTransactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Num = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreasuryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkstationId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OpenedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpenedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpeningCash = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    OpenNote = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ClosedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClosedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpectedCash = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    ActualCash = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    Difference = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    CloseNote = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsForceClosed = table.Column<bool>(type: "bit", nullable: false),
                    ExpectedBank = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    ExpectedWallet = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    ExpectedCredit = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    SalesTotal = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    RefundsTotal = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shifts_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shifts_Users_OpenedByUserId",
                        column: x => x.OpenedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("00000003-0000-7000-a000-000000000047"), "يمكنه إغلاق ورديه كاشير آخر (إغلاق إجباري)", "اغلاق الورديه إجباري" },
                    { new Guid("00000003-0000-7000-a000-000000000048"), "يمكنه عرض ورديات وتقارير الكاشيرين الآخرين", "عرض ورديات الكاشيرين الآخرين" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("00000004-0000-7000-a000-000000000076"), new Guid("00000003-0000-7000-a000-000000000047"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000077"), new Guid("00000003-0000-7000-a000-000000000048"), new Guid("00000002-0000-7000-a000-000000000001") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TreasuriesTransactions_ShiftId",
                table: "TreasuriesTransactions",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_RevenueExpenses_ShiftId",
                table: "RevenueExpenses",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAdjustments_ShiftId",
                table: "InventoryAdjustments",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_EWalletTransactions_ShiftId",
                table: "EWalletTransactions",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTransactions_ShiftId",
                table: "CustomerTransactions",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ShiftId",
                table: "Bills",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_BillPayments_ShiftId",
                table: "BillPayments",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_ShiftId",
                table: "BankTransactions",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_BranchId_WorkstationId_Status",
                table: "Shifts",
                columns: new[] { "BranchId", "WorkstationId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_OpenedByUserId",
                table: "Shifts",
                column: "OpenedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_TreasuriesTransactions_ShiftId",
                table: "TreasuriesTransactions");

            migrationBuilder.DropIndex(
                name: "IX_RevenueExpenses_ShiftId",
                table: "RevenueExpenses");

            migrationBuilder.DropIndex(
                name: "IX_InventoryAdjustments_ShiftId",
                table: "InventoryAdjustments");

            migrationBuilder.DropIndex(
                name: "IX_EWalletTransactions_ShiftId",
                table: "EWalletTransactions");

            migrationBuilder.DropIndex(
                name: "IX_CustomerTransactions_ShiftId",
                table: "CustomerTransactions");

            migrationBuilder.DropIndex(
                name: "IX_Bills_ShiftId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_BillPayments_ShiftId",
                table: "BillPayments");

            migrationBuilder.DropIndex(
                name: "IX_BankTransactions_ShiftId",
                table: "BankTransactions");

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-7000-a000-000000000076"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-7000-a000-000000000077"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-7000-a000-000000000047"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-7000-a000-000000000048"));

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "TreasuriesTransactions");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "RevenueExpenses");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "InventoryAdjustments");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "EWalletTransactions");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "CustomerTransactions");

            migrationBuilder.DropColumn(
                name: "ShiftModeEnabled",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "BillPayments");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "BankTransactions");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "Bills");

            migrationBuilder.AddColumn<int>(
                name: "ShiftId",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
