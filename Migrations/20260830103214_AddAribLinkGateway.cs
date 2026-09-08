using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class AddAribLinkGateway : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PinFailedCount",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PinHash",
                table: "Users",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PinLockedUntil",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AllowsFractionalQty",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.CreateTable(
                name: "TerminalOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TicketCode = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SectionId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    SectionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TerminalId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ClientRequestId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemCount = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SaleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerminalOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TerminalOrders_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TerminalOrders_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TerminalOrderLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TerminalOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UnitName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    LineTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AddedBySectionId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    AddedBySectionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AddedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddedByTerminalId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerminalOrderLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TerminalOrderLines_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TerminalOrderLines_TerminalOrders_TerminalOrderId",
                        column: x => x.TerminalOrderId,
                        principalTable: "TerminalOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TerminalOrderLines_Users_AddedByUserId",
                        column: x => x.AddedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("00000003-0000-7000-a000-000000000057"), "يمكنه إدارة الأجهزة الطرفية ومقاعدها", "ادارة الأجهزة الطرفية" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-7000-a000-000000000001"),
                columns: new[] { "PinFailedCount", "PinHash", "PinLockedUntil" },
                values: new object[] { 0, null, null });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[] { new Guid("00000004-0000-7000-a000-000000000085"), new Guid("00000003-0000-7000-a000-000000000057"), new Guid("00000002-0000-7000-a000-000000000001") });

            migrationBuilder.CreateIndex(
                name: "IX_TerminalOrderLines_AddedByUserId",
                table: "TerminalOrderLines",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TerminalOrderLines_ProductId",
                table: "TerminalOrderLines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TerminalOrderLines_TerminalOrderId",
                table: "TerminalOrderLines",
                column: "TerminalOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TerminalOrders_BranchId_Status_CreatedAt",
                table: "TerminalOrders",
                columns: new[] { "BranchId", "Status", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_TerminalOrders_BranchId_TicketCode",
                table: "TerminalOrders",
                columns: new[] { "BranchId", "TicketCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TerminalOrders_CreatedByUserId",
                table: "TerminalOrders",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TerminalOrders_TerminalId_ClientRequestId",
                table: "TerminalOrders",
                columns: new[] { "TerminalId", "ClientRequestId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TerminalOrderLines");

            migrationBuilder.DropTable(
                name: "TerminalOrders");

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-7000-a000-000000000085"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-7000-a000-000000000057"));

            migrationBuilder.DropColumn(
                name: "PinFailedCount",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PinHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PinLockedUntil",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AllowsFractionalQty",
                table: "Products");
        }
    }
}
