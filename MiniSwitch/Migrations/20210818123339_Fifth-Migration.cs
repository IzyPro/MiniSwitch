using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniSwitch.Migrations
{
    public partial class FifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CardPAN",
                table: "Routes",
                newName: "BIN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BIN",
                table: "Routes",
                newName: "CardPAN");
        }
    }
}
