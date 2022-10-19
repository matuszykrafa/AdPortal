using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvertisementPortal.DatabaseAccess.Migrations
{
    public partial class updateOfferStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAccepted",
                table: "Offers",
                newName: "OfferStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OfferStatus",
                table: "Offers",
                newName: "IsAccepted");
        }
    }
}
