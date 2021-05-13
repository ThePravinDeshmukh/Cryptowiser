using Microsoft.EntityFrameworkCore.Migrations;

namespace Cryptowiser.Models.Migrations
{
    public partial class RenameColumnUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "Users",
                newName: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "EmailAddress");
        }
    }
}
