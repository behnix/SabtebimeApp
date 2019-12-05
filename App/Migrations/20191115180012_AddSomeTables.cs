using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class AddSomeTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Subject = table.Column<string>(maxLength: 200, nullable: false),
                    Text = table.Column<string>(nullable: false),
                    PostedOn = table.Column<DateTime>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false),
                    Answer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faqs",
                columns: table => new
                {
                    FaqId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FaqQuestion = table.Column<string>(maxLength: 500, nullable: false),
                    FaqAnswer = table.Column<string>(nullable: true),
                    FaqIsPublished = table.Column<bool>(nullable: false),
                    FaqName = table.Column<string>(maxLength: 100, nullable: false),
                    FaqEmail = table.Column<string>(maxLength: 100, nullable: true),
                    FaqLike = table.Column<int>(nullable: false),
                    FaqDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faqs", x => x.FaqId);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    PageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PageTitle = table.Column<string>(nullable: false),
                    PageTitleInBrowser = table.Column<string>(maxLength: 100, nullable: false),
                    PageDescriptionForSearchEngines = table.Column<string>(maxLength: 150, nullable: false),
                    PageContent = table.Column<string>(nullable: true),
                    PageCreateDate = table.Column<DateTime>(nullable: false),
                    PageUpdateDate = table.Column<DateTime>(nullable: true),
                    PageVisit = table.Column<int>(nullable: false),
                    PageShortUrl = table.Column<string>(nullable: true),
                    RelatedPostGroup = table.Column<int>(nullable: false),
                    QuantityPostInPage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.PageId);
                });

            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    SliderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SliderName = table.Column<string>(maxLength: 100, nullable: false),
                    SliderImage = table.Column<string>(nullable: true),
                    SliderTitle1 = table.Column<string>(maxLength: 50, nullable: true),
                    SliderTitle2 = table.Column<string>(maxLength: 50, nullable: true),
                    SliderTitle3 = table.Column<string>(maxLength: 50, nullable: true),
                    SliderButtonText = table.Column<string>(maxLength: 50, nullable: true),
                    SliderButtonLink = table.Column<string>(maxLength: 200, nullable: true),
                    SliderType = table.Column<byte>(nullable: false),
                    SliderStartTime = table.Column<DateTime>(nullable: false),
                    SliderEndTime = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.SliderId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Faqs");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Sliders");
        }
    }
}
