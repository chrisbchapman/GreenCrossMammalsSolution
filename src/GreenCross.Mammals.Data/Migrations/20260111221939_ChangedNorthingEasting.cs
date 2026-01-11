using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenCross.Mammals.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedNorthingEasting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Northing",
                table: "Locations",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "Easting",
                table: "Locations",
                newName: "Latitude");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Locations",
                newName: "Northing");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "Locations",
                newName: "Easting");
        }
    }
}
