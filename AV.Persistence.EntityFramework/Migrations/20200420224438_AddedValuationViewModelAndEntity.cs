using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedValuationViewModelAndEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Valuations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ValuerId = table.Column<Guid>(nullable: false),
                    TitleDeedNo = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Neighbourhood = table.Column<string>(nullable: true),
                    TitleDeedDate = table.Column<DateTime>(nullable: false),
                    PlotSize = table.Column<decimal>(nullable: false),
                    LandUse = table.Column<int>(nullable: false),
                    Zoning = table.Column<int>(nullable: false),
                    BuiltUpArea = table.Column<decimal>(nullable: false),
                    CeilingDetails = table.Column<string>(nullable: true),
                    FloorDetails = table.Column<string>(nullable: true),
                    Walls = table.Column<string>(nullable: true),
                    FittingsAndFixtures = table.Column<string>(nullable: true),
                    Doors = table.Column<string>(nullable: true),
                    Roofing = table.Column<string>(nullable: true),
                    Windows = table.Column<string>(nullable: true),
                    ExteriorFinishes = table.Column<string>(nullable: true),
                    OtherDetails = table.Column<string>(nullable: true),
                    MarketCommentary = table.Column<string>(nullable: true),
                    ToiletAndBathroom = table.Column<string>(nullable: true),
                    SwimmingPool = table.Column<bool>(nullable: false),
                    FirePlace = table.Column<bool>(nullable: false),
                    BoundaryWall = table.Column<bool>(nullable: false),
                    ElectricFence = table.Column<bool>(nullable: false),
                    MotorizedGate = table.Column<bool>(nullable: false),
                    OutdoorEntertainmentArea = table.Column<bool>(nullable: false),
                    Paved = table.Column<bool>(nullable: false),
                    OtherSpecialFeatures = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: true),
                    InstructionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valuations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Valuations_Instructions_InstructionId",
                        column: x => x.InstructionId,
                        principalTable: "Instructions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Valuations_AspNetUsers_ValuerId",
                        column: x => x.ValuerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    ValuationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amenities_Valuations_ValuationId",
                        column: x => x.ValuationId,
                        principalTable: "Valuations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "e50a319a-00b7-4894-bbe8-d7fac8755a41");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "207f9070-7c8f-47e3-84d6-da5276204a81");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "feff9ee6-bc57-4fa5-bde3-b49d69f7dde1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "861de926-d801-4926-942c-73dff3589fa7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "ad7fd3f6-6199-41d6-b61c-a8cae75d01c6");

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_ValuationId",
                table: "Amenities",
                column: "ValuationId");

            migrationBuilder.CreateIndex(
                name: "IX_Valuations_InstructionId",
                table: "Valuations",
                column: "InstructionId");

            migrationBuilder.CreateIndex(
                name: "IX_Valuations_ValuerId",
                table: "Valuations",
                column: "ValuerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amenities");

            migrationBuilder.DropTable(
                name: "Valuations");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "425e9815-02cd-41ce-b658-d79ffdd405fd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "39385642-504e-490e-89e2-f8bedb8e7adb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "b5ddb1bc-3324-4011-a827-bf5887d373b1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "8b9d8973-af3c-4f00-9631-04cc8d77dca5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "2928f4c7-924c-48ff-9f0b-357c65780bea");
        }
    }
}
