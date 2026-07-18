using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class RenameAccountingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop FKs on tables that aren't being renamed but reference renamed columns/tables
            migrationBuilder.DropForeignKey(name: "FK_BankTransactions_Banks_BankId", table: "BankTransactions");
            migrationBuilder.DropForeignKey(name: "FK_BankTransactions_Customers_CustomerId", table: "BankTransactions");
            migrationBuilder.DropForeignKey(name: "FK_Bills_Branches_BranchId", table: "Bills");
            migrationBuilder.DropForeignKey(name: "FK_Bills_Customers_CustomerId", table: "Bills");
            migrationBuilder.DropForeignKey(name: "FK_Bills_Users_UserId", table: "Bills");
            migrationBuilder.DropForeignKey(name: "FK_EWalletTransactions_Customers_CustomerId", table: "EWalletTransactions");
            migrationBuilder.DropForeignKey(name: "FK_InstallmentPlans_Customers_CustomerId", table: "InstallmentPlans");
            migrationBuilder.DropForeignKey(name: "FK_InventoryMovements_Customers_CustomerId", table: "InventoryMovements");
            migrationBuilder.DropForeignKey(name: "FK_OrderFulfillments_BillEntries_OrderEntryId", table: "OrderFulfillments");
            migrationBuilder.DropForeignKey(name: "FK_OrderFulfillments_BillEntries_ReSaleEntryId", table: "OrderFulfillments");
            migrationBuilder.DropForeignKey(name: "FK_OrderFulfillments_BillEntries_SaleEntryId", table: "OrderFulfillments");

            // Drop FKs owned by the 9 tables being renamed
            migrationBuilder.DropForeignKey(name: "FK_Banks_Accounts_AccountId", table: "Banks");
            migrationBuilder.DropForeignKey(name: "FK_Banks_Currencies_CurrencyId", table: "Banks");
            migrationBuilder.DropForeignKey(name: "FK_Banks_Users_UserId", table: "Banks");

            migrationBuilder.DropForeignKey(name: "FK_BillEntries_Bills_BillId", table: "BillEntries");
            migrationBuilder.DropForeignKey(name: "FK_BillEntries_Branches_BranchId", table: "BillEntries");
            migrationBuilder.DropForeignKey(name: "FK_BillEntries_Customers_CustomerId", table: "BillEntries");
            migrationBuilder.DropForeignKey(name: "FK_BillEntries_Products_ProductId", table: "BillEntries");
            migrationBuilder.DropForeignKey(name: "FK_BillEntries_UnitOfMeasures_UnitId", table: "BillEntries");
            migrationBuilder.DropForeignKey(name: "FK_BillEntries_Warehouses_WarehouseId", table: "BillEntries");

            migrationBuilder.DropForeignKey(name: "FK_BillPayments_Bills_BillId", table: "BillPayments");

            migrationBuilder.DropForeignKey(name: "FK_Cashes_Banks_BankId", table: "Cashes");
            migrationBuilder.DropForeignKey(name: "FK_Cashes_Branches_BranchId", table: "Cashes");
            migrationBuilder.DropForeignKey(name: "FK_Cashes_Currencies_CurrencyId", table: "Cashes");
            migrationBuilder.DropForeignKey(name: "FK_Cashes_Customers_CustomerId", table: "Cashes");
            migrationBuilder.DropForeignKey(name: "FK_Cashes_EWallets_EWalletId", table: "Cashes");
            migrationBuilder.DropForeignKey(name: "FK_Cashes_Treasuries_TreasuryId", table: "Cashes");
            migrationBuilder.DropForeignKey(name: "FK_Cashes_Users_UserId", table: "Cashes");

            migrationBuilder.DropForeignKey(name: "FK_Customers_Areas_AreaId", table: "Customers");
            migrationBuilder.DropForeignKey(name: "FK_Customers_Groups_GroupId", table: "Customers");
            migrationBuilder.DropForeignKey(name: "FK_Customers_Images_ImageId", table: "Customers");

            migrationBuilder.DropForeignKey(name: "FK_CustomerTransactions_Customers_CustomerId", table: "CustomerTransactions");
            migrationBuilder.DropForeignKey(name: "FK_CustomerTransactions_Users_UserId", table: "CustomerTransactions");

            migrationBuilder.DropForeignKey(name: "FK_JournalEntries_Accounts_AccountId", table: "JournalEntries");
            migrationBuilder.DropForeignKey(name: "FK_JournalEntries_Branches_BranchId", table: "JournalEntries");
            migrationBuilder.DropForeignKey(name: "FK_JournalEntries_Customers_CustomerId", table: "JournalEntries");

            migrationBuilder.DropForeignKey(name: "FK_RevenueExpenses_Accounts_AccountId", table: "RevenueExpenses");
            migrationBuilder.DropForeignKey(name: "FK_RevenueExpenses_Banks_BankId", table: "RevenueExpenses");
            migrationBuilder.DropForeignKey(name: "FK_RevenueExpenses_Branches_BranchId", table: "RevenueExpenses");
            migrationBuilder.DropForeignKey(name: "FK_RevenueExpenses_Currencies_CurrencyId", table: "RevenueExpenses");
            migrationBuilder.DropForeignKey(name: "FK_RevenueExpenses_EWallets_EwalletId", table: "RevenueExpenses");
            migrationBuilder.DropForeignKey(name: "FK_RevenueExpenses_Treasuries_TreasuryId", table: "RevenueExpenses");
            migrationBuilder.DropForeignKey(name: "FK_RevenueExpenses_Users_UserId", table: "RevenueExpenses");

            // Drop primary keys about to be renamed
            migrationBuilder.DropPrimaryKey(name: "PK_AccountOperands", table: "AccountOperands");
            migrationBuilder.DropPrimaryKey(name: "PK_Banks", table: "Banks");
            migrationBuilder.DropPrimaryKey(name: "PK_BillEntries", table: "BillEntries");
            migrationBuilder.DropPrimaryKey(name: "PK_BillPayments", table: "BillPayments");
            migrationBuilder.DropPrimaryKey(name: "PK_Bills", table: "Bills");
            migrationBuilder.DropPrimaryKey(name: "PK_Cashes", table: "Cashes");
            migrationBuilder.DropPrimaryKey(name: "PK_Customers", table: "Customers");
            migrationBuilder.DropPrimaryKey(name: "PK_CustomerTransactions", table: "CustomerTransactions");
            migrationBuilder.DropPrimaryKey(name: "PK_JournalEntries", table: "JournalEntries");
            migrationBuilder.DropPrimaryKey(name: "PK_RevenueExpenses", table: "RevenueExpenses");

            // Rename tables
            migrationBuilder.RenameTable(name: "AccountOperands", newName: "PostingAccounts");
            migrationBuilder.RenameTable(name: "Banks", newName: "BankAccounts");
            migrationBuilder.RenameTable(name: "BillEntries", newName: "InvoiceLines");
            migrationBuilder.RenameTable(name: "BillPayments", newName: "InvoicePayments");
            migrationBuilder.RenameTable(name: "Bills", newName: "Invoices");
            migrationBuilder.RenameTable(name: "Cashes", newName: "PaymentVouchers");
            migrationBuilder.RenameTable(name: "Customers", newName: "Partners");
            migrationBuilder.RenameTable(name: "CustomerTransactions", newName: "PartnerLedgerEntries");
            migrationBuilder.RenameTable(name: "JournalEntries", newName: "GeneralLedgerEntries");
            migrationBuilder.RenameTable(name: "RevenueExpenses", newName: "ExpenseIncomeVouchers");

            // Rename columns
            migrationBuilder.RenameColumn(name: "Operand", table: "PostingAccounts", newName: "Role");

            migrationBuilder.RenameColumn(name: "BillId", table: "InvoiceLines", newName: "InvoiceId");
            migrationBuilder.RenameColumn(name: "CustomerId", table: "InvoiceLines", newName: "PartnerId");

            migrationBuilder.RenameColumn(name: "BillId", table: "InvoicePayments", newName: "InvoiceId");

            migrationBuilder.RenameColumn(name: "CustomerId", table: "PaymentVouchers", newName: "PartnerId");

            migrationBuilder.RenameColumn(name: "CustomerId", table: "PartnerLedgerEntries", newName: "PartnerId");

            migrationBuilder.RenameColumn(name: "CustomerId", table: "GeneralLedgerEntries", newName: "PartnerId");

            migrationBuilder.RenameColumn(name: "CustomerId", table: "Invoices", newName: "PartnerId");

            migrationBuilder.RenameColumn(name: "CustomerId", table: "TreasuriesTransactions", newName: "PartnerId");
            migrationBuilder.RenameColumn(name: "CustomerId", table: "InventoryMovements", newName: "PartnerId");
            migrationBuilder.RenameColumn(name: "SourceBillId", table: "InstallmentPlans", newName: "SourceInvoiceId");
            migrationBuilder.RenameColumn(name: "CustomerId", table: "InstallmentPlans", newName: "PartnerId");
            migrationBuilder.RenameColumn(name: "CustomerId", table: "EWalletTransactions", newName: "PartnerId");
            migrationBuilder.RenameColumn(name: "CustomerId", table: "BankTransactions", newName: "PartnerId");

            // OrderFulfillments: OrderEntry->OrderLine, SaleEntry->SaleLine, ReSaleEntry->SalesReturnLine
            migrationBuilder.RenameColumn(name: "SaleEntryId", table: "OrderFulfillments", newName: "SaleLineId");
            migrationBuilder.RenameColumn(name: "ReSaleEntryId", table: "OrderFulfillments", newName: "SalesReturnLineId");
            migrationBuilder.RenameColumn(name: "OrderEntryId", table: "OrderFulfillments", newName: "OrderLineId");

            // Rename indexes
            migrationBuilder.RenameIndex(name: "IX_Banks_AccountId", table: "BankAccounts", newName: "IX_BankAccounts_AccountId");
            migrationBuilder.RenameIndex(name: "IX_Banks_CurrencyId", table: "BankAccounts", newName: "IX_BankAccounts_CurrencyId");
            migrationBuilder.RenameIndex(name: "IX_Banks_UserId", table: "BankAccounts", newName: "IX_BankAccounts_UserId");

            migrationBuilder.RenameIndex(name: "IX_BillEntries_BillId", table: "InvoiceLines", newName: "IX_InvoiceLines_InvoiceId");
            migrationBuilder.RenameIndex(name: "IX_BillEntries_BranchId", table: "InvoiceLines", newName: "IX_InvoiceLines_BranchId");
            migrationBuilder.RenameIndex(name: "IX_BillEntries_CustomerId", table: "InvoiceLines", newName: "IX_InvoiceLines_PartnerId");
            migrationBuilder.RenameIndex(name: "IX_BillEntries_ProductId", table: "InvoiceLines", newName: "IX_InvoiceLines_ProductId");
            migrationBuilder.RenameIndex(name: "IX_BillEntries_UnitId", table: "InvoiceLines", newName: "IX_InvoiceLines_UnitId");
            migrationBuilder.RenameIndex(name: "IX_BillEntries_WarehouseId", table: "InvoiceLines", newName: "IX_InvoiceLines_WarehouseId");

            migrationBuilder.RenameIndex(name: "IX_BillPayments_BillId", table: "InvoicePayments", newName: "IX_InvoicePayments_InvoiceId");
            migrationBuilder.RenameIndex(name: "IX_BillPayments_ShiftId", table: "InvoicePayments", newName: "IX_InvoicePayments_ShiftId");

            migrationBuilder.RenameIndex(name: "IX_Cashes_BankId", table: "PaymentVouchers", newName: "IX_PaymentVouchers_BankId");
            migrationBuilder.RenameIndex(name: "IX_Cashes_BranchId", table: "PaymentVouchers", newName: "IX_PaymentVouchers_BranchId");
            migrationBuilder.RenameIndex(name: "IX_Cashes_CurrencyId", table: "PaymentVouchers", newName: "IX_PaymentVouchers_CurrencyId");
            migrationBuilder.RenameIndex(name: "IX_Cashes_CustomerId", table: "PaymentVouchers", newName: "IX_PaymentVouchers_PartnerId");
            migrationBuilder.RenameIndex(name: "IX_Cashes_EWalletId", table: "PaymentVouchers", newName: "IX_PaymentVouchers_EWalletId");
            migrationBuilder.RenameIndex(name: "IX_Cashes_TreasuryId", table: "PaymentVouchers", newName: "IX_PaymentVouchers_TreasuryId");
            migrationBuilder.RenameIndex(name: "IX_Cashes_UserId", table: "PaymentVouchers", newName: "IX_PaymentVouchers_UserId");

            migrationBuilder.RenameIndex(name: "IX_Customers_AreaId", table: "Partners", newName: "IX_Partners_AreaId");
            migrationBuilder.RenameIndex(name: "IX_Customers_GroupId", table: "Partners", newName: "IX_Partners_GroupId");
            migrationBuilder.RenameIndex(name: "IX_Customers_ImageId", table: "Partners", newName: "IX_Partners_ImageId");

            migrationBuilder.RenameIndex(name: "IX_CustomerTransactions_CustomerId", table: "PartnerLedgerEntries", newName: "IX_PartnerLedgerEntries_PartnerId");
            migrationBuilder.RenameIndex(name: "IX_CustomerTransactions_ShiftId", table: "PartnerLedgerEntries", newName: "IX_PartnerLedgerEntries_ShiftId");
            migrationBuilder.RenameIndex(name: "IX_CustomerTransactions_UserId", table: "PartnerLedgerEntries", newName: "IX_PartnerLedgerEntries_UserId");

            migrationBuilder.RenameIndex(name: "IX_JournalEntries_AccountId", table: "GeneralLedgerEntries", newName: "IX_GeneralLedgerEntries_AccountId");
            migrationBuilder.RenameIndex(name: "IX_JournalEntries_BranchId", table: "GeneralLedgerEntries", newName: "IX_GeneralLedgerEntries_BranchId");
            migrationBuilder.RenameIndex(name: "IX_JournalEntries_CustomerId", table: "GeneralLedgerEntries", newName: "IX_GeneralLedgerEntries_PartnerId");

            migrationBuilder.RenameIndex(name: "IX_RevenueExpenses_AccountId", table: "ExpenseIncomeVouchers", newName: "IX_ExpenseIncomeVouchers_AccountId");
            migrationBuilder.RenameIndex(name: "IX_RevenueExpenses_BankId", table: "ExpenseIncomeVouchers", newName: "IX_ExpenseIncomeVouchers_BankId");
            migrationBuilder.RenameIndex(name: "IX_RevenueExpenses_BranchId", table: "ExpenseIncomeVouchers", newName: "IX_ExpenseIncomeVouchers_BranchId");
            migrationBuilder.RenameIndex(name: "IX_RevenueExpenses_CurrencyId", table: "ExpenseIncomeVouchers", newName: "IX_ExpenseIncomeVouchers_CurrencyId");
            migrationBuilder.RenameIndex(name: "IX_RevenueExpenses_EwalletId", table: "ExpenseIncomeVouchers", newName: "IX_ExpenseIncomeVouchers_EwalletId");
            migrationBuilder.RenameIndex(name: "IX_RevenueExpenses_ShiftId", table: "ExpenseIncomeVouchers", newName: "IX_ExpenseIncomeVouchers_ShiftId");
            migrationBuilder.RenameIndex(name: "IX_RevenueExpenses_TreasuryId", table: "ExpenseIncomeVouchers", newName: "IX_ExpenseIncomeVouchers_TreasuryId");
            migrationBuilder.RenameIndex(name: "IX_RevenueExpenses_UserId", table: "ExpenseIncomeVouchers", newName: "IX_ExpenseIncomeVouchers_UserId");

            migrationBuilder.RenameIndex(name: "IX_OrderFulfillments_SaleEntryId", table: "OrderFulfillments", newName: "IX_OrderFulfillments_SaleLineId");
            migrationBuilder.RenameIndex(name: "IX_OrderFulfillments_ReSaleEntryId", table: "OrderFulfillments", newName: "IX_OrderFulfillments_SalesReturnLineId");
            migrationBuilder.RenameIndex(name: "IX_OrderFulfillments_OrderEntryId", table: "OrderFulfillments", newName: "IX_OrderFulfillments_OrderLineId");

            migrationBuilder.RenameIndex(name: "IX_InstallmentPlans_CustomerId", table: "InstallmentPlans", newName: "IX_InstallmentPlans_PartnerId");
            migrationBuilder.RenameIndex(name: "IX_InventoryMovements_CustomerId", table: "InventoryMovements", newName: "IX_InventoryMovements_PartnerId");
            migrationBuilder.RenameIndex(name: "IX_EWalletTransactions_CustomerId", table: "EWalletTransactions", newName: "IX_EWalletTransactions_PartnerId");
            migrationBuilder.RenameIndex(name: "IX_BankTransactions_CustomerId", table: "BankTransactions", newName: "IX_BankTransactions_PartnerId");
            migrationBuilder.RenameIndex(name: "IX_Bills_UserId", table: "Invoices", newName: "IX_Invoices_UserId");
            migrationBuilder.RenameIndex(name: "IX_Bills_ShiftId", table: "Invoices", newName: "IX_Invoices_ShiftId");
            migrationBuilder.RenameIndex(name: "IX_Bills_CustomerId", table: "Invoices", newName: "IX_Invoices_PartnerId");
            migrationBuilder.RenameIndex(name: "IX_Bills_BranchId", table: "Invoices", newName: "IX_Invoices_BranchId");

            // Re-add primary keys with new names
            migrationBuilder.AddPrimaryKey(name: "PK_PostingAccounts", table: "PostingAccounts", column: "Role");
            migrationBuilder.AddPrimaryKey(name: "PK_BankAccounts", table: "BankAccounts", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_InvoiceLines", table: "InvoiceLines", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_InvoicePayments", table: "InvoicePayments", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_Invoices", table: "Invoices", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_PaymentVouchers", table: "PaymentVouchers", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_Partners", table: "Partners", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_PartnerLedgerEntries", table: "PartnerLedgerEntries", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_GeneralLedgerEntries", table: "GeneralLedgerEntries", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_ExpenseIncomeVouchers", table: "ExpenseIncomeVouchers", column: "Id");

            // Re-add foreign keys owned by the renamed tables, with new names
            migrationBuilder.AddForeignKey(name: "FK_BankAccounts_Accounts_AccountId", table: "BankAccounts", column: "AccountId", principalTable: "Accounts", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_BankAccounts_Currencies_CurrencyId", table: "BankAccounts", column: "CurrencyId", principalTable: "Currencies", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_BankAccounts_Users_UserId", table: "BankAccounts", column: "UserId", principalTable: "Users", principalColumn: "Id", onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(name: "FK_InvoiceLines_Branches_BranchId", table: "InvoiceLines", column: "BranchId", principalTable: "Branches", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_InvoiceLines_Invoices_InvoiceId", table: "InvoiceLines", column: "InvoiceId", principalTable: "Invoices", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_InvoiceLines_Partners_PartnerId", table: "InvoiceLines", column: "PartnerId", principalTable: "Partners", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_InvoiceLines_Products_ProductId", table: "InvoiceLines", column: "ProductId", principalTable: "Products", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_InvoiceLines_UnitOfMeasures_UnitId", table: "InvoiceLines", column: "UnitId", principalTable: "UnitOfMeasures", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_InvoiceLines_Warehouses_WarehouseId", table: "InvoiceLines", column: "WarehouseId", principalTable: "Warehouses", principalColumn: "Id", onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(name: "FK_InvoicePayments_Invoices_InvoiceId", table: "InvoicePayments", column: "InvoiceId", principalTable: "Invoices", principalColumn: "Id", onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(name: "FK_PaymentVouchers_BankAccounts_BankId", table: "PaymentVouchers", column: "BankId", principalTable: "BankAccounts", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_PaymentVouchers_Branches_BranchId", table: "PaymentVouchers", column: "BranchId", principalTable: "Branches", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_PaymentVouchers_Currencies_CurrencyId", table: "PaymentVouchers", column: "CurrencyId", principalTable: "Currencies", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_PaymentVouchers_EWallets_EWalletId", table: "PaymentVouchers", column: "EWalletId", principalTable: "EWallets", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_PaymentVouchers_Partners_PartnerId", table: "PaymentVouchers", column: "PartnerId", principalTable: "Partners", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_PaymentVouchers_Treasuries_TreasuryId", table: "PaymentVouchers", column: "TreasuryId", principalTable: "Treasuries", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_PaymentVouchers_Users_UserId", table: "PaymentVouchers", column: "UserId", principalTable: "Users", principalColumn: "Id", onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(name: "FK_Partners_Areas_AreaId", table: "Partners", column: "AreaId", principalTable: "Areas", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_Partners_Groups_GroupId", table: "Partners", column: "GroupId", principalTable: "Groups", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_Partners_Images_ImageId", table: "Partners", column: "ImageId", principalTable: "Images", principalColumn: "Id");

            migrationBuilder.AddForeignKey(name: "FK_PartnerLedgerEntries_Partners_PartnerId", table: "PartnerLedgerEntries", column: "PartnerId", principalTable: "Partners", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_PartnerLedgerEntries_Users_UserId", table: "PartnerLedgerEntries", column: "UserId", principalTable: "Users", principalColumn: "Id", onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(name: "FK_GeneralLedgerEntries_Accounts_AccountId", table: "GeneralLedgerEntries", column: "AccountId", principalTable: "Accounts", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_GeneralLedgerEntries_Branches_BranchId", table: "GeneralLedgerEntries", column: "BranchId", principalTable: "Branches", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_GeneralLedgerEntries_Partners_PartnerId", table: "GeneralLedgerEntries", column: "PartnerId", principalTable: "Partners", principalColumn: "Id");

            migrationBuilder.AddForeignKey(name: "FK_ExpenseIncomeVouchers_Accounts_AccountId", table: "ExpenseIncomeVouchers", column: "AccountId", principalTable: "Accounts", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_ExpenseIncomeVouchers_BankAccounts_BankId", table: "ExpenseIncomeVouchers", column: "BankId", principalTable: "BankAccounts", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_ExpenseIncomeVouchers_Branches_BranchId", table: "ExpenseIncomeVouchers", column: "BranchId", principalTable: "Branches", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_ExpenseIncomeVouchers_Currencies_CurrencyId", table: "ExpenseIncomeVouchers", column: "CurrencyId", principalTable: "Currencies", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_ExpenseIncomeVouchers_EWallets_EwalletId", table: "ExpenseIncomeVouchers", column: "EwalletId", principalTable: "EWallets", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_ExpenseIncomeVouchers_Treasuries_TreasuryId", table: "ExpenseIncomeVouchers", column: "TreasuryId", principalTable: "Treasuries", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_ExpenseIncomeVouchers_Users_UserId", table: "ExpenseIncomeVouchers", column: "UserId", principalTable: "Users", principalColumn: "Id", onDelete: ReferentialAction.Cascade);

            // Re-add FKs on tables that weren't renamed, now pointing at renamed principals
            migrationBuilder.AddForeignKey(name: "FK_BankTransactions_BankAccounts_BankId", table: "BankTransactions", column: "BankId", principalTable: "BankAccounts", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_BankTransactions_Partners_PartnerId", table: "BankTransactions", column: "PartnerId", principalTable: "Partners", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_EWalletTransactions_Partners_PartnerId", table: "EWalletTransactions", column: "PartnerId", principalTable: "Partners", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_InstallmentPlans_Partners_PartnerId", table: "InstallmentPlans", column: "PartnerId", principalTable: "Partners", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_InventoryMovements_Partners_PartnerId", table: "InventoryMovements", column: "PartnerId", principalTable: "Partners", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_Invoices_Branches_BranchId", table: "Invoices", column: "BranchId", principalTable: "Branches", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_Invoices_Partners_PartnerId", table: "Invoices", column: "PartnerId", principalTable: "Partners", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_Invoices_Users_UserId", table: "Invoices", column: "UserId", principalTable: "Users", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_OrderFulfillments_InvoiceLines_OrderLineId", table: "OrderFulfillments", column: "OrderLineId", principalTable: "InvoiceLines", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_OrderFulfillments_InvoiceLines_SaleLineId", table: "OrderFulfillments", column: "SaleLineId", principalTable: "InvoiceLines", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_OrderFulfillments_InvoiceLines_SalesReturnLineId", table: "OrderFulfillments", column: "SalesReturnLineId", principalTable: "InvoiceLines", principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop foreign keys owned by the renamed tables
            migrationBuilder.DropForeignKey(name: "FK_BankAccounts_Accounts_AccountId", table: "BankAccounts");
            migrationBuilder.DropForeignKey(name: "FK_BankAccounts_Currencies_CurrencyId", table: "BankAccounts");
            migrationBuilder.DropForeignKey(name: "FK_BankAccounts_Users_UserId", table: "BankAccounts");

            migrationBuilder.DropForeignKey(name: "FK_InvoiceLines_Branches_BranchId", table: "InvoiceLines");
            migrationBuilder.DropForeignKey(name: "FK_InvoiceLines_Invoices_InvoiceId", table: "InvoiceLines");
            migrationBuilder.DropForeignKey(name: "FK_InvoiceLines_Partners_PartnerId", table: "InvoiceLines");
            migrationBuilder.DropForeignKey(name: "FK_InvoiceLines_Products_ProductId", table: "InvoiceLines");
            migrationBuilder.DropForeignKey(name: "FK_InvoiceLines_UnitOfMeasures_UnitId", table: "InvoiceLines");
            migrationBuilder.DropForeignKey(name: "FK_InvoiceLines_Warehouses_WarehouseId", table: "InvoiceLines");

            migrationBuilder.DropForeignKey(name: "FK_InvoicePayments_Invoices_InvoiceId", table: "InvoicePayments");

            migrationBuilder.DropForeignKey(name: "FK_PaymentVouchers_BankAccounts_BankId", table: "PaymentVouchers");
            migrationBuilder.DropForeignKey(name: "FK_PaymentVouchers_Branches_BranchId", table: "PaymentVouchers");
            migrationBuilder.DropForeignKey(name: "FK_PaymentVouchers_Currencies_CurrencyId", table: "PaymentVouchers");
            migrationBuilder.DropForeignKey(name: "FK_PaymentVouchers_EWallets_EWalletId", table: "PaymentVouchers");
            migrationBuilder.DropForeignKey(name: "FK_PaymentVouchers_Partners_PartnerId", table: "PaymentVouchers");
            migrationBuilder.DropForeignKey(name: "FK_PaymentVouchers_Treasuries_TreasuryId", table: "PaymentVouchers");
            migrationBuilder.DropForeignKey(name: "FK_PaymentVouchers_Users_UserId", table: "PaymentVouchers");

            migrationBuilder.DropForeignKey(name: "FK_Partners_Areas_AreaId", table: "Partners");
            migrationBuilder.DropForeignKey(name: "FK_Partners_Groups_GroupId", table: "Partners");
            migrationBuilder.DropForeignKey(name: "FK_Partners_Images_ImageId", table: "Partners");

            migrationBuilder.DropForeignKey(name: "FK_PartnerLedgerEntries_Partners_PartnerId", table: "PartnerLedgerEntries");
            migrationBuilder.DropForeignKey(name: "FK_PartnerLedgerEntries_Users_UserId", table: "PartnerLedgerEntries");

            migrationBuilder.DropForeignKey(name: "FK_GeneralLedgerEntries_Accounts_AccountId", table: "GeneralLedgerEntries");
            migrationBuilder.DropForeignKey(name: "FK_GeneralLedgerEntries_Branches_BranchId", table: "GeneralLedgerEntries");
            migrationBuilder.DropForeignKey(name: "FK_GeneralLedgerEntries_Partners_PartnerId", table: "GeneralLedgerEntries");

            migrationBuilder.DropForeignKey(name: "FK_ExpenseIncomeVouchers_Accounts_AccountId", table: "ExpenseIncomeVouchers");
            migrationBuilder.DropForeignKey(name: "FK_ExpenseIncomeVouchers_BankAccounts_BankId", table: "ExpenseIncomeVouchers");
            migrationBuilder.DropForeignKey(name: "FK_ExpenseIncomeVouchers_Branches_BranchId", table: "ExpenseIncomeVouchers");
            migrationBuilder.DropForeignKey(name: "FK_ExpenseIncomeVouchers_Currencies_CurrencyId", table: "ExpenseIncomeVouchers");
            migrationBuilder.DropForeignKey(name: "FK_ExpenseIncomeVouchers_EWallets_EwalletId", table: "ExpenseIncomeVouchers");
            migrationBuilder.DropForeignKey(name: "FK_ExpenseIncomeVouchers_Treasuries_TreasuryId", table: "ExpenseIncomeVouchers");
            migrationBuilder.DropForeignKey(name: "FK_ExpenseIncomeVouchers_Users_UserId", table: "ExpenseIncomeVouchers");

            // Drop FKs on tables that weren't renamed
            migrationBuilder.DropForeignKey(name: "FK_BankTransactions_BankAccounts_BankId", table: "BankTransactions");
            migrationBuilder.DropForeignKey(name: "FK_BankTransactions_Partners_PartnerId", table: "BankTransactions");
            migrationBuilder.DropForeignKey(name: "FK_EWalletTransactions_Partners_PartnerId", table: "EWalletTransactions");
            migrationBuilder.DropForeignKey(name: "FK_InstallmentPlans_Partners_PartnerId", table: "InstallmentPlans");
            migrationBuilder.DropForeignKey(name: "FK_InventoryMovements_Partners_PartnerId", table: "InventoryMovements");
            migrationBuilder.DropForeignKey(name: "FK_Invoices_Branches_BranchId", table: "Invoices");
            migrationBuilder.DropForeignKey(name: "FK_Invoices_Partners_PartnerId", table: "Invoices");
            migrationBuilder.DropForeignKey(name: "FK_Invoices_Users_UserId", table: "Invoices");
            migrationBuilder.DropForeignKey(name: "FK_OrderFulfillments_InvoiceLines_OrderLineId", table: "OrderFulfillments");
            migrationBuilder.DropForeignKey(name: "FK_OrderFulfillments_InvoiceLines_SaleLineId", table: "OrderFulfillments");
            migrationBuilder.DropForeignKey(name: "FK_OrderFulfillments_InvoiceLines_SalesReturnLineId", table: "OrderFulfillments");

            // Drop primary keys about to be renamed back
            migrationBuilder.DropPrimaryKey(name: "PK_PostingAccounts", table: "PostingAccounts");
            migrationBuilder.DropPrimaryKey(name: "PK_BankAccounts", table: "BankAccounts");
            migrationBuilder.DropPrimaryKey(name: "PK_InvoiceLines", table: "InvoiceLines");
            migrationBuilder.DropPrimaryKey(name: "PK_InvoicePayments", table: "InvoicePayments");
            migrationBuilder.DropPrimaryKey(name: "PK_Invoices", table: "Invoices");
            migrationBuilder.DropPrimaryKey(name: "PK_PaymentVouchers", table: "PaymentVouchers");
            migrationBuilder.DropPrimaryKey(name: "PK_Partners", table: "Partners");
            migrationBuilder.DropPrimaryKey(name: "PK_PartnerLedgerEntries", table: "PartnerLedgerEntries");
            migrationBuilder.DropPrimaryKey(name: "PK_GeneralLedgerEntries", table: "GeneralLedgerEntries");
            migrationBuilder.DropPrimaryKey(name: "PK_ExpenseIncomeVouchers", table: "ExpenseIncomeVouchers");

            // Rename tables back
            migrationBuilder.RenameTable(name: "PostingAccounts", newName: "AccountOperands");
            migrationBuilder.RenameTable(name: "BankAccounts", newName: "Banks");
            migrationBuilder.RenameTable(name: "InvoiceLines", newName: "BillEntries");
            migrationBuilder.RenameTable(name: "InvoicePayments", newName: "BillPayments");
            migrationBuilder.RenameTable(name: "Invoices", newName: "Bills");
            migrationBuilder.RenameTable(name: "PaymentVouchers", newName: "Cashes");
            migrationBuilder.RenameTable(name: "Partners", newName: "Customers");
            migrationBuilder.RenameTable(name: "PartnerLedgerEntries", newName: "CustomerTransactions");
            migrationBuilder.RenameTable(name: "GeneralLedgerEntries", newName: "JournalEntries");
            migrationBuilder.RenameTable(name: "ExpenseIncomeVouchers", newName: "RevenueExpenses");

            // Rename columns back
            migrationBuilder.RenameColumn(name: "Role", table: "AccountOperands", newName: "Operand");

            migrationBuilder.RenameColumn(name: "InvoiceId", table: "BillEntries", newName: "BillId");
            migrationBuilder.RenameColumn(name: "PartnerId", table: "BillEntries", newName: "CustomerId");

            migrationBuilder.RenameColumn(name: "InvoiceId", table: "BillPayments", newName: "BillId");

            migrationBuilder.RenameColumn(name: "PartnerId", table: "Cashes", newName: "CustomerId");

            migrationBuilder.RenameColumn(name: "PartnerId", table: "CustomerTransactions", newName: "CustomerId");

            migrationBuilder.RenameColumn(name: "PartnerId", table: "JournalEntries", newName: "CustomerId");

            migrationBuilder.RenameColumn(name: "PartnerId", table: "Bills", newName: "CustomerId");

            migrationBuilder.RenameColumn(name: "PartnerId", table: "TreasuriesTransactions", newName: "CustomerId");
            migrationBuilder.RenameColumn(name: "PartnerId", table: "InventoryMovements", newName: "CustomerId");
            migrationBuilder.RenameColumn(name: "SourceInvoiceId", table: "InstallmentPlans", newName: "SourceBillId");
            migrationBuilder.RenameColumn(name: "PartnerId", table: "InstallmentPlans", newName: "CustomerId");
            migrationBuilder.RenameColumn(name: "PartnerId", table: "EWalletTransactions", newName: "CustomerId");
            migrationBuilder.RenameColumn(name: "PartnerId", table: "BankTransactions", newName: "CustomerId");

            migrationBuilder.RenameColumn(name: "SaleLineId", table: "OrderFulfillments", newName: "SaleEntryId");
            migrationBuilder.RenameColumn(name: "SalesReturnLineId", table: "OrderFulfillments", newName: "ReSaleEntryId");
            migrationBuilder.RenameColumn(name: "OrderLineId", table: "OrderFulfillments", newName: "OrderEntryId");

            // Rename indexes back
            migrationBuilder.RenameIndex(name: "IX_BankAccounts_AccountId", table: "Banks", newName: "IX_Banks_AccountId");
            migrationBuilder.RenameIndex(name: "IX_BankAccounts_CurrencyId", table: "Banks", newName: "IX_Banks_CurrencyId");
            migrationBuilder.RenameIndex(name: "IX_BankAccounts_UserId", table: "Banks", newName: "IX_Banks_UserId");

            migrationBuilder.RenameIndex(name: "IX_InvoiceLines_InvoiceId", table: "BillEntries", newName: "IX_BillEntries_BillId");
            migrationBuilder.RenameIndex(name: "IX_InvoiceLines_BranchId", table: "BillEntries", newName: "IX_BillEntries_BranchId");
            migrationBuilder.RenameIndex(name: "IX_InvoiceLines_PartnerId", table: "BillEntries", newName: "IX_BillEntries_CustomerId");
            migrationBuilder.RenameIndex(name: "IX_InvoiceLines_ProductId", table: "BillEntries", newName: "IX_BillEntries_ProductId");
            migrationBuilder.RenameIndex(name: "IX_InvoiceLines_UnitId", table: "BillEntries", newName: "IX_BillEntries_UnitId");
            migrationBuilder.RenameIndex(name: "IX_InvoiceLines_WarehouseId", table: "BillEntries", newName: "IX_BillEntries_WarehouseId");

            migrationBuilder.RenameIndex(name: "IX_InvoicePayments_InvoiceId", table: "BillPayments", newName: "IX_BillPayments_BillId");
            migrationBuilder.RenameIndex(name: "IX_InvoicePayments_ShiftId", table: "BillPayments", newName: "IX_BillPayments_ShiftId");

            migrationBuilder.RenameIndex(name: "IX_PaymentVouchers_BankId", table: "Cashes", newName: "IX_Cashes_BankId");
            migrationBuilder.RenameIndex(name: "IX_PaymentVouchers_BranchId", table: "Cashes", newName: "IX_Cashes_BranchId");
            migrationBuilder.RenameIndex(name: "IX_PaymentVouchers_CurrencyId", table: "Cashes", newName: "IX_Cashes_CurrencyId");
            migrationBuilder.RenameIndex(name: "IX_PaymentVouchers_PartnerId", table: "Cashes", newName: "IX_Cashes_CustomerId");
            migrationBuilder.RenameIndex(name: "IX_PaymentVouchers_EWalletId", table: "Cashes", newName: "IX_Cashes_EWalletId");
            migrationBuilder.RenameIndex(name: "IX_PaymentVouchers_TreasuryId", table: "Cashes", newName: "IX_Cashes_TreasuryId");
            migrationBuilder.RenameIndex(name: "IX_PaymentVouchers_UserId", table: "Cashes", newName: "IX_Cashes_UserId");

            migrationBuilder.RenameIndex(name: "IX_Partners_AreaId", table: "Customers", newName: "IX_Customers_AreaId");
            migrationBuilder.RenameIndex(name: "IX_Partners_GroupId", table: "Customers", newName: "IX_Customers_GroupId");
            migrationBuilder.RenameIndex(name: "IX_Partners_ImageId", table: "Customers", newName: "IX_Customers_ImageId");

            migrationBuilder.RenameIndex(name: "IX_PartnerLedgerEntries_PartnerId", table: "CustomerTransactions", newName: "IX_CustomerTransactions_CustomerId");
            migrationBuilder.RenameIndex(name: "IX_PartnerLedgerEntries_ShiftId", table: "CustomerTransactions", newName: "IX_CustomerTransactions_ShiftId");
            migrationBuilder.RenameIndex(name: "IX_PartnerLedgerEntries_UserId", table: "CustomerTransactions", newName: "IX_CustomerTransactions_UserId");

            migrationBuilder.RenameIndex(name: "IX_GeneralLedgerEntries_AccountId", table: "JournalEntries", newName: "IX_JournalEntries_AccountId");
            migrationBuilder.RenameIndex(name: "IX_GeneralLedgerEntries_BranchId", table: "JournalEntries", newName: "IX_JournalEntries_BranchId");
            migrationBuilder.RenameIndex(name: "IX_GeneralLedgerEntries_PartnerId", table: "JournalEntries", newName: "IX_JournalEntries_CustomerId");

            migrationBuilder.RenameIndex(name: "IX_ExpenseIncomeVouchers_AccountId", table: "RevenueExpenses", newName: "IX_RevenueExpenses_AccountId");
            migrationBuilder.RenameIndex(name: "IX_ExpenseIncomeVouchers_BankId", table: "RevenueExpenses", newName: "IX_RevenueExpenses_BankId");
            migrationBuilder.RenameIndex(name: "IX_ExpenseIncomeVouchers_BranchId", table: "RevenueExpenses", newName: "IX_RevenueExpenses_BranchId");
            migrationBuilder.RenameIndex(name: "IX_ExpenseIncomeVouchers_CurrencyId", table: "RevenueExpenses", newName: "IX_RevenueExpenses_CurrencyId");
            migrationBuilder.RenameIndex(name: "IX_ExpenseIncomeVouchers_EwalletId", table: "RevenueExpenses", newName: "IX_RevenueExpenses_EwalletId");
            migrationBuilder.RenameIndex(name: "IX_ExpenseIncomeVouchers_ShiftId", table: "RevenueExpenses", newName: "IX_RevenueExpenses_ShiftId");
            migrationBuilder.RenameIndex(name: "IX_ExpenseIncomeVouchers_TreasuryId", table: "RevenueExpenses", newName: "IX_RevenueExpenses_TreasuryId");
            migrationBuilder.RenameIndex(name: "IX_ExpenseIncomeVouchers_UserId", table: "RevenueExpenses", newName: "IX_RevenueExpenses_UserId");

            migrationBuilder.RenameIndex(name: "IX_OrderFulfillments_SaleLineId", table: "OrderFulfillments", newName: "IX_OrderFulfillments_SaleEntryId");
            migrationBuilder.RenameIndex(name: "IX_OrderFulfillments_SalesReturnLineId", table: "OrderFulfillments", newName: "IX_OrderFulfillments_ReSaleEntryId");
            migrationBuilder.RenameIndex(name: "IX_OrderFulfillments_OrderLineId", table: "OrderFulfillments", newName: "IX_OrderFulfillments_OrderEntryId");

            migrationBuilder.RenameIndex(name: "IX_InstallmentPlans_PartnerId", table: "InstallmentPlans", newName: "IX_InstallmentPlans_CustomerId");
            migrationBuilder.RenameIndex(name: "IX_InventoryMovements_PartnerId", table: "InventoryMovements", newName: "IX_InventoryMovements_CustomerId");
            migrationBuilder.RenameIndex(name: "IX_EWalletTransactions_PartnerId", table: "EWalletTransactions", newName: "IX_EWalletTransactions_CustomerId");
            migrationBuilder.RenameIndex(name: "IX_BankTransactions_PartnerId", table: "BankTransactions", newName: "IX_BankTransactions_CustomerId");
            migrationBuilder.RenameIndex(name: "IX_Invoices_UserId", table: "Bills", newName: "IX_Bills_UserId");
            migrationBuilder.RenameIndex(name: "IX_Invoices_ShiftId", table: "Bills", newName: "IX_Bills_ShiftId");
            migrationBuilder.RenameIndex(name: "IX_Invoices_PartnerId", table: "Bills", newName: "IX_Bills_CustomerId");
            migrationBuilder.RenameIndex(name: "IX_Invoices_BranchId", table: "Bills", newName: "IX_Bills_BranchId");

            // Re-add primary keys with old names
            migrationBuilder.AddPrimaryKey(name: "PK_AccountOperands", table: "AccountOperands", column: "Operand");
            migrationBuilder.AddPrimaryKey(name: "PK_Banks", table: "Banks", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_BillEntries", table: "BillEntries", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_BillPayments", table: "BillPayments", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_Bills", table: "Bills", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_Cashes", table: "Cashes", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_Customers", table: "Customers", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_CustomerTransactions", table: "CustomerTransactions", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_JournalEntries", table: "JournalEntries", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_RevenueExpenses", table: "RevenueExpenses", column: "Id");

            // Re-add foreign keys with old names
            migrationBuilder.AddForeignKey(name: "FK_Banks_Accounts_AccountId", table: "Banks", column: "AccountId", principalTable: "Accounts", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_Banks_Currencies_CurrencyId", table: "Banks", column: "CurrencyId", principalTable: "Currencies", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_Banks_Users_UserId", table: "Banks", column: "UserId", principalTable: "Users", principalColumn: "Id", onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(name: "FK_BillEntries_Bills_BillId", table: "BillEntries", column: "BillId", principalTable: "Bills", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_BillEntries_Branches_BranchId", table: "BillEntries", column: "BranchId", principalTable: "Branches", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_BillEntries_Customers_CustomerId", table: "BillEntries", column: "CustomerId", principalTable: "Customers", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_BillEntries_Products_ProductId", table: "BillEntries", column: "ProductId", principalTable: "Products", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_BillEntries_UnitOfMeasures_UnitId", table: "BillEntries", column: "UnitId", principalTable: "UnitOfMeasures", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_BillEntries_Warehouses_WarehouseId", table: "BillEntries", column: "WarehouseId", principalTable: "Warehouses", principalColumn: "Id", onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(name: "FK_BillPayments_Bills_BillId", table: "BillPayments", column: "BillId", principalTable: "Bills", principalColumn: "Id", onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(name: "FK_Cashes_Banks_BankId", table: "Cashes", column: "BankId", principalTable: "Banks", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_Cashes_Branches_BranchId", table: "Cashes", column: "BranchId", principalTable: "Branches", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_Cashes_Currencies_CurrencyId", table: "Cashes", column: "CurrencyId", principalTable: "Currencies", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_Cashes_Customers_CustomerId", table: "Cashes", column: "CustomerId", principalTable: "Customers", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_Cashes_EWallets_EWalletId", table: "Cashes", column: "EWalletId", principalTable: "EWallets", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_Cashes_Treasuries_TreasuryId", table: "Cashes", column: "TreasuryId", principalTable: "Treasuries", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_Cashes_Users_UserId", table: "Cashes", column: "UserId", principalTable: "Users", principalColumn: "Id", onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(name: "FK_Customers_Areas_AreaId", table: "Customers", column: "AreaId", principalTable: "Areas", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_Customers_Groups_GroupId", table: "Customers", column: "GroupId", principalTable: "Groups", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_Customers_Images_ImageId", table: "Customers", column: "ImageId", principalTable: "Images", principalColumn: "Id");

            migrationBuilder.AddForeignKey(name: "FK_CustomerTransactions_Customers_CustomerId", table: "CustomerTransactions", column: "CustomerId", principalTable: "Customers", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_CustomerTransactions_Users_UserId", table: "CustomerTransactions", column: "UserId", principalTable: "Users", principalColumn: "Id", onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(name: "FK_JournalEntries_Accounts_AccountId", table: "JournalEntries", column: "AccountId", principalTable: "Accounts", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_JournalEntries_Branches_BranchId", table: "JournalEntries", column: "BranchId", principalTable: "Branches", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_JournalEntries_Customers_CustomerId", table: "JournalEntries", column: "CustomerId", principalTable: "Customers", principalColumn: "Id");

            migrationBuilder.AddForeignKey(name: "FK_RevenueExpenses_Accounts_AccountId", table: "RevenueExpenses", column: "AccountId", principalTable: "Accounts", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_RevenueExpenses_Banks_BankId", table: "RevenueExpenses", column: "BankId", principalTable: "Banks", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_RevenueExpenses_Branches_BranchId", table: "RevenueExpenses", column: "BranchId", principalTable: "Branches", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_RevenueExpenses_Currencies_CurrencyId", table: "RevenueExpenses", column: "CurrencyId", principalTable: "Currencies", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_RevenueExpenses_EWallets_EwalletId", table: "RevenueExpenses", column: "EwalletId", principalTable: "EWallets", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_RevenueExpenses_Treasuries_TreasuryId", table: "RevenueExpenses", column: "TreasuryId", principalTable: "Treasuries", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_RevenueExpenses_Users_UserId", table: "RevenueExpenses", column: "UserId", principalTable: "Users", principalColumn: "Id", onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(name: "FK_BankTransactions_Banks_BankId", table: "BankTransactions", column: "BankId", principalTable: "Banks", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_BankTransactions_Customers_CustomerId", table: "BankTransactions", column: "CustomerId", principalTable: "Customers", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_Bills_Branches_BranchId", table: "Bills", column: "BranchId", principalTable: "Branches", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_Bills_Customers_CustomerId", table: "Bills", column: "CustomerId", principalTable: "Customers", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_Bills_Users_UserId", table: "Bills", column: "UserId", principalTable: "Users", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_EWalletTransactions_Customers_CustomerId", table: "EWalletTransactions", column: "CustomerId", principalTable: "Customers", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_InstallmentPlans_Customers_CustomerId", table: "InstallmentPlans", column: "CustomerId", principalTable: "Customers", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_InventoryMovements_Customers_CustomerId", table: "InventoryMovements", column: "CustomerId", principalTable: "Customers", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_OrderFulfillments_BillEntries_OrderEntryId", table: "OrderFulfillments", column: "OrderEntryId", principalTable: "BillEntries", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_OrderFulfillments_BillEntries_ReSaleEntryId", table: "OrderFulfillments", column: "ReSaleEntryId", principalTable: "BillEntries", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_OrderFulfillments_BillEntries_SaleEntryId", table: "OrderFulfillments", column: "SaleEntryId", principalTable: "BillEntries", principalColumn: "Id");
        }
    }
}
