using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class AddStockTransferCancelledFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CancelledAt",
                table: "StockTransfers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CancelledByUserId",
                table: "StockTransfers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_CancelledByUserId",
                table: "StockTransfers",
                column: "CancelledByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransfers_Users_CancelledByUserId",
                table: "StockTransfers",
                column: "CancelledByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransfers_Users_CancelledByUserId",
                table: "StockTransfers");

            migrationBuilder.DropIndex(
                name: "IX_StockTransfers_CancelledByUserId",
                table: "StockTransfers");

            migrationBuilder.DropColumn(
                name: "CancelledAt",
                table: "StockTransfers");

            migrationBuilder.DropColumn(
                name: "CancelledByUserId",
                table: "StockTransfers");
        }
    }
}
