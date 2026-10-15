using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class PurchaseReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PurchaseLineId",
                table: "ReservationFulfillments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationFulfillments_PurchaseLineId",
                table: "ReservationFulfillments",
                column: "PurchaseLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationFulfillments_InvoiceLines_PurchaseLineId",
                table: "ReservationFulfillments",
                column: "PurchaseLineId",
                principalTable: "InvoiceLines",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationFulfillments_InvoiceLines_PurchaseLineId",
                table: "ReservationFulfillments");

            migrationBuilder.DropIndex(
                name: "IX_ReservationFulfillments_PurchaseLineId",
                table: "ReservationFulfillments");

            migrationBuilder.DropColumn(
                name: "PurchaseLineId",
                table: "ReservationFulfillments");
        }
    }
}
