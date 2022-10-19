using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvertisementPortal.DatabaseAccess.Migrations
{
    public partial class updateuserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "TEXT",
                nullable: true);
        }
    }
}
