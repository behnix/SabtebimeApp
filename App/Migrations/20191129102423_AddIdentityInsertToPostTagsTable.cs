using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class AddIdentityInsertToPostTagsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TermsTitle",
                table: "Settings",
                maxLength: 65,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 65,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Slogan2",
                table: "Settings",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "Slogan1",
                table: "Settings",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "FaqTitle",
                table: "Settings",
                maxLength: 65,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 65,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FaqDescription",
                table: "Settings",
                maxLength: 155,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 155,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactTitle",
                table: "Settings",
                maxLength: 65,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 65,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactDescription",
                table: "Settings",
                maxLength: 155,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 155,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AboutUsTitle",
                table: "Settings",
                maxLength: 65,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 65,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AboutUsDescription",
                table: "Settings",
                maxLength: 155,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 155,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TermsTitle",
                table: "Settings",
                maxLength: 65,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 65);

            migrationBuilder.AlterColumn<string>(
                name: "Slogan2",
                table: "Settings",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Slogan1",
                table: "Settings",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FaqTitle",
                table: "Settings",
                maxLength: 65,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 65);

            migrationBuilder.AlterColumn<string>(
                name: "FaqDescription",
                table: "Settings",
                maxLength: 155,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 155);

            migrationBuilder.AlterColumn<string>(
                name: "ContactTitle",
                table: "Settings",
                maxLength: 65,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 65);

            migrationBuilder.AlterColumn<string>(
                name: "ContactDescription",
                table: "Settings",
                maxLength: 155,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 155);

            migrationBuilder.AlterColumn<string>(
                name: "AboutUsTitle",
                table: "Settings",
                maxLength: 65,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 65);

            migrationBuilder.AlterColumn<string>(
                name: "AboutUsDescription",
                table: "Settings",
                maxLength: 155,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 155);
        }
    }
}
