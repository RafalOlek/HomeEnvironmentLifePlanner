﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeEnvironmentLifePlanner.Server.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AcC_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcC_ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcC_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AcC_Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankStatmentHeaders",
                columns: table => new
                {
                    BsH_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BsH_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BsH_CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BsH_DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BsH_DateTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankStatmentHeaders", x => x.BsH_Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryTypes",
                columns: table => new
                {
                    CtY_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CtY_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CtY_Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTypes", x => x.CtY_Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractorGroups",
                columns: table => new
                {
                    CtG_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CtG_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CtG_ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorGroups", x => x.CtG_Id);
                    table.ForeignKey(
                        name: "FK_ContractorGroups_ContractorGroups_CtG_ParentId",
                        column: x => x.CtG_ParentId,
                        principalTable: "ContractorGroups",
                        principalColumn: "CtG_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    CuR_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CuR_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.CuR_Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "ProductGroups",
                columns: table => new
                {
                    PrG_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrG_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrG_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrG_ParentId = table.Column<int>(type: "int", nullable: true),
                    ProductGroup1PrG_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroups", x => x.PrG_Id);
                    table.ForeignKey(
                        name: "FK_ProductGroups_ProductGroups_ProductGroup1PrG_Id",
                        column: x => x.ProductGroup1PrG_Id,
                        principalTable: "ProductGroups",
                        principalColumn: "PrG_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    PyT_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PyT_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PyT_ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PyT_ACCID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.PyT_Id);
                    table.ForeignKey(
                        name: "FK_PaymentTypes_Accounts_PyT_ACCID",
                        column: x => x.PyT_ACCID,
                        principalTable: "Accounts",
                        principalColumn: "AcC_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CaT_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaT_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaT_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaT_ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaT_ParentId = table.Column<int>(type: "int", nullable: true),
                    CaT_CTYID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CaT_Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_CaT_ParentId",
                        column: x => x.CaT_ParentId,
                        principalTable: "Categories",
                        principalColumn: "CaT_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_CategoryTypes_CaT_CTYID",
                        column: x => x.CaT_CTYID,
                        principalTable: "CategoryTypes",
                        principalColumn: "CtY_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contractors",
                columns: table => new
                {
                    CtR_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CtR_Acronym = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CtR_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CtR_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CtR_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CtR_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CtR_ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CtR_CTGID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractors", x => x.CtR_Id);
                    table.ForeignKey(
                        name: "FK_Contractors_ContractorGroups_CtR_CTGID",
                        column: x => x.CtR_CTGID,
                        principalTable: "ContractorGroups",
                        principalColumn: "CtG_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    PrD_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrD_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrD_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrD_PRGID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.PrD_Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductGroups_PrD_PRGID",
                        column: x => x.PrD_PRGID,
                        principalTable: "ProductGroups",
                        principalColumn: "PrG_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankStatmentPositions",
                columns: table => new
                {
                    BsP_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BsP_BSHID = table.Column<int>(type: "int", nullable: false),
                    BsP_ExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BsP_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BsP_CURID = table.Column<int>(type: "int", nullable: false),
                    BsP_SenderReceiver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BsP_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BsP_TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BsP_IsImportedToTransactions = table.Column<bool>(type: "bit", nullable: false),
                    Bsp_IsPreparedToImport = table.Column<bool>(type: "bit", nullable: false),
                    BsP_ImportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BsP_RecommendedContractorId = table.Column<int>(type: "int", nullable: true),
                    BsP_RecommendedAccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankStatmentPositions", x => x.BsP_Id);
                    table.ForeignKey(
                        name: "FK_BankStatmentPositions_Accounts_BsP_RecommendedAccountId",
                        column: x => x.BsP_RecommendedAccountId,
                        principalTable: "Accounts",
                        principalColumn: "AcC_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankStatmentPositions_BankStatmentHeaders_BsP_BSHID",
                        column: x => x.BsP_BSHID,
                        principalTable: "BankStatmentHeaders",
                        principalColumn: "BsH_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankStatmentPositions_Contractors_BsP_RecommendedContractorId",
                        column: x => x.BsP_RecommendedContractorId,
                        principalTable: "Contractors",
                        principalColumn: "CtR_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankStatmentPositions_Currencies_BsP_CURID",
                        column: x => x.BsP_CURID,
                        principalTable: "Currencies",
                        principalColumn: "CuR_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPrices",
                columns: table => new
                {
                    PrP_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrP_PRDID = table.Column<int>(type: "int", nullable: false),
                    PrP_Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrP_CTRID = table.Column<int>(type: "int", nullable: false),
                    PrP_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPrices", x => x.PrP_Id);
                    table.ForeignKey(
                        name: "FK_ProductPrices_Contractors_PrP_CTRID",
                        column: x => x.PrP_CTRID,
                        principalTable: "Contractors",
                        principalColumn: "CtR_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductPrices_Products_PrP_PRDID",
                        column: x => x.PrP_PRDID,
                        principalTable: "Products",
                        principalColumn: "PrD_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankStatmentSubPositions",
                columns: table => new
                {
                    BsS_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BsS_BSPID = table.Column<int>(type: "int", nullable: false),
                    BsS_CATID = table.Column<int>(type: "int", nullable: true),
                    BsS_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankStatmentSubPositions", x => x.BsS_Id);
                    table.ForeignKey(
                        name: "FK_BankStatmentSubPositions_BankStatmentPositions_BsS_BSPID",
                        column: x => x.BsS_BSPID,
                        principalTable: "BankStatmentPositions",
                        principalColumn: "BsP_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankStatmentSubPositions_Categories_BsS_CATID",
                        column: x => x.BsS_CATID,
                        principalTable: "Categories",
                        principalColumn: "CaT_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionHeaders",
                columns: table => new
                {
                    TrH_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrH_BSPID = table.Column<int>(type: "int", nullable: true),
                    TrH_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrH_CTRID = table.Column<int>(type: "int", nullable: false),
                    TrH_CURID = table.Column<int>(type: "int", nullable: false),
                    TrH_CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrH_ExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHeaders", x => x.TrH_Id);
                    table.ForeignKey(
                        name: "FK_TransactionHeaders_BankStatmentPositions_TrH_BSPID",
                        column: x => x.TrH_BSPID,
                        principalTable: "BankStatmentPositions",
                        principalColumn: "BsP_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionHeaders_Contractors_TrH_CTRID",
                        column: x => x.TrH_CTRID,
                        principalTable: "Contractors",
                        principalColumn: "CtR_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionHeaders_Currencies_TrH_CURID",
                        column: x => x.TrH_CURID,
                        principalTable: "Currencies",
                        principalColumn: "CuR_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionPositions",
                columns: table => new
                {
                    TrP_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrP_TRHID = table.Column<int>(type: "int", nullable: false),
                    TrP_Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TrP_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrP_Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TrP_PRDID = table.Column<int>(type: "int", nullable: true),
                    TrP_Unit = table.Column<int>(type: "int", nullable: true),
                    TrP_BSSID = table.Column<int>(type: "int", nullable: true),
                    TrP_CATID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionPositions", x => x.TrP_Id);
                    table.ForeignKey(
                        name: "FK_TransactionPositions_BankStatmentSubPositions_TrP_BSSID",
                        column: x => x.TrP_BSSID,
                        principalTable: "BankStatmentSubPositions",
                        principalColumn: "BsS_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionPositions_Categories_TrP_CATID",
                        column: x => x.TrP_CATID,
                        principalTable: "Categories",
                        principalColumn: "CaT_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionPositions_TransactionHeaders_TrP_TRHID",
                        column: x => x.TrP_TRHID,
                        principalTable: "TransactionHeaders",
                        principalColumn: "TrH_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AcC_Id", "AcC_Name", "AcC_ReferenceNumber" },
                values: new object[,]
                {
                    { 1, "Gotówka_Rafał", " " },
                    { 2, "Gotówka_Paulina", " " },
                    { 3, "Gotówka_Wakacje", " " },
                    { 4, "Konto", " " }
                });

            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "CtY_Id", "CtY_Name", "CtY_Value" },
                values: new object[,]
                {
                    { 1, "Przychód/Wydatek", 0 },
                    { 2, "Przychód", 1 },
                    { 3, "Wydatek", 2 }
                });

            migrationBuilder.InsertData(
                table: "ContractorGroups",
                columns: new[] { "CtG_Id", "CtG_Name", "CtG_ParentId" },
                values: new object[] { 1, "Grupa Główna", null });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "CuR_Id", "CuR_Name" },
                values: new object[,]
                {
                    { 1, "PLN" },
                    { 2, "EUR" },
                    { 3, "GBP" }
                });

            migrationBuilder.InsertData(
                table: "ProductGroups",
                columns: new[] { "PrG_Id", "PrG_Code", "PrG_Name", "PrG_ParentId", "ProductGroup1PrG_Id" },
                values: new object[] { 1, "GŁÓWNA", "Grupa Główna", null, null });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CaT_Id", "CaT_CTYID", "CaT_Description", "CaT_Name", "CaT_ParentId", "CaT_ReferenceNumber" },
                values: new object[] { 1, 1, "Grupa o najwyższym poziomie", "Grupa Główna", null, "<BRAK>" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CaT_Id", "CaT_CTYID", "CaT_Description", "CaT_Name", "CaT_ParentId", "CaT_ReferenceNumber" },
                values: new object[,]
                {
                    { 2, 2, "Wszystkie źródła przychodów", "Całkowite przychody", 1, "<BRAK>" },
                    { 3, 3, "", "Jedzenie", 1, "<BRAK>" },
                    { 4, 3, "", "Mieszkania", 1, "<BRAK>" },
                    { 5, 2, "", "Transport", 1, "<BRAK>" },
                    { 6, 3, "", "Telekomunikacja", 1, "<BRAK>" },
                    { 7, 3, "", "Opieka zdrowotna", 1, "<BRAK>" },
                    { 8, 2, "", "Ubrania", 1, "<BRAK>" },
                    { 9, 2, "", "Higiena", 1, "<BRAK>" },
                    { 10, 2, "", "Rozrywka", 1, "<BRAK>" },
                    { 11, 2, "", "Inne wydatki", 1, "<BRAK>" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CaT_Id", "CaT_CTYID", "CaT_Description", "CaT_Name", "CaT_ParentId", "CaT_ReferenceNumber" },
                values: new object[,]
                {
                    { 12, 2, "Wypłata bez premii", "Pensja Rafał", 2, "<BRAK>" },
                    { 13, 2, "Wypłata bez premii", "Pensja Paulina", 2, "<BRAK>" },
                    { 14, 2, "Premia roczna (tzw świąteczna)", "Premia Roczna Rafał", 2, "<BRAK>" },
                    { 15, 2, "Trzynastka", "Premia Roczna Paulina", 2, "<BRAK>" },
                    { 16, 2, "Kwota tylko wynajmu ( bez opłat za rachunki)", "Wynajem Mieszkania Szarych Szeregów", 2, "<BRAK>" },
                    { 17, 2, "Wpłata najemcy na poczet rachunku za prąd", "Wpłata Najmcy Prąd", 2, "<BRAK>" },
                    { 18, 2, "Wpłata najemcy na poczet czynszu( wraz z zaliczką na ogrzewanie i wode)", "Wpłata Najmcy Czynsz", 2, "<BRAK>" },
                    { 19, 2, " ", "Wynajem Garażu", 2, "<BRAK>" },
                    { 20, 2, "500+ ", "Pięćset plus", 2, "<BRAK>" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BankStatmentPositions_BsP_BSHID",
                table: "BankStatmentPositions",
                column: "BsP_BSHID");

            migrationBuilder.CreateIndex(
                name: "IX_BankStatmentPositions_BsP_CURID",
                table: "BankStatmentPositions",
                column: "BsP_CURID");

            migrationBuilder.CreateIndex(
                name: "IX_BankStatmentPositions_BsP_RecommendedAccountId",
                table: "BankStatmentPositions",
                column: "BsP_RecommendedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BankStatmentPositions_BsP_RecommendedContractorId",
                table: "BankStatmentPositions",
                column: "BsP_RecommendedContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_BankStatmentSubPositions_BsS_BSPID",
                table: "BankStatmentSubPositions",
                column: "BsS_BSPID");

            migrationBuilder.CreateIndex(
                name: "IX_BankStatmentSubPositions_BsS_CATID",
                table: "BankStatmentSubPositions",
                column: "BsS_CATID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CaT_CTYID",
                table: "Categories",
                column: "CaT_CTYID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CaT_ParentId",
                table: "Categories",
                column: "CaT_ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorGroups_CtG_ParentId",
                table: "ContractorGroups",
                column: "CtG_ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_CtR_CTGID",
                table: "Contractors",
                column: "CtR_CTGID");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTypes_PyT_ACCID",
                table: "PaymentTypes",
                column: "PyT_ACCID");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroups_ProductGroup1PrG_Id",
                table: "ProductGroups",
                column: "ProductGroup1PrG_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_PrP_CTRID",
                table: "ProductPrices",
                column: "PrP_CTRID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_PrP_PRDID",
                table: "ProductPrices",
                column: "PrP_PRDID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PrD_PRGID",
                table: "Products",
                column: "PrD_PRGID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHeaders_TrH_BSPID",
                table: "TransactionHeaders",
                column: "TrH_BSPID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHeaders_TrH_CTRID",
                table: "TransactionHeaders",
                column: "TrH_CTRID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHeaders_TrH_CURID",
                table: "TransactionHeaders",
                column: "TrH_CURID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionPositions_TrP_BSSID",
                table: "TransactionPositions",
                column: "TrP_BSSID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionPositions_TrP_CATID",
                table: "TransactionPositions",
                column: "TrP_CATID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionPositions_TrP_TRHID",
                table: "TransactionPositions",
                column: "TrP_TRHID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "ProductPrices");

            migrationBuilder.DropTable(
                name: "TransactionPositions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "BankStatmentSubPositions");

            migrationBuilder.DropTable(
                name: "TransactionHeaders");

            migrationBuilder.DropTable(
                name: "ProductGroups");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "BankStatmentPositions");

            migrationBuilder.DropTable(
                name: "CategoryTypes");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "BankStatmentHeaders");

            migrationBuilder.DropTable(
                name: "Contractors");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "ContractorGroups");
        }
    }
}
