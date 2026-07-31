using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleLineReturn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OriginalInvoiceId",
                table: "Invoices",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SaleLineReturns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaleLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesReturnLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleLineReturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleLineReturns_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleLineReturns_InvoiceLines_SaleLineId",
                        column: x => x.SaleLineId,
                        principalTable: "InvoiceLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleLineReturns_InvoiceLines_SalesReturnLineId",
                        column: x => x.SalesReturnLineId,
                        principalTable: "InvoiceLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleLineReturns_BranchId",
                table: "SaleLineReturns",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleLineReturns_SaleLineId",
                table: "SaleLineReturns",
                column: "SaleLineId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleLineReturns_SalesReturnLineId",
                table: "SaleLineReturns",
                column: "SalesReturnLineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleLineReturns");

            migrationBuilder.DropColumn(
                name: "OriginalInvoiceId",
                table: "Invoices");
        }
    }
}
