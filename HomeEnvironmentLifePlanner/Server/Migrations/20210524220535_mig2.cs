using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeEnvironmentLifePlanner.Server.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroups_ProductGroups_ProductGroup1PrG_Id",
                table: "ProductGroups");

            migrationBuilder.DropIndex(
                name: "IX_ProductGroups_ProductGroup1PrG_Id",
                table: "ProductGroups");

            migrationBuilder.DropColumn(
                name: "ProductGroup1PrG_Id",
                table: "ProductGroups");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroups_PrG_ParentId",
                table: "ProductGroups",
                column: "PrG_ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroups_ProductGroups_PrG_ParentId",
                table: "ProductGroups",
                column: "PrG_ParentId",
                principalTable: "ProductGroups",
                principalColumn: "PrG_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroups_ProductGroups_PrG_ParentId",
                table: "ProductGroups");

            migrationBuilder.DropIndex(
                name: "IX_ProductGroups_PrG_ParentId",
                table: "ProductGroups");

            migrationBuilder.AddColumn<int>(
                name: "ProductGroup1PrG_Id",
                table: "ProductGroups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroups_ProductGroup1PrG_Id",
                table: "ProductGroups",
                column: "ProductGroup1PrG_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroups_ProductGroups_ProductGroup1PrG_Id",
                table: "ProductGroups",
                column: "ProductGroup1PrG_Id",
                principalTable: "ProductGroups",
                principalColumn: "PrG_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
