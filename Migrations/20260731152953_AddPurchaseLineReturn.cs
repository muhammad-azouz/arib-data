using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class AddPurchaseLineReturn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseLineReturns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseReturnLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseLineReturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseLineReturns_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseLineReturns_InvoiceLines_PurchaseLineId",
                        column: x => x.PurchaseLineId,
                        principalTable: "InvoiceLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseLineReturns_InvoiceLines_PurchaseReturnLineId",
                        column: x => x.PurchaseReturnLineId,
                        principalTable: "InvoiceLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseLineReturns_BranchId",
                table: "PurchaseLineReturns",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseLineReturns_PurchaseLineId",
                table: "PurchaseLineReturns",
                column: "PurchaseLineId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseLineReturns_PurchaseReturnLineId",
                table: "PurchaseLineReturns",
                column: "PurchaseReturnLineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseLineReturns");
        }
    }
}
