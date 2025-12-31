using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenCross.Mammals.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Easting = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Northing = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Recorders",
                columns: table => new
                {
                    RecorderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Initials = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    WildlifeGroupMember = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recorders", x => x.RecorderId);
                });

            migrationBuilder.CreateTable(
                name: "RecordVerificationStatuses",
                columns: table => new
                {
                    RecordVerificationStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordVerificationStatuses", x => x.RecordVerificationStatusId);
                });

            migrationBuilder.CreateTable(
                name: "HarvestMouseRecords",
                columns: table => new
                {
                    RecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceRecordId = table.Column<int>(type: "int", nullable: false),
                    DateRecorded = table.Column<DateOnly>(type: "date", nullable: false),
                    RecorderId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    VerificationStatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HarvestMouseRecords", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_HarvestMouseRecords_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HarvestMouseRecords_RecordVerificationStatuses_VerificationStatusId",
                        column: x => x.VerificationStatusId,
                        principalTable: "RecordVerificationStatuses",
                        principalColumn: "RecordVerificationStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HarvestMouseRecords_Recorders_RecorderId",
                        column: x => x.RecorderId,
                        principalTable: "Recorders",
                        principalColumn: "RecorderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HarvestMouseRecords_LocationId",
                table: "HarvestMouseRecords",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestMouseRecords_RecorderId",
                table: "HarvestMouseRecords",
                column: "RecorderId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestMouseRecords_VerificationStatusId",
                table: "HarvestMouseRecords",
                column: "VerificationStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HarvestMouseRecords");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "RecordVerificationStatuses");

            migrationBuilder.DropTable(
                name: "Recorders");
        }
    }
}
