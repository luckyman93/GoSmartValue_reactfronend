using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedAddedByFieldsOnSalesRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AddedBy",
                table: "PropertyDetails",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LastUpdatedBy",
                table: "PropertyDetails",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastUpdatedOn",
                table: "PropertyDetails",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateTable(
                name: "PropertyFeature",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PropertySaleId = table.Column<Guid>(nullable: false),
                    FeatureType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyFeature_PropertyDetails_PropertySaleId",
                        column: x => x.PropertySaleId,
                        principalTable: "PropertyDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PropertySaleId = table.Column<Guid>(nullable: false),
                    RoomType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_PropertyDetails_PropertySaleId",
                        column: x => x.PropertySaleId,
                        principalTable: "PropertyDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "99c2815e-7bdd-4c2b-b144-53f4534d47e0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "4ccf8712-f44a-4645-bbfa-4bf4988a2ac8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "64e3f483-88e3-41a8-a415-46aba4956cdd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "960515e6-b98c-4a7c-9add-4783bfdfb911");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "fd39ae2b-eb0a-4644-a18a-0efb7d56b3d9");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeature_PropertySaleId",
                table: "PropertyFeature",
                column: "PropertySaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_PropertySaleId",
                table: "Rooms",
                column: "PropertySaleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyFeature");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropColumn(
                name: "AddedBy",
                table: "PropertyDetails");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "PropertyDetails");

            migrationBuilder.DropColumn(
                name: "LastUpdatedOn",
                table: "PropertyDetails");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "90780de0-fcf4-4cdb-a638-a9fdc67f56c0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "e1ba3f8d-4a15-4f48-b87c-01e1c6e2c9cd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "1e507ef6-8dc5-4b73-9cc2-dafd23bdc4ac");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "1fca13de-3b83-4f08-be13-697227c2fcab");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "25d5b55c-4297-4196-a508-71204a95a6aa");
        }
    }
}
