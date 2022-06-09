using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class RenameLandRatesToMarketInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LandRates");

            migrationBuilder.CreateTable(
                name: "MarketInformation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CountryId = table.Column<int>(nullable: true),
                    DistrictId = table.Column<int>(nullable: true),
                    LocationId = table.Column<int>(nullable: false),
                    LocalityId = table.Column<int>(nullable: true),
                    Metric = table.Column<int>(nullable: false),
                    FromRate = table.Column<double>(nullable: true),
                    ToRate = table.Column<double>(nullable: true),
                    AveragePlotSize = table.Column<double>(nullable: false),
                    AveragePlotSizeMetric = table.Column<int>(nullable: false),
                    Zoning = table.Column<int>(nullable: false),
                    AveragePrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketInformation_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarketInformation_Localities_LocalityId",
                        column: x => x.LocalityId,
                        principalTable: "Localities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarketInformation_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemConfiguration",
                columns: table => new
                {
                    ItemName = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemConfiguration", x => x.ItemName);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "d0d99a28-3021-495d-9a42-c280b85b1d00");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "dd131ea4-feaa-4998-b3d3-088dd96be6fa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "9048acb7-bf5f-403e-97c7-1e2ad35cf96c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "5cbda485-14b9-4ad5-bf8e-f8a2b2967c63");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "4a822d37-3353-4174-b11e-72cb8c9c5f80");

            migrationBuilder.CreateIndex(
                name: "IX_MarketInformation_DistrictId",
                table: "MarketInformation",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketInformation_LocalityId",
                table: "MarketInformation",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketInformation_LocationId",
                table: "MarketInformation",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketInformation");

            migrationBuilder.DropTable(
                name: "SystemConfiguration");

            migrationBuilder.CreateTable(
                name: "LandRates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CountryId = table.Column<int>(nullable: false),
                    DistrictId = table.Column<int>(nullable: false),
                    FromRate = table.Column<double>(nullable: false),
                    LocalityId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    Metric = table.Column<int>(nullable: false),
                    ToRate = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LandRates_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LandRates_Localities_LocalityId",
                        column: x => x.LocalityId,
                        principalTable: "Localities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                value: "fdbd0c8c-3e90-40db-a8a0-c7976a667a0f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "3475ee0b-7408-4210-a351-37ed5e9dc33d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "7ea0b251-bf1d-4761-a59a-6df219e4d2f6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "24a38086-a0dc-490e-958c-35f234fad323");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "571a4a1b-967d-427f-8d4c-6b2bac5f71cf");

            migrationBuilder.CreateIndex(
                name: "IX_LandRates_DistrictId",
                table: "LandRates",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_LandRates_LocalityId",
                table: "LandRates",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_LandRates_LocationId",
                table: "LandRates",
                column: "LocationId");
        }
    }
}
