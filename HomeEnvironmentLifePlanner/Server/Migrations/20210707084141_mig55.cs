using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeEnvironmentLifePlanner.Server.Migrations
{
    public partial class mig55 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShoppingListHeaders",
                columns: table => new
                {
                    SlH_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SlH_Number = table.Column<int>(type: "int", nullable: false),
                    SlH_Month = table.Column<int>(type: "int", nullable: false),
                    SlH_Year = table.Column<int>(type: "int", nullable: false),
                    SlH_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SlH_BSPID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingListHeaders", x => x.SlH_Id);
                    table.ForeignKey(
                        name: "FK_ShoppingListHeaders_BankStatmentPositions_SlH_BSPID",
                        column: x => x.SlH_BSPID,
                        principalTable: "BankStatmentPositions",
                        principalColumn: "BsP_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingListPositions",
                columns: table => new
                {
                    SlP_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SlP_SLHID = table.Column<int>(type: "int", nullable: false),
                    SlP_PRDID = table.Column<int>(type: "int", nullable: false),
                    SlP_AssumedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SlP_RealizedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SlP_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SlP_BSSID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingListPositions", x => x.SlP_Id);
                    table.ForeignKey(
                        name: "FK_ShoppingListPositions_BankStatmentSubPositions_SlP_BSSID",
                        column: x => x.SlP_BSSID,
                        principalTable: "BankStatmentSubPositions",
                        principalColumn: "BsS_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingListPositions_ShoppingListHeaders_SlP_SLHID",
                        column: x => x.SlP_SLHID,
                        principalTable: "ShoppingListHeaders",
                        principalColumn: "SlH_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListHeaders_SlH_BSPID",
                table: "ShoppingListHeaders",
                column: "SlH_BSPID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListPositions_SlP_BSSID",
                table: "ShoppingListPositions",
                column: "SlP_BSSID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListPositions_SlP_SLHID",
                table: "ShoppingListPositions",
                column: "SlP_SLHID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingListPositions");

            migrationBuilder.DropTable(
                name: "ShoppingListHeaders");
        }
    }
}
