using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class RenameService14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Service14Url",
                table: "Settings",
                newName: "Service4Url");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Service4Url",
                table: "Settings",
                newName: "Service14Url");
        }
    }
}
