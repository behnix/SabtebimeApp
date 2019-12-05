using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class AddSettingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    SettingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SiteName = table.Column<string>(maxLength: 65, nullable: false),
                    SiteNameInEnglish = table.Column<string>(nullable: true),
                    SiteDescription = table.Column<string>(maxLength: 155, nullable: false),
                    SiteTel = table.Column<string>(maxLength: 100, nullable: true),
                    SiteAddress = table.Column<string>(maxLength: 500, nullable: true),
                    SiteEmail = table.Column<string>(nullable: false),
                    SiteEmailPassword = table.Column<string>(maxLength: 100, nullable: true),
                    SiteSmtp = table.Column<string>(maxLength: 100, nullable: true),
                    SmtpPort = table.Column<int>(nullable: false),
                    SiteSmsNumber = table.Column<string>(maxLength: 100, nullable: true),
                    SiteSmsSigniture = table.Column<string>(maxLength: 100, nullable: true),
                    SiteTelegramId = table.Column<string>(maxLength: 100, nullable: true),
                    SiteInstagramId = table.Column<string>(maxLength: 100, nullable: true),
                    SiteTwitterId = table.Column<string>(maxLength: 100, nullable: true),
                    SiteFacebookId = table.Column<string>(maxLength: 100, nullable: true),
                    SiteCopyRightText = table.Column<string>(maxLength: 300, nullable: false),
                    Index = table.Column<string>(nullable: true),
                    Slogan1 = table.Column<string>(maxLength: 300, nullable: false),
                    Slogan2 = table.Column<string>(maxLength: 300, nullable: false),
                    ContactTitle = table.Column<string>(maxLength: 65, nullable: true),
                    ContactDescription = table.Column<string>(maxLength: 155, nullable: true),
                    TermsTitle = table.Column<string>(maxLength: 65, nullable: true),
                    TermsDescription = table.Column<string>(maxLength: 155, nullable: true),
                    Terms = table.Column<string>(nullable: true),
                    ContactWorkHour = table.Column<string>(maxLength: 100, nullable: true),
                    FaqTitle = table.Column<string>(maxLength: 65, nullable: true),
                    FaqDescription = table.Column<string>(maxLength: 155, nullable: true),
                    FaqText = table.Column<string>(nullable: true),
                    AboutUsTitle = table.Column<string>(maxLength: 65, nullable: true),
                    AboutUsDescription = table.Column<string>(maxLength: 155, nullable: true),
                    AboutUs = table.Column<string>(nullable: true),
                    QuantityPostInIndex = table.Column<int>(nullable: false),
                    MerchantId = table.Column<string>(maxLength: 50, nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    Style = table.Column<string>(nullable: true),
                    IsSmsActiveToAdmin = table.Column<bool>(nullable: false),
                    IsSmsActiveToClient = table.Column<bool>(nullable: false),
                    GoogleSearchConsoleTag = table.Column<string>(maxLength: 500, nullable: true),
                    GoogleAnalyticsTag = table.Column<string>(maxLength: 2000, nullable: true),
                    Faveicon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.SettingId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
