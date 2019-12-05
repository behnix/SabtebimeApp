using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class AddServicesToSettingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OfferText",
                table: "Settings",
                maxLength: 1200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfferTitle",
                table: "Settings",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecomendedText",
                table: "Settings",
                maxLength: 1200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecomendedTitle",
                table: "Settings",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Service14Url",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Service1Icon",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Service1Title",
                table: "Settings",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Service1Url",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Service2Icon",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Service2Title",
                table: "Settings",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Service2Url",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Service3Icon",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Service3Title",
                table: "Settings",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Service3Url",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Service4Icon",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Service4Title",
                table: "Settings",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfferText",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "OfferTitle",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "RecomendedText",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "RecomendedTitle",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Service14Url",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Service1Icon",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Service1Title",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Service1Url",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Service2Icon",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Service2Title",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Service2Url",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Service3Icon",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Service3Title",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Service3Url",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Service4Icon",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Service4Title",
                table: "Settings");
        }
    }
}
