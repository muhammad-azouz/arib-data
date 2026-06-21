using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountOperands",
                columns: table => new
                {
                    Operand = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LabelAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LabelEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountOperands", x => x.Operand);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RootId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Num = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsParent = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Class = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Village = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    ArabicName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Num = table.Column<int>(type: "int", nullable: false),
                    Kind = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    ProductCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", maxLength: 2097152, nullable: false),
                    Kind = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductDefaults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductKind = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Buy = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Sale = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Order = table.Column<double>(type: "float", nullable: false),
                    ReOrder = table.Column<double>(type: "float", nullable: false),
                    RecessionPeriod = table.Column<double>(type: "float", nullable: false),
                    ExpirationDate = table.Column<double>(type: "float", nullable: false),
                    StIdSale = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StIdSaleCost = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StIdStock = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SrAccount = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SrIdSaleCost = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SrIdSale = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDefaults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductKind = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Vendor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ReOrder = table.Column<double>(type: "float", nullable: false),
                    MaxOrder = table.Column<double>(type: "float", nullable: false),
                    TargetSales = table.Column<double>(type: "float", nullable: false),
                    IsExpire = table.Column<bool>(type: "bit", nullable: false),
                    SalesAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesCostAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryValuationMethod = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Field = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone1 = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Phone2 = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Phone3 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Street = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EMail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Num = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    LogoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxCard = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CommercialRegister = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Companies_Images_LogoId",
                        column: x => x.LogoId,
                        principalTable: "Images",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Num = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Phone1 = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Phone2 = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Phone3 = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Company = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDoubleType = table.Column<bool>(type: "bit", nullable: false),
                    PriceTier = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BankBrunch = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreditLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OpenBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsCredit = table.Column<bool>(type: "bit", nullable: false),
                    RegNum = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FromId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AreaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebSite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ToId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegNum = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OpenBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banks_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Banks_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Banks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitOfMeasures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValSub = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MasterBuy = table.Column<bool>(type: "bit", nullable: false),
                    MasterSale = table.Column<bool>(type: "bit", nullable: false),
                    Buy = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Sale = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Price1 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Price2 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Price3 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Price4 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Price5 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Price6 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Price7 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Price8 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Price9 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitOfMeasures_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branches_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Dealing = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Pay = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Remain = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Extra = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    RegNum = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerTransactions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerTransactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Dealing = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegNum = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyVal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ship = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankTransactions_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankTransactions_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankTransactions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BankTransactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Barcodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitOfMeasureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barcodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Barcodes_UnitOfMeasures_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalTable: "UnitOfMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Num = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    warehouse = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ship = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShipAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShipPhone1 = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    ShipPhone2 = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    ItemTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BillDiscount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BillDiscountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemDiscount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ItemDiscountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillTax = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BillTaxPercentage = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BillTaxId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Money = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MoneyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Remain = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalMoney = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BillExtraId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalExtra = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalDiscount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RegNum = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemCount = table.Column<int>(type: "int", nullable: false),
                    IsCash = table.Column<bool>(type: "bit", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaidValue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    PaidDiscount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    MoneyTotalPaid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    PaidRegNum = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InternalNote = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bills_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EWallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OpenBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComplementaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegNum = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EWallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EWallets_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EWallets_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryAdjustments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Num = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NowQty = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    NowPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NewQty = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    NewPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DiffQty = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    DiffVal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegNum = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UniteCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ExprDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ItemCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryAdjustments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryAdjustments_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Dealing = table.Column<int>(type: "int", nullable: false),
                    RegNum = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Ship = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalEntries_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JournalEntries_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JournalEntries_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Treasuries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpeningBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CloseAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treasuries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Treasuries_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Num = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProductsCount = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EWalletTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EWalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Dealing = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegNum = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ship = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EWalletTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EWalletTransactions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EWalletTransactions_EWallets_EWalletId",
                        column: x => x.EWalletId,
                        principalTable: "EWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EWalletTransactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cashes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Num = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyVal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegNum = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Dealing = table.Column<int>(type: "int", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EWalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TreasuryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cashes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cashes_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cashes_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cashes_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cashes_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cashes_EWallets_EWalletId",
                        column: x => x.EWalletId,
                        principalTable: "EWallets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cashes_Treasuries_TreasuryId",
                        column: x => x.TreasuryId,
                        principalTable: "Treasuries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cashes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RevenueExpenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dealing = table.Column<int>(type: "int", nullable: false),
                    Num = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreasuryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EwalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RegNum = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkId = table.Column<int>(type: "int", nullable: true),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyVal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RevenueExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RevenueExpenses_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RevenueExpenses_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RevenueExpenses_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RevenueExpenses_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RevenueExpenses_EWallets_EwalletId",
                        column: x => x.EwalletId,
                        principalTable: "EWallets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RevenueExpenses_Treasuries_TreasuryId",
                        column: x => x.TreasuryId,
                        principalTable: "Treasuries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RevenueExpenses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TreasuriesTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreasuryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Num = table.Column<int>(type: "int", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Dealing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deal = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RegNum = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreasuriesTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreasuriesTransactions_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TreasuriesTransactions_Treasuries_TreasuryId",
                        column: x => x.TreasuryId,
                        principalTable: "Treasuries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TreasuriesTransactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Num = table.Column<int>(type: "int", nullable: false),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalQty = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RegNum = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    ItemCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    FromOrder = table.Column<bool>(type: "bit", nullable: false),
                    ExpireDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillEntries_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillEntries_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillEntries_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BillEntries_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillEntries_UnitOfMeasures_UnitId",
                        column: x => x.UnitId,
                        principalTable: "UnitOfMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillEntries_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DailyProductCosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BatchNumber = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                name: "ProductOpeningBalances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RegNum = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOpeningBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOpeningBalances_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductOpeningBalances_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOpeningBalances_Warehouses_WarehouseId",
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

            migrationBuilder.CreateTable(
                name: "WarehousesProductInventories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalQty = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    LastInPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    LastInQty = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    LastInDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastOutPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    LastOutQty = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    LastOutDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehousesProductInventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehousesProductInventories_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WarehousesProductInventories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehousesProductInventories_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderFulfillments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderEntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaleEntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReSaleEntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Qty = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FulfilledAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FulfilledByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderFulfillments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderFulfillments_BillEntries_OrderEntryId",
                        column: x => x.OrderEntryId,
                        principalTable: "BillEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderFulfillments_BillEntries_ReSaleEntryId",
                        column: x => x.ReSaleEntryId,
                        principalTable: "BillEntries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderFulfillments_BillEntries_SaleEntryId",
                        column: x => x.SaleEntryId,
                        principalTable: "BillEntries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderFulfillments_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AccountOperands",
                columns: new[] { "Operand", "AccountId", "LabelAr", "LabelEn" },
                values: new object[,]
                {
                    { "AdvancesToEmployees", new Guid("00000001-0000-7000-a000-000000000061"), "سلف الموظفين", "Advances To Employees" },
                    { "Asset", new Guid("00000001-0000-7000-a000-000000000001"), "الاصول", "Asset" },
                    { "BadDebit", new Guid("00000001-0000-7000-a000-000000000195"), "ديون معدومة", "Bad Debit" },
                    { "Bank", new Guid("00000001-0000-7000-a000-000000000034"), "البنوك", "Banks" },
                    { "BankExpenses", new Guid("00000001-0000-7000-a000-000000000181"), "مصروفات البنوك", "BankExpenses" },
                    { "BillPurchaseExtra", new Guid("00000001-0000-7000-a000-000000000156"), "خدمات مشتراة", "Extra on Purchase" },
                    { "BillSaleExtra", new Guid("00000001-0000-7000-a000-000000000218"), "اضافى على الفاتورة", "Extra on Bill" },
                    { "Capital", new Guid("00000001-0000-7000-a000-000000000126"), "راس المال", "Capital" },
                    { "CashBranchesTransfer", new Guid("00000001-0000-7000-a000-000000000062"), "تحويلات النقديه بين الفروع", "Cash Branches Transfer" },
                    { "CashDiscountIn", new Guid("00000001-0000-7000-a000-000000000217"), "خصم مكتسب (المشتريات)", "Cash Discount In" },
                    { "CashDiscountOut", new Guid("00000001-0000-7000-a000-000000001245"), "خصم مسموح به ( مبيعات )", "Cash Discount Out" },
                    { "Customers", new Guid("00000001-0000-7000-a000-000000000052"), "العملاء", "Customers" },
                    { "CustomersAdjustment", new Guid("00000001-0000-7000-a000-000000000205"), "تسويات ماليه", "Customers Adjustment" },
                    { "DeliveryIncome", new Guid("00000001-0000-7000-a000-000000004268"), "التوصيل للمنازل", "Delivery Income" },
                    { "DueRevenue", new Guid("00000001-0000-7000-a000-000000000059"), "إيرادات مستحقة", "DueRevenue" },
                    { "EarnedProfit", new Guid("00000001-0000-7000-a000-000000004266"), "أرباح محققة", "EarnedProfit" },
                    { "EWallet", new Guid("00000001-0000-7000-a000-000000004270"), "المحافظ الاليكترونيه", "EWallets" },
                    { "FixedAssetsDepreciation", new Guid("00000001-0000-7000-a000-000000000164"), "إهلاك الأصول الثابتة", "Fixed Assets Depreciation" },
                    { "Gifts", new Guid("00000001-0000-7000-a000-000000000173"), "هدايا", "Gifts" },
                    { "Incentive", new Guid("00000000-0000-0000-0000-000000000000"), "حوافذ", "Incentive" },
                    { "Insurance", new Guid("00000001-0000-7000-a000-000000000096"), "التأمينات الاجتماعية", "Insurance" },
                    { "InventoryAdjustmentDifferences", new Guid("00000001-0000-7000-a000-000000001252"), "تسويه جرديه", "Inventory Adjustment" },
                    { "MaintenanceAndRepairExpenses", new Guid("00000001-0000-7000-a000-000000000189"), "مصروفات صيانة واصلاح", "" },
                    { "OpeningBalances", new Guid("00000001-0000-7000-a000-000000004263"), "ارصدة افتتاحيه", "Opening Balances" },
                    { "Pledge", new Guid("00000001-0000-7000-a000-000000000035"), "عهد الموظفين", "Pledge" },
                    { "QtyDiscountIn", new Guid("00000001-0000-7000-a000-000000001243"), "خصم كميه مكتسب", "Qty Discount In" },
                    { "QtyDiscountOut", new Guid("00000001-0000-7000-a000-000000001244"), "خصم كمية مسموح به", "Qty Discount Out" },
                    { "Sadka", new Guid("00000001-0000-7000-a000-000000000201"), "الصدقه", "Sadka" },
                    { "Sale", new Guid("00000001-0000-7000-a000-000000000210"), "المبيعات", "Sale" },
                    { "SaleCost", new Guid("00000001-0000-7000-a000-000000000137"), "تكلفة المبيعات", "Sale Cost" },
                    { "SaleReturn", new Guid("00000001-0000-7000-a000-000000000211"), "مرتجع المبيعات", "SaleReturn" },
                    { "Shortage", new Guid("00000001-0000-7000-a000-000000003264"), "عجز في إغلاق اليومية", "Shortage" },
                    { "Stock", new Guid("00000001-0000-7000-a000-000000000039"), "مخزون البضاعة", "Stock" },
                    { "Surplus", new Guid("00000001-0000-7000-a000-000000003263"), "فائض إغلاق اليومية", "Surplus" },
                    { "Tax", new Guid("00000001-0000-7000-a000-000000000093"), "الضريبة", "Tax" },
                    { "Treasury", new Guid("00000001-0000-7000-a000-000000000033"), "الخزينة", "Treasury" },
                    { "Vendor", new Guid("00000001-0000-7000-a000-000000000084"), "الموردون", "Vendor" },
                    { "WarehouseTransfer", new Guid("00000001-0000-7000-a000-000000003262"), "تحويلات المخازن", "Warehouse Transfer" },
                    { "WasteStock", new Guid("00000001-0000-7000-a000-000000000174"), "هالك المخزون", "Waste Stock" },
                    { "WithholdingTax", new Guid("00000001-0000-7000-a000-000000004264"), "ضريبة الخصم و الإضافة", "Withholding Tax" },
                    { "WorkSalary", new Guid("00000001-0000-7000-a000-000000000150"), "أجور نقدية", "Work Salary" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "BranchId", "Class", "CreatedAt", "Currency", "IsActive", "IsParent", "NameAr", "NameEn", "Note", "Num", "ParentId", "RootId", "Type", "TypeId" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-7000-a000-000000000001"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "الأصول", "Assets", "", 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 1 },
                    { new Guid("00000001-0000-7000-a000-000000000002"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "الأصول طويلة الأجل", "Long Term Assets", "", 11, new Guid("00000001-0000-7000-a000-000000000001"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 11 },
                    { new Guid("00000001-0000-7000-a000-000000000003"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "أصول ثابتة", "Fixed Assets", "", 111, new Guid("00000001-0000-7000-a000-000000000002"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 11 },
                    { new Guid("00000001-0000-7000-a000-000000000004"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "أراضي", "Land", "", 1111, new Guid("00000001-0000-7000-a000-000000000003"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 11 },
                    { new Guid("00000001-0000-7000-a000-000000000005"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مباني وإنشاءات ومرافق وطرق", "Buildings, Constructions, Facilities and roads", "", 1112, new Guid("00000001-0000-7000-a000-000000000003"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 11 },
                    { new Guid("00000001-0000-7000-a000-000000000006"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "آلات ومعدات", "Machinery and equipments", "", 1113, new Guid("00000001-0000-7000-a000-000000000003"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 11 },
                    { new Guid("00000001-0000-7000-a000-000000000007"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "وسائل نقل وانتقال", "Transportation", "", 1114, new Guid("00000001-0000-7000-a000-000000000003"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 11 },
                    { new Guid("00000001-0000-7000-a000-000000000008"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "عدد وأدوات", "Tools & Equipments", "", 1115, new Guid("00000001-0000-7000-a000-000000000003"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 11 },
                    { new Guid("00000001-0000-7000-a000-000000000009"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "ثروة حيوانية ومائية", "Animal & Water wealth", "", 1116, new Guid("00000001-0000-7000-a000-000000000003"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 11 },
                    { new Guid("00000001-0000-7000-a000-000000000010"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "مشروعات تحت التنفيذ", "Ongoing Projects", "", 112, new Guid("00000001-0000-7000-a000-000000000002"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 11 },
                    { new Guid("00000001-0000-7000-a000-000000000018"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "إنفاق استثماري", "Investment expenditure", "", 1128, new Guid("00000001-0000-7000-a000-000000000010"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 11 },
                    { new Guid("00000001-0000-7000-a000-000000000019"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "دفعات مقدمة", "Advance payments", "", 11281, new Guid("00000001-0000-7000-a000-000000000018"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 11 },
                    { new Guid("00000001-0000-7000-a000-000000000020"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "اعتمادات مستنديه لشراء أصول ثابتة", "LC for Fixed assets purchasing", "", 11282, new Guid("00000001-0000-7000-a000-000000000018"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 11 },
                    { new Guid("00000001-0000-7000-a000-000000000021"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "استثمارات طويلة الأجل", "Long term investments", "", 113, new Guid("00000001-0000-7000-a000-000000000002"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 11 },
                    { new Guid("00000001-0000-7000-a000-000000000022"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "استثمارات عقارية", "Real State Investment", "", 1131, new Guid("00000001-0000-7000-a000-000000000021"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 11 },
                    { new Guid("00000001-0000-7000-a000-000000000025"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "استثمارات في أسهم في شركات أخرى", "Shares Investments in other companies", "", 1134, new Guid("00000001-0000-7000-a000-000000000021"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 11 },
                    { new Guid("00000001-0000-7000-a000-000000000026"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "استثمارات في سندات", "Bond Investments", "", 1135, new Guid("00000001-0000-7000-a000-000000000021"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 11 },
                    { new Guid("00000001-0000-7000-a000-000000000027"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "استثمارات في وثائق استثمار", "Investments in Investment documents", "", 1136, new Guid("00000001-0000-7000-a000-000000000021"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 11 },
                    { new Guid("00000001-0000-7000-a000-000000000031"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "الأصول المتداولة", "Current Assets", "", 12, new Guid("00000001-0000-7000-a000-000000000001"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000000032"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "نقدية بالخزينة و البنوك", "Cash in Hand and at Bank", "", 121, new Guid("00000001-0000-7000-a000-000000000031"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 12 },
                    { new Guid("00000001-0000-7000-a000-000000000033"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "الخزينة", "Treasury", "", 1211, new Guid("00000001-0000-7000-a000-000000000032"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 12 },
                    { new Guid("00000001-0000-7000-a000-000000000034"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "البنوك", "Banks", "", 1212, new Guid("00000001-0000-7000-a000-000000000032"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 12 },
                    { new Guid("00000001-0000-7000-a000-000000000035"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "عهد الموظفين", "Employees Custody", "", 1213, new Guid("00000001-0000-7000-a000-000000000032"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 12 },
                    { new Guid("00000001-0000-7000-a000-000000000036"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "ودائع بالبنوك لأجل أو بإخطار سابق", "Bank term and Notice Deposits", "", 1214, new Guid("00000001-0000-7000-a000-000000000032"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 12 },
                    { new Guid("00000001-0000-7000-a000-000000000037"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "غطاء حسابات ضمان", "Escrow Account Guarantee", "", 1215, new Guid("00000001-0000-7000-a000-000000000032"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 12 },
                    { new Guid("00000001-0000-7000-a000-000000000038"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "مخزون", "Stock", "", 122, new Guid("00000001-0000-7000-a000-000000000031"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000000039"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخزون البضاعة", "Goods Stock", "", 1221, new Guid("00000001-0000-7000-a000-000000000038"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000000040"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخزون إنتاج غير تام", "Stock of Unfinished Goods", "", 1222, new Guid("00000001-0000-7000-a000-000000000038"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000000041"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخزون إنتاج تام", "Stock of Finished Goods", "", 1223, new Guid("00000001-0000-7000-a000-000000000038"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000000042"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخزون لدى الغير", "Stock in External Warehouses", "", 1224, new Guid("00000001-0000-7000-a000-000000000038"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000000043"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "اعتمادات مستنديه لشراء سلع وخدمات", "LC to Purchase Goods and Services", "", 1225, new Guid("00000001-0000-7000-a000-000000000038"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000000044"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخزن خامات ومواد ووقود وقطع غيار", "Raw materials, Materials, Fuel and Spare Parts Warehouse", "", 1226, new Guid("00000001-0000-7000-a000-000000000038"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000000051"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "حسابات المدينون", "Accounts Receivable", "", 123, new Guid("00000001-0000-7000-a000-000000000031"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 13 },
                    { new Guid("00000001-0000-7000-a000-000000000052"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "العملاء", "Clients", "", 1231, new Guid("00000001-0000-7000-a000-000000000051"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 13 },
                    { new Guid("00000001-0000-7000-a000-000000000053"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "أوراق قبض", "Notes Receivable", "", 1232, new Guid("00000001-0000-7000-a000-000000000051"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 13 },
                    { new Guid("00000001-0000-7000-a000-000000000054"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "أوراق تحت التحصيل", "Receivables", "", 1233, new Guid("00000001-0000-7000-a000-000000000051"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 13 },
                    { new Guid("00000001-0000-7000-a000-000000000055"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "حسابات مدينة لدى المصالح والهيئات", "Accounts Receivable of Authorities and Bodies", "", 124, new Guid("00000001-0000-7000-a000-000000000031"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000000056"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مصلحة الجمارك (أمانات)", "Customs Authority (deposits)", "", 1241, new Guid("00000001-0000-7000-a000-000000000055"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000000057"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "الضرائب", "Taxes", "", 1242, new Guid("00000001-0000-7000-a000-000000000055"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000000058"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مصلحة الضرائب العامة (مبالغ مخصومة من الشركة بمعرفة الغير)", "General Customs Authority (third party deductions)", "", 1243, new Guid("00000001-0000-7000-a000-000000000055"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000000059"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "إيرادات مستحقة التحصيل", "Accrued Revenues", "", 127, new Guid("00000001-0000-7000-a000-000000000031"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000000060"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مصروفات مدفوعة مقدما", "prepaid expenses", "", 126, new Guid("00000001-0000-7000-a000-000000000031"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000000061"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "سلف الموظفين", "Advances to Employees ", "", 1251, new Guid("00000001-0000-7000-a000-000000001250"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000000062"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "تحويلات النقدية بين الفروع", "Cash Transfer between Branches", "", 128, new Guid("00000001-0000-7000-a000-000000000031"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000000063"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "استثمارات وأوراق مالية متداولة :", "Current Investments and Securities", "", 129, new Guid("00000001-0000-7000-a000-000000000031"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000000064"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "اسهم", "Stocks", "", 1291, new Guid("00000001-0000-7000-a000-000000000063"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000000065"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "سندات استثمار", "investment bonds", "", 1292, new Guid("00000001-0000-7000-a000-000000000063"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000000066"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "وثائق استثمار", "Investment documents", "", 1293, new Guid("00000001-0000-7000-a000-000000000063"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000000067"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "أذون خزانه", "Treasury bills", "", 1294, new Guid("00000001-0000-7000-a000-000000000063"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000000068"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "أصول أخرى", "Other Assets", "", 13, new Guid("00000001-0000-7000-a000-000000000001"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 15 },
                    { new Guid("00000001-0000-7000-a000-000000000069"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "أصول غير ملموسة", "Intangible assets", "", 131, new Guid("00000001-0000-7000-a000-000000000068"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 15 },
                    { new Guid("00000001-0000-7000-a000-000000000070"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "شهرة", "Goodwill", "", 1311, new Guid("00000001-0000-7000-a000-000000000069"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 15 },
                    { new Guid("00000001-0000-7000-a000-000000000071"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "براءات اختراع/ علامات تجارية/ حقوق امتياز وتأليف", "Patents/Trademarks/Franchise Rights and Authorship", "", 1312, new Guid("00000001-0000-7000-a000-000000000069"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 15 },
                    { new Guid("00000001-0000-7000-a000-000000000072"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "تكاليف التطوير", "Development cost", "", 1313, new Guid("00000001-0000-7000-a000-000000000069"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 15 },
                    { new Guid("00000001-0000-7000-a000-000000000073"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "نفقات مرسلة", "Capitalized Expenses", "", 132, new Guid("00000001-0000-7000-a000-000000000068"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 15 },
                    { new Guid("00000001-0000-7000-a000-000000000074"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "نفقات تحديث فروع ومعارض النشاط التجاري", "Business branches and showrooms modernization expenses", "", 1321, new Guid("00000001-0000-7000-a000-000000000073"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 15 },
                    { new Guid("00000001-0000-7000-a000-000000000075"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مساهمة المنشأة في إنشاء أصول غير مملوكة لها وتخدم أغراضها", "Company Contribution in Establishing Assets that it doesn't own but serve its purposes.", "", 1322, new Guid("00000001-0000-7000-a000-000000000073"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 15 },
                    { new Guid("00000001-0000-7000-a000-000000000076"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مقابل حق الانتفاع بمقار عن طريق الشراء بالجدك", "In exchange for the right to usufruct a place through purchase of business assets", "", 1323, new Guid("00000001-0000-7000-a000-000000000073"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 15 },
                    { new Guid("00000001-0000-7000-a000-000000000077"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "نفقات مؤجلة*", "Deferred Expenses*", "", 133, new Guid("00000001-0000-7000-a000-000000000068"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 15 },
                    { new Guid("00000001-0000-7000-a000-000000000078"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "نفقات تأسيس", "Incorporation Expenses", "", 1331, new Guid("00000001-0000-7000-a000-000000000077"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 15 },
                    { new Guid("00000001-0000-7000-a000-000000000079"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "نفقات ما قبل بدء الإنتاج/ التشغيل", "Pre-production/Pre-operation Expenses", "", 1332, new Guid("00000001-0000-7000-a000-000000000077"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 15 },
                    { new Guid("00000001-0000-7000-a000-000000000080"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "حملة إعلانية", "Advertising campaign", "", 1333, new Guid("00000001-0000-7000-a000-000000000077"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 15 },
                    { new Guid("00000001-0000-7000-a000-000000000081"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "الخصوم", "Liabilities", "", 2, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 0 },
                    { new Guid("00000001-0000-7000-a000-000000000082"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "الخصوم المتداولة", "Currents Liabilites", "", 21, new Guid("00000001-0000-7000-a000-000000000081"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000083"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "حسابات الدائنون", "Accounts Payable", "", 211, new Guid("00000001-0000-7000-a000-000000000082"), new Guid("00000001-0000-7000-a000-000000000081"), 0, 0 },
                    { new Guid("00000001-0000-7000-a000-000000000084"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "موردون", "Vendors", "", 2111, new Guid("00000001-0000-7000-a000-000000000083"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 21 },
                    { new Guid("00000001-0000-7000-a000-000000000085"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "أوراق الدفع", "Notes Payable", "", 2112, new Guid("00000001-0000-7000-a000-000000000083"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 21 },
                    { new Guid("00000001-0000-7000-a000-000000000086"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "دائنو التوزيعات", "Creditors of Dividends", "", 2113, new Guid("00000001-0000-7000-a000-000000000083"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 21 },
                    { new Guid("00000001-0000-7000-a000-000000000087"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "مصروفات مستحقة السداد", "Due expenses", "", 212, new Guid("00000001-0000-7000-a000-000000000082"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000088"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مرتبات مستحقة السداد", "Due salaries", "", 2121, new Guid("00000001-0000-7000-a000-000000000087"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000089"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "إيرادات محصلة مقدما", "Revenues Collected in Advance", "", 2122, new Guid("00000001-0000-7000-a000-000000000087"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000090"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "حسابات دائنه أخرى", "Other Accounts Payable", "", 2123, new Guid("00000001-0000-7000-a000-000000000087"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000091"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "حسابات دائنه للمصالح والهيئات", "Accounts Payable for Authorities and Bodies", "", 213, new Guid("00000001-0000-7000-a000-000000000082"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000092"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مصلحة الجمارك", "Customs authority ", "", 2131, new Guid("00000001-0000-7000-a000-000000000091"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000093"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "ضريبة الفيمة المضافة", "VAT", "", 2132, new Guid("00000001-0000-7000-a000-000000000091"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000094"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مصلحة الضرائب العامة", "General Taxation Authority ", "", 2133, new Guid("00000001-0000-7000-a000-000000000091"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000095"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مصلحة الضرائب العقارية", "Real Estate Taxation Authority", "", 2134, new Guid("00000001-0000-7000-a000-000000000091"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000096"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "جارى مصلحة التأمينات الاجتماعية", "Social insurance current account", "", 2135, new Guid("00000001-0000-7000-a000-000000000091"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000097"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "هيئات تأمينية أخرى", "Other Insurance Authorities", "", 2136, new Guid("00000001-0000-7000-a000-000000000091"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000098"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "حسابات دائنة أخرى", "Other Accounts Payable", "", 214, new Guid("00000001-0000-7000-a000-000000000082"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000099"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "سحب على المكشوف", "Overdraft", "", 2141, new Guid("00000001-0000-7000-a000-000000000098"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000100"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "تمويل اعتمادات مستنديه", "Financing of LCs", "", 2142, new Guid("00000001-0000-7000-a000-000000000098"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000101"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "قروض قصيرة الأجل", "Short term loans", "", 2143, new Guid("00000001-0000-7000-a000-000000000098"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000102"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "حسابات دائنه للشركات القابضة / التابعة / الشقيقة", "Credit Accounts for Holding Company, Affiliated Company and Sister Company", "", 2144, new Guid("00000001-0000-7000-a000-000000000098"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000103"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "الخصوم طويلة الأجل", "Long term Liabilities", "", 22, new Guid("00000001-0000-7000-a000-000000000081"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 24 },
                    { new Guid("00000001-0000-7000-a000-000000000104"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "قروض طويلة الأجل من شركات قابضة / تابعة / شقيقة", "Long term loans from Holding company, affiliated company and sister company", "", 221, new Guid("00000001-0000-7000-a000-000000000103"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 24 },
                    { new Guid("00000001-0000-7000-a000-000000000105"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "قروض طويلة الأجل من البنوك", "Long terms loans from banks", "", 222, new Guid("00000001-0000-7000-a000-000000000103"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 24 },
                    { new Guid("00000001-0000-7000-a000-000000000106"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "قروض طويلة الأجل من جهات أخرى", "Long Terms Loans from Other Parties", "", 223, new Guid("00000001-0000-7000-a000-000000000103"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 24 },
                    { new Guid("00000001-0000-7000-a000-000000000107"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "سندات", "Bonds", "", 224, new Guid("00000001-0000-7000-a000-000000000103"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 24 },
                    { new Guid("00000001-0000-7000-a000-000000000108"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "مخصصات :", "Provisions:", "", 23, new Guid("00000001-0000-7000-a000-000000000081"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 24 },
                    { new Guid("00000001-0000-7000-a000-000000000109"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "مخصص إهلاك أصول ثابتة", "Provision for Fixed Assets Depreciation", "", 231, new Guid("00000001-0000-7000-a000-000000000108"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 24 },
                    { new Guid("00000001-0000-7000-a000-000000000110"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخصص إهلاك مزروعات معمرة قابلة للإهلاك", "Provision for Destructible Perennial Produce", "", 2311, new Guid("00000001-0000-7000-a000-000000000109"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 24 },
                    { new Guid("00000001-0000-7000-a000-000000000111"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخصص إهلاك مباني وإنشاءات ومرافق وطرق", "Provision for Buildings, Construction, Facilities and Roads Depreciation", "", 2312, new Guid("00000001-0000-7000-a000-000000000109"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 24 },
                    { new Guid("00000001-0000-7000-a000-000000000112"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخصص إهلاك آلات ومعدات", "Provision for Tools & Equipments Depreciation ", "", 2313, new Guid("00000001-0000-7000-a000-000000000109"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 24 },
                    { new Guid("00000001-0000-7000-a000-000000000113"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخصص إهلاك وسائل نقل وانتقال", "Provision for Transportation Depreciation ", "", 2314, new Guid("00000001-0000-7000-a000-000000000109"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 24 },
                    { new Guid("00000001-0000-7000-a000-000000000114"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخصص إهلاك عدد وأدوات", "Provision for Machines and Equipments Depreciation", "", 2315, new Guid("00000001-0000-7000-a000-000000000109"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 24 },
                    { new Guid("00000001-0000-7000-a000-000000000115"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخصص إهلاك أثاث وتجهيزات مكتبية", "Provision for Furniture and Office Equipments Depreciation", "", 2316, new Guid("00000001-0000-7000-a000-000000000109"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 24 },
                    { new Guid("00000001-0000-7000-a000-000000000116"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخصص إهلاك ثروة حيوانية ومائية", "Provision for Animal & Water Wealth Depreciation ", "", 2317, new Guid("00000001-0000-7000-a000-000000000109"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 24 },
                    { new Guid("00000001-0000-7000-a000-000000000117"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخصص هبوط أسعار مخزون الإنتاج غير التام", "Provision for Unfinished Products Price Decrease", "", 232, new Guid("00000001-0000-7000-a000-000000000108"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000118"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخصص هبوط أسعار مخزون الإنتاج التام", "Provision for Finished Products Price Decrease", "", 233, new Guid("00000001-0000-7000-a000-000000000108"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000119"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخصص هبوط أسعار مخزون البضائع المشتراة", "Provision for Purchased Goods Price Decrease", "", 234, new Guid("00000001-0000-7000-a000-000000000108"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000120"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخصص هبوط أسعار الأوراق المالية", "Provision for Securities Price Decrease", "", 235, new Guid("00000001-0000-7000-a000-000000000108"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000121"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخصص الديون المشكوك في تحصيلها", "provision for doubtful debts", "", 236, new Guid("00000001-0000-7000-a000-000000000108"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000122"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخصص الضرائب المتنازع عليها", "Provision for disputed taxes", "", 237, new Guid("00000001-0000-7000-a000-000000000108"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000123"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخصص المطالبات والمنازعات", "provision for claims and disputes", "", 238, new Guid("00000001-0000-7000-a000-000000000108"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000124"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخصصات أخرى", "other provisions", "", 239, new Guid("00000001-0000-7000-a000-000000000108"), new Guid("00000001-0000-7000-a000-000000000081"), 1, 23 },
                    { new Guid("00000001-0000-7000-a000-000000000125"), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "حقوق الملكية", "Property rights", "", 3, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0000-7000-a000-000000000125"), 1, 25 },
                    { new Guid("00000001-0000-7000-a000-000000000126"), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "رأس المال", "Capital", "", 341, new Guid("00000001-0000-7000-a000-000000000125"), new Guid("00000001-0000-7000-a000-000000000125"), 1, 25 },
                    { new Guid("00000001-0000-7000-a000-000000000127"), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "جاري الشركاء", "Shareholders current accounts", "", 342, new Guid("00000001-0000-7000-a000-000000000125"), new Guid("00000001-0000-7000-a000-000000000125"), 1, 25 },
                    { new Guid("00000001-0000-7000-a000-000000000128"), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "أقساط متأخر سدادها", "Late installments", "", 343, new Guid("00000001-0000-7000-a000-000000000125"), new Guid("00000001-0000-7000-a000-000000000125"), 1, 25 },
                    { new Guid("00000001-0000-7000-a000-000000000129"), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "أرباح (خسائر) مرحلة", "Retained Earnings (losses)", "", 344, new Guid("00000001-0000-7000-a000-000000000125"), new Guid("00000001-0000-7000-a000-000000000125"), 1, 25 },
                    { new Guid("00000001-0000-7000-a000-000000000130"), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "اسهم الخزينة", "Treasury Stock", "", 345, new Guid("00000001-0000-7000-a000-000000000125"), new Guid("00000001-0000-7000-a000-000000000125"), 1, 25 },
                    { new Guid("00000001-0000-7000-a000-000000000131"), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "احتياطيات", "Reserves", "", 346, new Guid("00000001-0000-7000-a000-000000000125"), new Guid("00000001-0000-7000-a000-000000000125"), 1, 25 },
                    { new Guid("00000001-0000-7000-a000-000000000132"), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "احتياطي قانوني", "legal reserve", "", 3461, new Guid("00000001-0000-7000-a000-000000000131"), new Guid("00000001-0000-7000-a000-000000000125"), 1, 25 },
                    { new Guid("00000001-0000-7000-a000-000000000133"), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "احتياطي نظامي", "Statutory Reserve", "", 3462, new Guid("00000001-0000-7000-a000-000000000131"), new Guid("00000001-0000-7000-a000-000000000125"), 1, 25 },
                    { new Guid("00000001-0000-7000-a000-000000000134"), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "احتياطي رأسمالي", "Capital reserve", "", 3463, new Guid("00000001-0000-7000-a000-000000000131"), new Guid("00000001-0000-7000-a000-000000000125"), 1, 25 },
                    { new Guid("00000001-0000-7000-a000-000000000135"), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "احتياطي أخرى", "other reserves", "", 3464, new Guid("00000001-0000-7000-a000-000000000131"), new Guid("00000001-0000-7000-a000-000000000125"), 1, 25 },
                    { new Guid("00000001-0000-7000-a000-000000000136"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "المصروفات", "Expenses", "", 4, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 0 },
                    { new Guid("00000001-0000-7000-a000-000000000137"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "تكلفة البضاعة المباعة", "Cost of Sold Goods", "", 41, new Guid("00000001-0000-7000-a000-000000000136"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 31 },
                    { new Guid("00000001-0000-7000-a000-000000000138"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "مصروفات النشاط", "Statement  Expenses", "", 42, new Guid("00000001-0000-7000-a000-000000000136"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000139"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "مصروفات ادارية وعمومية", "General & Admin expenses", "", 421, new Guid("00000001-0000-7000-a000-000000000138"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000140"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "مواد ووقود وقطع غيار", "Material, fuel and spare parts", "", 4211, new Guid("00000001-0000-7000-a000-000000000139"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000141"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "وقود وزيوت", "Fuel and Oils", "", 42111, new Guid("00000001-0000-7000-a000-000000000140"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000142"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "قطع غيار ومهمات", "Spare Parts and Gears", "", 42112, new Guid("00000001-0000-7000-a000-000000000140"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000143"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "كهرباء ومياه", "Electricity & water", "", 42113, new Guid("00000001-0000-7000-a000-000000000140"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000144"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "مصروفات نثرية", "Petty expenses", "", 4212, new Guid("00000001-0000-7000-a000-000000000139"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000145"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "تلفونات ومحمول وانترنت", "Telephones, Mobiles & Internet", "", 42121, new Guid("00000001-0000-7000-a000-000000000144"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000146"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "ضيافة المكتب وخارجة", "In office and Out-of-office Expenses", "", 42122, new Guid("00000001-0000-7000-a000-000000000144"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000147"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "اكراميات ادارية", "Administrative tips", "", 42123, new Guid("00000001-0000-7000-a000-000000000144"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000148"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "أدوات كتابية", "Stationary", "", 42124, new Guid("00000001-0000-7000-a000-000000000144"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000149"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "أجور", "Wages", "", 4213, new Guid("00000001-0000-7000-a000-000000000139"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000150"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "أجور نقدية", "Wages in Cash", "", 42131, new Guid("00000001-0000-7000-a000-000000000149"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000151"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مزايا عينية", "Advantages In kind", "", 42132, new Guid("00000001-0000-7000-a000-000000000149"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000152"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "تأمينات اجتماعية", "Social insurance", "", 42133, new Guid("00000001-0000-7000-a000-000000000149"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000153"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مصروفات انتقالات", "transportation expenses", "", 42134, new Guid("00000001-0000-7000-a000-000000000149"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000154"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "اتعاب محاميين ومحاسبيين", "Legal and Accounting fees", "", 42135, new Guid("00000001-0000-7000-a000-000000000149"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000155"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "مصروفات إدارية أخرى", "Other Administrative Expenses", "", 4214, new Guid("00000001-0000-7000-a000-000000000139"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000156"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "خدمات مشتراة", "Purchases Services", "", 42141, new Guid("00000001-0000-7000-a000-000000000155"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000157"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مصروفات صيانة", "Maintenance Expenses", "", 42142, new Guid("00000001-0000-7000-a000-000000000155"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000158"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مصروفات دعاية وإعلان ونشر وطبع وعلاقات عامة واستقبال", "Advertising, Publicity, Printing, PR and Reception Expenses", "", 42143, new Guid("00000001-0000-7000-a000-000000000155"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000159"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مصروفات نقل وانتقالات واتصالات", "Transportation and communication expenses", "", 42144, new Guid("00000001-0000-7000-a000-000000000155"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000160"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "إيجار أصول ثابتة (بخلاف العقارات)", "Fixed Assets Rent  (real estate excluded)", "", 42145, new Guid("00000001-0000-7000-a000-000000000155"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000161"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "خدمات الجهات الحكومية والمؤسسات", "Government agencies &  Institutions  services", "", 42146, new Guid("00000001-0000-7000-a000-000000000155"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000162"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "مصروفات خدميه أخرى", "Other Tertiary Expenses", "", 4215, new Guid("00000001-0000-7000-a000-000000000139"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000163"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "الإهلاك والاستهلاك", "Depreciation and Amortization", "", 42151, new Guid("00000001-0000-7000-a000-000000000162"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000164"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "إهلاك الأصول الثابتة", "Fixed assets depreciation", "", 421511, new Guid("00000001-0000-7000-a000-000000000163"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 33 },
                    { new Guid("00000001-0000-7000-a000-000000000165"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "استهلاك الأصول غير الملموسة والنفقات المرسملة", "Intangible Assets and Capital Expenses Amortization", "", 421512, new Guid("00000001-0000-7000-a000-000000000163"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 33 },
                    { new Guid("00000001-0000-7000-a000-000000000166"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "فوائد", "Interests", "", 42152, new Guid("00000001-0000-7000-a000-000000000162"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000167"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "إيجار عقارات (أراضى ومباني)", "Real Estate Rent (lands& buildings)", "", 42153, new Guid("00000001-0000-7000-a000-000000000162"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000168"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "ضرائب عقارية", "Real Estate Taxes", "", 42154, new Guid("00000001-0000-7000-a000-000000000162"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000169"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "ضرائب غير مباشرة على النشاط ", "Indirect Tax on Statement ", "", 42155, new Guid("00000001-0000-7000-a000-000000000162"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000170"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "مصروفات تسويقية", "marketing expenses", "", 422, new Guid("00000001-0000-7000-a000-000000000138"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000171"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "اقامة فنادق", "Hotels", "", 4221, new Guid("00000001-0000-7000-a000-000000000170"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000172"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "دعاية واعلان", "Publicity and Advertising", "", 4222, new Guid("00000001-0000-7000-a000-000000000170"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000173"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "هدايا وعينات تسويقية", "Marketing Gifts and Samples", "", 4223, new Guid("00000001-0000-7000-a000-000000000170"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000174"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "تألف إنتاج تالف / بضائع مشتراة (في مرحلة البيع)", "Damaged Production/Purchased Goods (in sale stage).", "", 4224, new Guid("00000001-0000-7000-a000-000000000170"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000175"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "غرامات التاخير", "Delay penalties", "", 4225, new Guid("00000001-0000-7000-a000-000000000170"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000176"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مؤتمرات", "Conferences", "", 4226, new Guid("00000001-0000-7000-a000-000000000170"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000177"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "كراسات الشروط", "Tenders Specifications", "", 4227, new Guid("00000001-0000-7000-a000-000000000170"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000178"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "اكراميات تسويقية", "Marketing Tips", "", 4228, new Guid("00000001-0000-7000-a000-000000000170"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000179"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "عمولات المناديب و المسوقين", "Sales reps & Marketers Commissions", "", 4229, new Guid("00000001-0000-7000-a000-000000000170"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000180"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "مصروفات تمويلية", "Financial expenses", "", 423, new Guid("00000001-0000-7000-a000-000000000138"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000181"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مصروفات بنكية", "Bank expenses", "", 4231, new Guid("00000001-0000-7000-a000-000000000180"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000182"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مصروفات خطابات الضمان", "LG expenses", "", 4232, new Guid("00000001-0000-7000-a000-000000000180"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000183"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مصروفات بنكية وعمولات", "Bank Expenses and Commissions", "", 4233, new Guid("00000001-0000-7000-a000-000000000180"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000184"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مصروفات كشف الحساب", "Bank statement expenses", "", 4234, new Guid("00000001-0000-7000-a000-000000000180"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000185"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "مصروفات التشغيل والانتاج", "Operation and production expenses", "", 424, new Guid("00000001-0000-7000-a000-000000000138"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000186"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "اجور تشغيلية", "Operating Salaries", "", 4241, new Guid("00000001-0000-7000-a000-000000000185"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000187"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "مصروفات تشغيلية", "Operating Expenses", "", 4242, new Guid("00000001-0000-7000-a000-000000000185"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000188"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مصروف اهلاكات تشغيلية(الالات ومعدات)", "Operation Depreciation Expenses (tools& equipments)", "", 42421, new Guid("00000001-0000-7000-a000-000000000187"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000189"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مصروفات صيانة واصلاح", "Maintenance and repair expenses", "", 42422, new Guid("00000001-0000-7000-a000-000000000187"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000190"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مصروفات تشغيل لدى الغير", "Operating expenses with others", "", 42423, new Guid("00000001-0000-7000-a000-000000000187"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000191"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مصروفات نقل تشغيلية", "Operating transportation expenses", "", 42424, new Guid("00000001-0000-7000-a000-000000000187"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000192"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مصروفات استئجار الالات ومعدات للتشغيل", "Rent Expenses for Operation Tools and Equipments", "", 42425, new Guid("00000001-0000-7000-a000-000000000187"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000000193"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "مصروفات أخرى", "other expenses", "", 43, new Guid("00000001-0000-7000-a000-000000000136"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 34 },
                    { new Guid("00000001-0000-7000-a000-000000000194"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخصصات (بخلاف الإهلاك)", "Provisions (other than depreciation)", "", 431, new Guid("00000001-0000-7000-a000-000000000193"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 34 },
                    { new Guid("00000001-0000-7000-a000-000000000195"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "ديون معدومة", "Bad debts", "", 432, new Guid("00000001-0000-7000-a000-000000000193"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 33 },
                    { new Guid("00000001-0000-7000-a000-000000000196"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "خسائر بيع أوراق مالية", "Securities Sale Losses", "", 433, new Guid("00000001-0000-7000-a000-000000000193"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 34 },
                    { new Guid("00000001-0000-7000-a000-000000000197"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "أعباء وخسائر متنوعة", "Miscellaneous burdens and losses", "", 434, new Guid("00000001-0000-7000-a000-000000000193"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 34 },
                    { new Guid("00000001-0000-7000-a000-000000000198"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "خسائر بيع مخلفات", "Waste Sale Losses", "", 4341, new Guid("00000001-0000-7000-a000-000000000197"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 33 },
                    { new Guid("00000001-0000-7000-a000-000000000199"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "خسائر بيع خامات ومواد وقطع غيار", "Raw material, Material and Spare Parts Sale Losses", "", 4342, new Guid("00000001-0000-7000-a000-000000000197"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 33 },
                    { new Guid("00000001-0000-7000-a000-000000000200"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "تعويضات وغرامات", "Compensations and Penalties", "", 4343, new Guid("00000001-0000-7000-a000-000000000197"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 34 },
                    { new Guid("00000001-0000-7000-a000-000000000201"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "تبرعات وإعانات", "Donations and Aids", "", 4344, new Guid("00000001-0000-7000-a000-000000000197"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 34 },
                    { new Guid("00000001-0000-7000-a000-000000000202"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "خسائر فروق العملة", "Exchange rate losses", "", 435, new Guid("00000001-0000-7000-a000-000000000193"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 34 },
                    { new Guid("00000001-0000-7000-a000-000000000203"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مصروفات سنوات سابقة", "Previous Years Expenses", "", 436, new Guid("00000001-0000-7000-a000-000000000193"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 34 },
                    { new Guid("00000001-0000-7000-a000-000000000204"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "خسائر رأسمالية", "Capital losses", "", 437, new Guid("00000001-0000-7000-a000-000000000193"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 34 },
                    { new Guid("00000001-0000-7000-a000-000000000205"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "فروق تسويات مالية", "Financial adjustment differences", "", 438, new Guid("00000001-0000-7000-a000-000000000193"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 34 },
                    { new Guid("00000001-0000-7000-a000-000000000206"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "ضرائب الدخل", "Income Taxes", "", 439, new Guid("00000001-0000-7000-a000-000000000193"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 34 },
                    { new Guid("00000001-0000-7000-a000-000000000207"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "الإيرادات", "Revenues", "", 5, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 0 },
                    { new Guid("00000001-0000-7000-a000-000000000208"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "إيرادات النشاط", "Activity revenues", "", 51, new Guid("00000001-0000-7000-a000-000000000207"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 41 },
                    { new Guid("00000001-0000-7000-a000-000000000209"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "المبيعات", "sales", "", 511, new Guid("00000001-0000-7000-a000-000000000208"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 41 },
                    { new Guid("00000001-0000-7000-a000-000000000210"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مبيعات بضائع مشتراة", "Sales of Purchased Goods", "", 5111, new Guid("00000001-0000-7000-a000-000000000209"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 41 },
                    { new Guid("00000001-0000-7000-a000-000000000211"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مرتجعات مبيعات  (مدين)", "Sales return (debit)", "", 5114, new Guid("00000001-0000-7000-a000-000000000209"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 41 },
                    { new Guid("00000001-0000-7000-a000-000000000212"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "الخصومات", "Penalties", "", 512, new Guid("00000001-0000-7000-a000-000000000208"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 41 },
                    { new Guid("00000001-0000-7000-a000-000000000213"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مبيعات منتج تام", "Sales of Finished Goods", "", 5112, new Guid("00000001-0000-7000-a000-000000000209"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 41 },
                    { new Guid("00000001-0000-7000-a000-000000000215"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "ايرادات تشغيلية أخرى", "Other Operating Revenues", "", 5133, new Guid("00000001-0000-7000-a000-000000000221"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 41 },
                    { new Guid("00000001-0000-7000-a000-000000000216"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مسموحات مبيعات  (مدين)", "Sales Allowances (debit)", "", 5115, new Guid("00000001-0000-7000-a000-000000000209"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 41 },
                    { new Guid("00000001-0000-7000-a000-000000000217"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "خصم مكتسب", "Discount Allowed", "", 5121, new Guid("00000001-0000-7000-a000-000000000212"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 41 },
                    { new Guid("00000001-0000-7000-a000-000000000218"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "خدمات مباعة", "Sold services", "", 5113, new Guid("00000001-0000-7000-a000-000000000209"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 41 },
                    { new Guid("00000001-0000-7000-a000-000000000219"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "إيرادات تشغيل للغير", "Operation revenue for others", "", 5132, new Guid("00000001-0000-7000-a000-000000000221"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 41 },
                    { new Guid("00000001-0000-7000-a000-000000000220"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "عائد عقود تأجير تمويلي", "Financial lease contract revenue", "", 5131, new Guid("00000001-0000-7000-a000-000000000221"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 41 },
                    { new Guid("00000001-0000-7000-a000-000000000221"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "إيرادات النشاط الأخرى", "Other Statement  Revenues", "", 513, new Guid("00000001-0000-7000-a000-000000000208"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 41 },
                    { new Guid("00000001-0000-7000-a000-000000000222"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "منح وإعانات", "Grants & Aids", "", 52, new Guid("00000001-0000-7000-a000-000000000207"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 44 },
                    { new Guid("00000001-0000-7000-a000-000000000223"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "إيرادات استثمار وفوائد", "Investment & Interest Revenues", "", 53, new Guid("00000001-0000-7000-a000-000000000207"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 44 },
                    { new Guid("00000001-0000-7000-a000-000000000224"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "إيرادات استثمارات مالية من شركات قابضة", "Financial Investments Revenues from Holding Companies", "", 531, new Guid("00000001-0000-7000-a000-000000000223"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 44 },
                    { new Guid("00000001-0000-7000-a000-000000000225"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "إيرادات استثمارات مالية من شركات شقيقة", "Financial Investments Revenues from Sister Companies", "", 532, new Guid("00000001-0000-7000-a000-000000000223"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 44 },
                    { new Guid("00000001-0000-7000-a000-000000000226"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "إيرادات استثمارات مالية أخرى", "Other Financial Investments Revenue", "", 533, new Guid("00000001-0000-7000-a000-000000000223"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 44 },
                    { new Guid("00000001-0000-7000-a000-000000000227"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "فوائد قروض لشركات قابضة / تابعة / شقيقة", "Loan Interests to Holding Companies, Affiliated Companies and Sister Companies", "", 534, new Guid("00000001-0000-7000-a000-000000000223"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 44 },
                    { new Guid("00000001-0000-7000-a000-000000000228"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "فوائد دائنه أخرى", "Other credit benefits", "", 535, new Guid("00000001-0000-7000-a000-000000000223"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 44 },
                    { new Guid("00000001-0000-7000-a000-000000000229"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "إيرادات وأرباح أخرى", "Other Revenues and Profits", "", 54, new Guid("00000001-0000-7000-a000-000000000207"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 43 },
                    { new Guid("00000001-0000-7000-a000-000000000230"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخصصات وانتفى الغرض منها", "Provisions no longer required", "", 541, new Guid("00000001-0000-7000-a000-000000000229"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 43 },
                    { new Guid("00000001-0000-7000-a000-000000000231"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "ديون سبق إعدامها", "Debts Already Written-off", "", 542, new Guid("00000001-0000-7000-a000-000000000229"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 43 },
                    { new Guid("00000001-0000-7000-a000-000000000232"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "أرباح بيع أوراق مالية", "Securities Sale Profits", "", 543, new Guid("00000001-0000-7000-a000-000000000229"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 43 },
                    { new Guid("00000001-0000-7000-a000-000000000233"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "إيرادات وأرباح متنوعة", "Diverse Revenues and Profits", "", 544, new Guid("00000001-0000-7000-a000-000000000229"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 44 },
                    { new Guid("00000001-0000-7000-a000-000000000234"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "أرباح بيع مخلفات", "Waste Sale Profits", "", 5441, new Guid("00000001-0000-7000-a000-000000000233"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 44 },
                    { new Guid("00000001-0000-7000-a000-000000000235"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "أرباح بيع خدمات ومواد وقطع غيار", "Services, Material and Spare Parts Sale Profits", "", 5442, new Guid("00000001-0000-7000-a000-000000000233"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 44 },
                    { new Guid("00000001-0000-7000-a000-000000000236"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "إيرادات  تعويضات وغرامات", "Compensations and Penalties Revenues", "", 5443, new Guid("00000001-0000-7000-a000-000000000233"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 44 },
                    { new Guid("00000001-0000-7000-a000-000000000237"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "عمولات", "Commissions", "", 5444, new Guid("00000001-0000-7000-a000-000000000233"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 44 },
                    { new Guid("00000001-0000-7000-a000-000000000238"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "إيجارات دائنه", "Credit rent", "", 5445, new Guid("00000001-0000-7000-a000-000000000233"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 44 },
                    { new Guid("00000001-0000-7000-a000-000000000239"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "أرباح فروق العملة", "Exchange rate profit", "", 545, new Guid("00000001-0000-7000-a000-000000000229"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 44 },
                    { new Guid("00000001-0000-7000-a000-000000000240"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "إيرادات سنوية سابقة", "Previous Annual Revenues", "", 546, new Guid("00000001-0000-7000-a000-000000000229"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 44 },
                    { new Guid("00000001-0000-7000-a000-000000000241"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "أرباح رأسمالية", "Capital Profits", "", 547, new Guid("00000001-0000-7000-a000-000000000229"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 44 },
                    { new Guid("00000001-0000-7000-a000-000000000242"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "إيرادات وأرباح غير عادية", "Extraordinary Revenues and Profit", "", 548, new Guid("00000001-0000-7000-a000-000000000229"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 44 },
                    { new Guid("00000001-0000-7000-a000-000000001243"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "خصم كميه مكتسب", "Quantity Discount Earned", "", 5123, new Guid("00000001-0000-7000-a000-000000000212"), new Guid("00000001-0000-7000-a000-000000000207"), 0, 41 },
                    { new Guid("00000001-0000-7000-a000-000000001244"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "خصم كمية مسموح به ( مدين )", "Quantity Allowed Discount  (debit)", "", 5124, new Guid("00000001-0000-7000-a000-000000000212"), new Guid("00000001-0000-7000-a000-000000000207"), 0, 41 },
                    { new Guid("00000001-0000-7000-a000-000000001245"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "خصم مسموح به ( مدين )", "Discount Allowed (debit)", "", 5122, new Guid("00000001-0000-7000-a000-000000000212"), new Guid("00000001-0000-7000-a000-000000000207"), 0, 41 },
                    { new Guid("00000001-0000-7000-a000-000000001248"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "عملاء أجانب", "Foreign Customers", "", 1234, new Guid("00000001-0000-7000-a000-000000000051"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 13 },
                    { new Guid("00000001-0000-7000-a000-000000001250"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, true, "حسابات مدينة لدى الموظفين", "Debit accounts by employees", "", 125, new Guid("00000001-0000-7000-a000-000000000031"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000001251"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مستحقات سداد الخدمات الالكترونية", "E-Services Payment entitlements", "", 1252, new Guid("00000001-0000-7000-a000-000000001250"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000001252"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "فروق تسويات جردية", "Inventory Adjustment Differences", "", 4310, new Guid("00000001-0000-7000-a000-000000000193"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 33 },
                    { new Guid("00000001-0000-7000-a000-000000001253"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "الهيئة العامة للزكاة و الدخل", "General Authority for Zakat and Income ", "", 1244, new Guid("00000001-0000-7000-a000-000000000055"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000001254"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "بطاقات إئتمان", "Credit Cards", "", 1216, new Guid("00000001-0000-7000-a000-000000000032"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 12 },
                    { new Guid("00000001-0000-7000-a000-000000001255"), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "الزمم الدائنة", "Credit Accounts", "", 2114, new Guid("00000001-0000-7000-a000-000000000083"), new Guid("00000001-0000-7000-a000-000000000081"), 0, 21 },
                    { new Guid("00000001-0000-7000-a000-000000002256"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "الوكلاء", "Franchisees", "", 1235, new Guid("00000001-0000-7000-a000-000000000051"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 13 },
                    { new Guid("00000001-0000-7000-a000-000000002257"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "الموزعون", "Distributors", "", 1236, new Guid("00000001-0000-7000-a000-000000000051"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 13 },
                    { new Guid("00000001-0000-7000-a000-000000002258"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "اجور نسب المناديب", "Salary of Sales Rep", "", 42136, new Guid("00000001-0000-7000-a000-000000000149"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000002259"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "حافظة الشيكات المرتجعة", "Returned Checks Portfolio", "", 1237, new Guid("00000001-0000-7000-a000-000000000051"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 13 },
                    { new Guid("00000001-0000-7000-a000-000000003259"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مخزون بضاعة على سبيل الأمانة", "Consignment Inventory", "", 1227, new Guid("00000001-0000-7000-a000-000000000038"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000003260"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مبيعات بضاعة بغرض الأمانة", "Sales of Consignment goods", "", 5116, new Guid("00000001-0000-7000-a000-000000000209"), new Guid("00000001-0000-7000-a000-000000000207"), 0, 41 },
                    { new Guid("00000001-0000-7000-a000-000000003261"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "مرتجعات مبيعات  بغرض الأمانة (مدين)", "Consignment Goods Sale Returns (debit)", "", 5114, new Guid("00000001-0000-7000-a000-000000000209"), new Guid("00000001-0000-7000-a000-000000000207"), 1, 41 },
                    { new Guid("00000001-0000-7000-a000-000000003262"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "تحويلات المخازن", "Warehouse transfers", "", 1228, new Guid("00000001-0000-7000-a000-000000000038"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 14 },
                    { new Guid("00000001-0000-7000-a000-000000003263"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "فائض إغلاق اليومية", "End of Day Surplus", "", 5134, new Guid("00000001-0000-7000-a000-000000000221"), new Guid("00000001-0000-7000-a000-000000000207"), 0, 42 },
                    { new Guid("00000001-0000-7000-a000-000000003264"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "عجز في إغلاق اليومية", "End of Day Deficit", "", 425, new Guid("00000001-0000-7000-a000-000000001250"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 42 },
                    { new Guid("00000001-0000-7000-a000-000000004263"), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "الأرصدة الإفتتاحية", "Opening balances", "", 347, new Guid("00000001-0000-7000-a000-000000000125"), new Guid("00000001-0000-7000-a000-000000000081"), 0, 25 },
                    { new Guid("00000001-0000-7000-a000-000000004264"), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "ضريبة الخصم و الإضافة", "Withholding Tax", "", 3137, new Guid("00000001-0000-7000-a000-000000000091"), new Guid("00000001-0000-7000-a000-000000000081"), 0, 23 },
                    { new Guid("00000001-0000-7000-a000-000000004265"), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "أرباح غير محققة", "Unrealized Gains", "", 3145, new Guid("00000001-0000-7000-a000-000000000098"), new Guid("00000001-0000-7000-a000-000000000081"), 0, 23 },
                    { new Guid("00000001-0000-7000-a000-000000004266"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "أرباح محققة", "Realized Gains", "", 5135, new Guid("00000001-0000-7000-a000-000000000221"), new Guid("00000001-0000-7000-a000-000000000207"), 0, 41 },
                    { new Guid("00000001-0000-7000-a000-000000004267"), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "فروق تقريب الكسور العشرية", "Fraction Approximation Differences", "", 4345, new Guid("00000001-0000-7000-a000-000000000197"), new Guid("00000001-0000-7000-a000-000000000136"), 0, 32 },
                    { new Guid("00000001-0000-7000-a000-000000004268"), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "ايرادات التوصيل", "Delivery Income", "", 5136, new Guid("00000001-0000-7000-a000-000000000221"), new Guid("00000001-0000-7000-a000-000000000207"), 0, 42 },
                    { new Guid("00000001-0000-7000-a000-000000004269"), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "كوبونات الخصم", "Discount Coupons", "", 3146, new Guid("00000001-0000-7000-a000-000000000098"), new Guid("00000001-0000-7000-a000-000000000081"), 0, 23 },
                    { new Guid("00000001-0000-7000-a000-000000004270"), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EGP", true, false, "محافظ اليكترونيه", "EWallets", "", 1217, new Guid("00000001-0000-7000-a000-000000000032"), new Guid("00000001-0000-7000-a000-000000000001"), 0, 12 }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "ArabicName", "Code", "EnglishName", "IsDefault", "Value" },
                values: new object[,]
                {
                    { new Guid("00000008-0000-7000-a000-000000000001"), "جنية مصرى", "EGP", "Egyptian Pound", true, 1m },
                    { new Guid("00000008-0000-7000-a000-000000000002"), "ريال سعودى", "SR", "Saudi Riyal", false, 1m }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("00000003-0000-7000-a000-000000000001"), "يمكنه عرض بيانات المبيعات", "عرض المبيعات" },
                    { new Guid("00000003-0000-7000-a000-000000000002"), "يمكنه معالجة معاملات المبيعات", "معالجة المبيعات" },
                    { new Guid("00000003-0000-7000-a000-000000000003"), "يمكنه إدارة المخزون", "ادارة المخزون" },
                    { new Guid("00000003-0000-7000-a000-000000000004"), "يمكنه عرض التقارير", "عرض التقارير" },
                    { new Guid("00000003-0000-7000-a000-000000000005"), "يمكنه إدارة المستخدمين", "ادارة المستخدمين" },
                    { new Guid("00000003-0000-7000-a000-000000000006"), "يمكنه إدارة الأدوار والصلاحيات", "ادارة الأدوار" },
                    { new Guid("00000003-0000-7000-a000-000000000007"), "يمكنه عرض معلومات العملاء", "عرض العملاء" },
                    { new Guid("00000003-0000-7000-a000-000000000008"), "يمكنه إدارة معلومات العملاء", "ادارة العملاء" },
                    { new Guid("00000003-0000-7000-a000-000000000009"), "يمكنه تطبيق الخصومات", "تطبيق الخصومات" },
                    { new Guid("00000003-0000-7000-a000-000000000010"), "يمكنه الوصول إلى إعدادات النظام", "الوصول للإعدادات" },
                    { new Guid("00000003-0000-7000-a000-000000000011"), "يمكنه إدارة الحسابات", "ادارة الحسابات" },
                    { new Guid("00000003-0000-7000-a000-000000000012"), "يمكنه إدارة الفواتير بجميع أنواعها (مبيعات، مشتريات، مرتجعات)", "ادارة الفواتير" },
                    { new Guid("00000003-0000-7000-a000-000000000013"), "يمكنه إنشاء فاتورة مبيعات جديدة", "فاتورة مبيعات جديدة" },
                    { new Guid("00000003-0000-7000-a000-000000000014"), "يمكنه إنشاء فاتورة مرتجع مبيعات", "فاتورة مرتجع مبيعات" },
                    { new Guid("00000003-0000-7000-a000-000000000015"), "يمكنه إنشاء فاتورة مشتريات جديدة", "فاتورة مشتريات جديدة" },
                    { new Guid("00000003-0000-7000-a000-000000000016"), "يمكنه إنشاء فاتورة مرتجع مشتريات", "فاتورة مرتجع مشتريات" },
                    { new Guid("00000003-0000-7000-a000-000000000017"), "يمكنه تعديل الفواتير", "تعديل الفاتورة" },
                    { new Guid("00000003-0000-7000-a000-000000000018"), "يمكنه إدارة الطلبات", "ادارة الطلبات" },
                    { new Guid("00000003-0000-7000-a000-000000000019"), "يمكنه إضافة عميل جديد (زبون أو مورد)", "اضافة عميل" },
                    { new Guid("00000003-0000-7000-a000-000000000020"), "يمكنه عرض تقرير حساب العميل", "تقرير حساب عميل" },
                    { new Guid("00000003-0000-7000-a000-000000000021"), "يمكنه تنفيذ عمليات قبض نقدي", "قبض نقدي" },
                    { new Guid("00000003-0000-7000-a000-000000000022"), "يمكنه تنفيذ عمليات دفع نقدي", "دفع نقدي" },
                    { new Guid("00000003-0000-7000-a000-000000000023"), "يمكنه الوصول إلى قسم التقارير", "الوصول للتقارير" },
                    { new Guid("00000003-0000-7000-a000-000000000024"), "يمكنه إدارة المنتجات والمخازن", "ادارة المنتجات والمخازن" },
                    { new Guid("00000003-0000-7000-a000-000000000025"), "يمكنه تحويل المنتجات بين المخازن", "تحويل بين المخازن" },
                    { new Guid("00000003-0000-7000-a000-000000000026"), "يمكنه معالجة الحسابات", "معالجة الحسابات" },
                    { new Guid("00000003-0000-7000-a000-000000000027"), "يمكنه إدارة حسابات البنوك", "ادارة حسابات البنوك" },
                    { new Guid("00000003-0000-7000-a000-000000000028"), "يمكنه إدارة حسابات المحافظ", "ادارة حسابات المحافظ" },
                    { new Guid("00000003-0000-7000-a000-000000000029"), "يمكنه إضافة مصروف جديد", "اضافة مصروف" },
                    { new Guid("00000003-0000-7000-a000-000000000030"), "يمكنه إضافة دخل جديد", "اضافة دخل" },
                    { new Guid("00000003-0000-7000-a000-000000000031"), "يمكنه استعراض المصروفات والإيرادات", "استعراض المصروفات والإيرادات" },
                    { new Guid("00000003-0000-7000-a000-000000000032"), "يمكنه إدارة الخزنة", "ادارة الخزنة" },
                    { new Guid("00000003-0000-7000-a000-000000000033"), "يمكنه إغلاق الورديه", "اغلاق الورديه" },
                    { new Guid("00000003-0000-7000-a000-000000000034"), "يمكنه فتح الورديه", "فتح الورديه" },
                    { new Guid("00000003-0000-7000-a000-000000000035"), "يمكنه استعراض معلومات الورديه", "استعراض الورديه" },
                    { new Guid("00000003-0000-7000-a000-000000000036"), "يمكنة مراجعة النقديه المستلمه والمدفوعه", "ادارة النقديه" },
                    { new Guid("00000003-0000-7000-a000-000000000037"), "اضافة خصم للفاتوره كلها", "خصم فاتورة" },
                    { new Guid("00000003-0000-7000-a000-000000000038"), "اضافة خصم لصنف واحد فقط", "خصم صنف" },
                    { new Guid("00000003-0000-7000-a000-000000000039"), "اضافة اضافى لاجمالى الفاتورة", "اضافة اضافى" },
                    { new Guid("00000003-0000-7000-a000-000000000040"), "يمكنة تغير سعر الببيع للصنف اثناء البيع", "تغير سعر البيع" },
                    { new Guid("00000003-0000-7000-a000-000000000041"), "يمكنة عرض تقاريرالعملاء والموردين", "تقارير العملاء والموردين" },
                    { new Guid("00000003-0000-7000-a000-000000000042"), "تعديل المنتجات والأسعار والحسابات (يُسمح به للإدارة الرئيسية فقط على الفروع السحابية)", "تعديل البيانات الرئيسية" }
                });

            migrationBuilder.InsertData(
                table: "ProductDefaults",
                columns: new[] { "Id", "Buy", "ExpirationDate", "Order", "ProductKind", "ReOrder", "RecessionPeriod", "Sale", "SrAccount", "SrIdSale", "SrIdSaleCost", "StIdSale", "StIdSaleCost", "StIdStock", "Unit" },
                values: new object[] { new Guid("00000007-0000-7000-a000-000000000001"), 0m, 0.0, 0.0, 0, 0.0, 5.0, 0m, new Guid("00000001-0000-7000-a000-000000000156"), new Guid("00000001-0000-7000-a000-000000003260"), new Guid("00000001-0000-7000-a000-000000000218"), new Guid("00000001-0000-7000-a000-000000000209"), new Guid("00000001-0000-7000-a000-000000000137"), new Guid("00000001-0000-7000-a000-000000000039"), "قطعة" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("00000002-0000-7000-a000-000000000001"), "Full system access", "Administrator" },
                    { new Guid("00000002-0000-7000-a000-000000000002"), "Can manage store operations", "Manager" },
                    { new Guid("00000002-0000-7000-a000-000000000003"), "Can operate POS terminal", "Cashier" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BranchId", "CompanyId", "CreatedAt", "IsActive", "LoginName", "Name", "PasswordHash" },
                values: new object[] { new Guid("00000005-0000-7000-a000-000000000001"), new Guid("00000009-0000-7000-a000-000000000001"), new Guid("00000010-0000-7000-a000-000000000001"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "admin", "admin", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=" });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("00000004-0000-7000-a000-000000000001"), new Guid("00000003-0000-7000-a000-000000000001"), new Guid("00000002-0000-7000-a000-000000000002") },
                    { new Guid("00000004-0000-7000-a000-000000000002"), new Guid("00000003-0000-7000-a000-000000000002"), new Guid("00000002-0000-7000-a000-000000000002") },
                    { new Guid("00000004-0000-7000-a000-000000000003"), new Guid("00000003-0000-7000-a000-000000000003"), new Guid("00000002-0000-7000-a000-000000000002") },
                    { new Guid("00000004-0000-7000-a000-000000000004"), new Guid("00000003-0000-7000-a000-000000000004"), new Guid("00000002-0000-7000-a000-000000000002") },
                    { new Guid("00000004-0000-7000-a000-000000000005"), new Guid("00000003-0000-7000-a000-000000000005"), new Guid("00000002-0000-7000-a000-000000000002") },
                    { new Guid("00000004-0000-7000-a000-000000000006"), new Guid("00000003-0000-7000-a000-000000000007"), new Guid("00000002-0000-7000-a000-000000000002") },
                    { new Guid("00000004-0000-7000-a000-000000000007"), new Guid("00000003-0000-7000-a000-000000000008"), new Guid("00000002-0000-7000-a000-000000000002") },
                    { new Guid("00000004-0000-7000-a000-000000000008"), new Guid("00000003-0000-7000-a000-000000000010"), new Guid("00000002-0000-7000-a000-000000000002") },
                    { new Guid("00000004-0000-7000-a000-000000000009"), new Guid("00000003-0000-7000-a000-000000000011"), new Guid("00000002-0000-7000-a000-000000000002") },
                    { new Guid("00000004-0000-7000-a000-000000000010"), new Guid("00000003-0000-7000-a000-000000000013"), new Guid("00000002-0000-7000-a000-000000000002") },
                    { new Guid("00000004-0000-7000-a000-000000000011"), new Guid("00000003-0000-7000-a000-000000000014"), new Guid("00000002-0000-7000-a000-000000000002") },
                    { new Guid("00000004-0000-7000-a000-000000000012"), new Guid("00000003-0000-7000-a000-000000000015"), new Guid("00000002-0000-7000-a000-000000000002") },
                    { new Guid("00000004-0000-7000-a000-000000000013"), new Guid("00000003-0000-7000-a000-000000000016"), new Guid("00000002-0000-7000-a000-000000000002") },
                    { new Guid("00000004-0000-7000-a000-000000000014"), new Guid("00000003-0000-7000-a000-000000000017"), new Guid("00000002-0000-7000-a000-000000000002") },
                    { new Guid("00000004-0000-7000-a000-000000000015"), new Guid("00000003-0000-7000-a000-000000000021"), new Guid("00000002-0000-7000-a000-000000000002") },
                    { new Guid("00000004-0000-7000-a000-000000000016"), new Guid("00000003-0000-7000-a000-000000000022"), new Guid("00000002-0000-7000-a000-000000000002") },
                    { new Guid("00000004-0000-7000-a000-000000000017"), new Guid("00000003-0000-7000-a000-000000000023"), new Guid("00000002-0000-7000-a000-000000000002") },
                    { new Guid("00000004-0000-7000-a000-000000000018"), new Guid("00000003-0000-7000-a000-000000000033"), new Guid("00000002-0000-7000-a000-000000000002") },
                    { new Guid("00000004-0000-7000-a000-000000000019"), new Guid("00000003-0000-7000-a000-000000000034"), new Guid("00000002-0000-7000-a000-000000000002") },
                    { new Guid("00000004-0000-7000-a000-000000000020"), new Guid("00000003-0000-7000-a000-000000000035"), new Guid("00000002-0000-7000-a000-000000000002") },
                    { new Guid("00000004-0000-7000-a000-000000000021"), new Guid("00000003-0000-7000-a000-000000000001"), new Guid("00000002-0000-7000-a000-000000000003") },
                    { new Guid("00000004-0000-7000-a000-000000000022"), new Guid("00000003-0000-7000-a000-000000000002"), new Guid("00000002-0000-7000-a000-000000000003") },
                    { new Guid("00000004-0000-7000-a000-000000000023"), new Guid("00000003-0000-7000-a000-000000000013"), new Guid("00000002-0000-7000-a000-000000000003") },
                    { new Guid("00000004-0000-7000-a000-000000000024"), new Guid("00000003-0000-7000-a000-000000000014"), new Guid("00000002-0000-7000-a000-000000000003") },
                    { new Guid("00000004-0000-7000-a000-000000000025"), new Guid("00000003-0000-7000-a000-000000000021"), new Guid("00000002-0000-7000-a000-000000000003") },
                    { new Guid("00000004-0000-7000-a000-000000000026"), new Guid("00000003-0000-7000-a000-000000000022"), new Guid("00000002-0000-7000-a000-000000000003") },
                    { new Guid("00000004-0000-7000-a000-000000000027"), new Guid("00000003-0000-7000-a000-000000000033"), new Guid("00000002-0000-7000-a000-000000000003") },
                    { new Guid("00000004-0000-7000-a000-000000000028"), new Guid("00000003-0000-7000-a000-000000000034"), new Guid("00000002-0000-7000-a000-000000000003") },
                    { new Guid("00000004-0000-7000-a000-000000000029"), new Guid("00000003-0000-7000-a000-000000000035"), new Guid("00000002-0000-7000-a000-000000000003") },
                    { new Guid("00000004-0000-7000-a000-000000000030"), new Guid("00000003-0000-7000-a000-000000000001"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000031"), new Guid("00000003-0000-7000-a000-000000000002"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000032"), new Guid("00000003-0000-7000-a000-000000000003"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000033"), new Guid("00000003-0000-7000-a000-000000000004"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000034"), new Guid("00000003-0000-7000-a000-000000000005"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000035"), new Guid("00000003-0000-7000-a000-000000000006"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000036"), new Guid("00000003-0000-7000-a000-000000000007"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000037"), new Guid("00000003-0000-7000-a000-000000000008"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000038"), new Guid("00000003-0000-7000-a000-000000000009"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000039"), new Guid("00000003-0000-7000-a000-000000000010"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000040"), new Guid("00000003-0000-7000-a000-000000000011"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000041"), new Guid("00000003-0000-7000-a000-000000000012"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000042"), new Guid("00000003-0000-7000-a000-000000000013"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000043"), new Guid("00000003-0000-7000-a000-000000000014"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000044"), new Guid("00000003-0000-7000-a000-000000000015"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000045"), new Guid("00000003-0000-7000-a000-000000000016"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000046"), new Guid("00000003-0000-7000-a000-000000000017"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000047"), new Guid("00000003-0000-7000-a000-000000000018"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000048"), new Guid("00000003-0000-7000-a000-000000000019"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000049"), new Guid("00000003-0000-7000-a000-000000000020"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000050"), new Guid("00000003-0000-7000-a000-000000000021"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000051"), new Guid("00000003-0000-7000-a000-000000000022"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000052"), new Guid("00000003-0000-7000-a000-000000000023"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000053"), new Guid("00000003-0000-7000-a000-000000000024"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000054"), new Guid("00000003-0000-7000-a000-000000000025"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000055"), new Guid("00000003-0000-7000-a000-000000000026"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000056"), new Guid("00000003-0000-7000-a000-000000000027"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000057"), new Guid("00000003-0000-7000-a000-000000000028"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000058"), new Guid("00000003-0000-7000-a000-000000000029"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000059"), new Guid("00000003-0000-7000-a000-000000000030"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000060"), new Guid("00000003-0000-7000-a000-000000000031"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000061"), new Guid("00000003-0000-7000-a000-000000000032"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000062"), new Guid("00000003-0000-7000-a000-000000000033"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000063"), new Guid("00000003-0000-7000-a000-000000000034"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000064"), new Guid("00000003-0000-7000-a000-000000000035"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000065"), new Guid("00000003-0000-7000-a000-000000000036"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000066"), new Guid("00000003-0000-7000-a000-000000000037"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000067"), new Guid("00000003-0000-7000-a000-000000000038"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000068"), new Guid("00000003-0000-7000-a000-000000000039"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000069"), new Guid("00000003-0000-7000-a000-000000000040"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000070"), new Guid("00000003-0000-7000-a000-000000000041"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000071"), new Guid("00000003-0000-7000-a000-000000000042"), new Guid("00000002-0000-7000-a000-000000000001") }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[] { new Guid("00000006-0000-7000-a000-000000000001"), new Guid("00000002-0000-7000-a000-000000000001"), new Guid("00000005-0000-7000-a000-000000000001") });

            migrationBuilder.CreateIndex(
                name: "IX_Banks_AccountId",
                table: "Banks",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_CurrencyId",
                table: "Banks",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_UserId",
                table: "Banks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_BankId",
                table: "BankTransactions",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_CurrencyId",
                table: "BankTransactions",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_CustomerId",
                table: "BankTransactions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_UserId",
                table: "BankTransactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Barcodes_UnitOfMeasureId",
                table: "Barcodes",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_BillEntries_BillId",
                table: "BillEntries",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillEntries_BranchId",
                table: "BillEntries",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BillEntries_CustomerId",
                table: "BillEntries",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BillEntries_ProductId",
                table: "BillEntries",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BillEntries_UnitId",
                table: "BillEntries",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_BillEntries_WarehouseId",
                table: "BillEntries",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BranchId",
                table: "Bills",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CustomerId",
                table: "Bills",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_UserId",
                table: "Bills",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_CompanyId",
                table: "Branches",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Cashes_BankId",
                table: "Cashes",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Cashes_BranchId",
                table: "Cashes",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Cashes_CurrencyId",
                table: "Cashes",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Cashes_CustomerId",
                table: "Cashes",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cashes_EWalletId",
                table: "Cashes",
                column: "EWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Cashes_TreasuryId",
                table: "Cashes",
                column: "TreasuryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cashes_UserId",
                table: "Cashes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CurrencyId",
                table: "Companies",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_LogoId",
                table: "Companies",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AreaId",
                table: "Customers",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_GroupId",
                table: "Customers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ImageId",
                table: "Customers",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTransactions_CustomerId",
                table: "CustomerTransactions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTransactions_UserId",
                table: "CustomerTransactions",
                column: "UserId");

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
                name: "IX_EWallets_AccountId",
                table: "EWallets",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_EWallets_BranchId",
                table: "EWallets",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_EWalletTransactions_CustomerId",
                table: "EWalletTransactions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_EWalletTransactions_EWalletId",
                table: "EWalletTransactions",
                column: "EWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_EWalletTransactions_UserId",
                table: "EWalletTransactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAdjustments_BranchId",
                table: "InventoryAdjustments",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_AccountId",
                table: "JournalEntries",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_BranchId",
                table: "JournalEntries",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_CustomerId",
                table: "JournalEntries",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderFulfillments_BranchId",
                table: "OrderFulfillments",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderFulfillments_OrderEntryId",
                table: "OrderFulfillments",
                column: "OrderEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderFulfillments_ReSaleEntryId",
                table: "OrderFulfillments",
                column: "ReSaleEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderFulfillments_SaleEntryId",
                table: "OrderFulfillments",
                column: "SaleEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOpeningBalances_BranchId",
                table: "ProductOpeningBalances",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOpeningBalances_ProductId",
                table: "ProductOpeningBalances",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOpeningBalances_WarehouseId",
                table: "ProductOpeningBalances",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_GroupId",
                table: "Products",
                column: "GroupId");

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

            migrationBuilder.CreateIndex(
                name: "IX_RevenueExpenses_AccountId",
                table: "RevenueExpenses",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RevenueExpenses_BankId",
                table: "RevenueExpenses",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_RevenueExpenses_BranchId",
                table: "RevenueExpenses",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_RevenueExpenses_CurrencyId",
                table: "RevenueExpenses",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_RevenueExpenses_EwalletId",
                table: "RevenueExpenses",
                column: "EwalletId");

            migrationBuilder.CreateIndex(
                name: "IX_RevenueExpenses_TreasuryId",
                table: "RevenueExpenses",
                column: "TreasuryId");

            migrationBuilder.CreateIndex(
                name: "IX_RevenueExpenses_UserId",
                table: "RevenueExpenses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Treasuries_BranchId",
                table: "Treasuries",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_TreasuriesTransactions_BranchId",
                table: "TreasuriesTransactions",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_TreasuriesTransactions_TreasuryId",
                table: "TreasuriesTransactions",
                column: "TreasuryId");

            migrationBuilder.CreateIndex(
                name: "IX_TreasuriesTransactions_UserId",
                table: "TreasuriesTransactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasures_ProductId",
                table: "UnitOfMeasures",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_BranchId",
                table: "Warehouses",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehousesProductInventories_BranchId",
                table: "WarehousesProductInventories",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehousesProductInventories_ProductId",
                table: "WarehousesProductInventories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehousesProductInventories_WarehouseId",
                table: "WarehousesProductInventories",
                column: "WarehouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountOperands");

            migrationBuilder.DropTable(
                name: "BankTransactions");

            migrationBuilder.DropTable(
                name: "Barcodes");

            migrationBuilder.DropTable(
                name: "Cashes");

            migrationBuilder.DropTable(
                name: "CustomerTransactions");

            migrationBuilder.DropTable(
                name: "DailyProductCosts");

            migrationBuilder.DropTable(
                name: "EWalletTransactions");

            migrationBuilder.DropTable(
                name: "InventoryAdjustments");

            migrationBuilder.DropTable(
                name: "JournalEntries");

            migrationBuilder.DropTable(
                name: "OrderFulfillments");

            migrationBuilder.DropTable(
                name: "ProductDefaults");

            migrationBuilder.DropTable(
                name: "ProductOpeningBalances");

            migrationBuilder.DropTable(
                name: "ProductTransactions");

            migrationBuilder.DropTable(
                name: "RevenueExpenses");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "TreasuriesTransactions");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "WarehousesProductInventories");

            migrationBuilder.DropTable(
                name: "BillEntries");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "EWallets");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Treasuries");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "UnitOfMeasures");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
