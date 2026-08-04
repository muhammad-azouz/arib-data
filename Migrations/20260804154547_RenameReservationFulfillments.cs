using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class RenameReservationFulfillments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop FKs/PK owned by the table being renamed
            migrationBuilder.DropForeignKey(name: "FK_OrderFulfillments_Branches_BranchId", table: "OrderFulfillments");
            migrationBuilder.DropForeignKey(name: "FK_OrderFulfillments_InvoiceLines_OrderLineId", table: "OrderFulfillments");
            migrationBuilder.DropForeignKey(name: "FK_OrderFulfillments_InvoiceLines_SaleLineId", table: "OrderFulfillments");
            migrationBuilder.DropForeignKey(name: "FK_OrderFulfillments_InvoiceLines_SalesReturnLineId", table: "OrderFulfillments");

            migrationBuilder.DropPrimaryKey(name: "PK_OrderFulfillments", table: "OrderFulfillments");

            migrationBuilder.RenameTable(name: "OrderFulfillments", newName: "ReservationFulfillments");

            migrationBuilder.RenameIndex(name: "IX_OrderFulfillments_BranchId", table: "ReservationFulfillments", newName: "IX_ReservationFulfillments_BranchId");
            migrationBuilder.RenameIndex(name: "IX_OrderFulfillments_OrderLineId", table: "ReservationFulfillments", newName: "IX_ReservationFulfillments_OrderLineId");
            migrationBuilder.RenameIndex(name: "IX_OrderFulfillments_SaleLineId", table: "ReservationFulfillments", newName: "IX_ReservationFulfillments_SaleLineId");
            migrationBuilder.RenameIndex(name: "IX_OrderFulfillments_SalesReturnLineId", table: "ReservationFulfillments", newName: "IX_ReservationFulfillments_SalesReturnLineId");

            migrationBuilder.AddPrimaryKey(name: "PK_ReservationFulfillments", table: "ReservationFulfillments", column: "Id");

            migrationBuilder.AddForeignKey(name: "FK_ReservationFulfillments_Branches_BranchId", table: "ReservationFulfillments", column: "BranchId", principalTable: "Branches", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_ReservationFulfillments_InvoiceLines_OrderLineId", table: "ReservationFulfillments", column: "OrderLineId", principalTable: "InvoiceLines", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_ReservationFulfillments_InvoiceLines_SaleLineId", table: "ReservationFulfillments", column: "SaleLineId", principalTable: "InvoiceLines", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_ReservationFulfillments_InvoiceLines_SalesReturnLineId", table: "ReservationFulfillments", column: "SalesReturnLineId", principalTable: "InvoiceLines", principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_ReservationFulfillments_Branches_BranchId", table: "ReservationFulfillments");
            migrationBuilder.DropForeignKey(name: "FK_ReservationFulfillments_InvoiceLines_OrderLineId", table: "ReservationFulfillments");
            migrationBuilder.DropForeignKey(name: "FK_ReservationFulfillments_InvoiceLines_SaleLineId", table: "ReservationFulfillments");
            migrationBuilder.DropForeignKey(name: "FK_ReservationFulfillments_InvoiceLines_SalesReturnLineId", table: "ReservationFulfillments");

            migrationBuilder.DropPrimaryKey(name: "PK_ReservationFulfillments", table: "ReservationFulfillments");

            migrationBuilder.RenameTable(name: "ReservationFulfillments", newName: "OrderFulfillments");

            migrationBuilder.RenameIndex(name: "IX_ReservationFulfillments_BranchId", table: "OrderFulfillments", newName: "IX_OrderFulfillments_BranchId");
            migrationBuilder.RenameIndex(name: "IX_ReservationFulfillments_OrderLineId", table: "OrderFulfillments", newName: "IX_OrderFulfillments_OrderLineId");
            migrationBuilder.RenameIndex(name: "IX_ReservationFulfillments_SaleLineId", table: "OrderFulfillments", newName: "IX_OrderFulfillments_SaleLineId");
            migrationBuilder.RenameIndex(name: "IX_ReservationFulfillments_SalesReturnLineId", table: "OrderFulfillments", newName: "IX_OrderFulfillments_SalesReturnLineId");

            migrationBuilder.AddPrimaryKey(name: "PK_OrderFulfillments", table: "OrderFulfillments", column: "Id");

            migrationBuilder.AddForeignKey(name: "FK_OrderFulfillments_Branches_BranchId", table: "OrderFulfillments", column: "BranchId", principalTable: "Branches", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_OrderFulfillments_InvoiceLines_OrderLineId", table: "OrderFulfillments", column: "OrderLineId", principalTable: "InvoiceLines", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_OrderFulfillments_InvoiceLines_SaleLineId", table: "OrderFulfillments", column: "SaleLineId", principalTable: "InvoiceLines", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_OrderFulfillments_InvoiceLines_SalesReturnLineId", table: "OrderFulfillments", column: "SalesReturnLineId", principalTable: "InvoiceLines", principalColumn: "Id");
        }
    }
}
