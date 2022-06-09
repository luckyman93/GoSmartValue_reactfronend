using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class LandRateEntityAddedToContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Imports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    ImportedById = table.Column<Guid>(nullable: false),
                    ActiveFrom = table.Column<DateTimeOffset>(nullable: false),
                    ActiveTo = table.Column<DateTimeOffset>(nullable: false),
                    FileStream = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LandRates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    DistrictId = table.Column<int>(nullable: true),
                    LocationId = table.Column<int>(nullable: false),
                    LocalityId = table.Column<int>(nullable: true),
                    Metric = table.Column<int>(nullable: false),
                    Rate = table.Column<decimal>(nullable: true),
                    LowIncome = table.Column<int>(nullable: false),
                    MiddleIncome = table.Column<int>(nullable: false),
                    HighIncome = table.Column<int>(nullable: false),
                    Zoning = table.Column<int>(nullable: false),
                    AveragePrice = table.Column<decimal>(nullable: true),
                    ImportHeaderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LandRates_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LandRates_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LandRates_Imports_ImportHeaderId",
                        column: x => x.ImportHeaderId,
                        principalTable: "Imports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LandRates_Localities_LocalityId",
                        column: x => x.LocalityId,
                        principalTable: "Localities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LandRates_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "47db1d03-be0b-47a6-914d-3a53ef561dd1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "15c8ce32-4740-4d1e-ab14-faf52a92c453");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "c2e9eb86-2806-4c11-a9e7-6a6f07539795");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "1f0f5a17-b92d-4b06-b886-978284390baa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "216a9fff-77d5-403f-8765-be3ce9ef41aa");

            migrationBuilder.CreateIndex(
                name: "IX_LandRates_CountryId",
                table: "LandRates",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_LandRates_DistrictId",
                table: "LandRates",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_LandRates_ImportHeaderId",
                table: "LandRates",
                column: "ImportHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_LandRates_LocalityId",
                table: "LandRates",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_LandRates_LocationId",
                table: "LandRates",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LandRates");

            migrationBuilder.DropTable(
                name: "Imports");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "387adbdf-4fa2-460b-982d-b92c82ca4d20");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "7308cc00-dac8-40f8-bdf6-1b2bed6cff03");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "f0bcd82a-b7f8-437b-9f3a-a535bee17257");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "8b770ab0-49a0-45ca-9c51-764abfa9bf7d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "4a95dac3-c176-47b4-ba32-e3580f98ac87");
        }
    }
}
