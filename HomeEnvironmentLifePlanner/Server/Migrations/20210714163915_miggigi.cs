using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeEnvironmentLifePlanner.Server.Migrations
{
    public partial class miggigi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BsS_Description",
                table: "BankStatmentSubPositions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BsS_Description",
                table: "BankStatmentSubPositions");
        }
    }
}
