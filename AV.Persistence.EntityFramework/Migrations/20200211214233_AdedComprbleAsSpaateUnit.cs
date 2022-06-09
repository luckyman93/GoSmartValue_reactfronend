using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AdedComprbleAsSpaateUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ComparableId",
                table: "Rooms",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComparableId",
                table: "PropertyFeature",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComparableId",
                table: "PropertyDetails",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Comparables",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AddedOn = table.Column<DateTimeOffset>(nullable: false),
                    AddedBy = table.Column<Guid>(nullable: false),
                    LastUpdatedBy = table.Column<Guid>(nullable: false),
                    LastUpdatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DataState = table.Column<int>(nullable: false),
                    DateOfSale = table.Column<DateTimeOffset>(nullable: false),
                    SalePrice = table.Column<decimal>(nullable: false),
                    Metric = table.Column<int>(nullable: false),
                    PlotSize = table.Column<decimal>(nullable: false),
                    PlotId = table.Column<Guid>(nullable: true),
                    LandUse = table.Column<int>(nullable: false),
                    PropertyType = table.Column<int>(nullable: false),
                    BandClassBandName = table.Column<string>(nullable: true),
                    LocationId = table.Column<int>(nullable: false),
                    LocalityId = table.Column<int>(nullable: true),
                    StreetName = table.Column<string>(nullable: true),
                    PlotNo = table.Column<string>(nullable: true),
                    ComparableResultId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comparables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comparables_ComparableBandSizes_BandClassBandName",
                        column: x => x.BandClassBandName,
                        principalTable: "ComparableBandSizes",
                        principalColumn: "BandName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comparables_ComparableResults_ComparableResultId",
                        column: x => x.ComparableResultId,
                        principalTable: "ComparableResults",
                        principalColumn: "ReferenceNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comparables_Localities_LocalityId",
                        column: x => x.LocalityId,
                        principalTable: "Localities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comparables_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comparables_Plots_PlotId",
                        column: x => x.PlotId,
                        principalTable: "Plots",
                        principalColumn: "PlotId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "69fe6157-7727-4639-89dd-f2a478f9b490");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "20b27bb3-68c6-4551-9559-59e3fc93cd33");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "33dae1bb-2de0-433d-870c-231c5561ce80");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "216f7945-fa03-4d9f-85d1-e35253cf2a92");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "088949bf-c80b-4bc6-9488-d3adcedcd888");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ComparableId",
                table: "Rooms",
                column: "ComparableId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeature_ComparableId",
                table: "PropertyFeature",
                column: "ComparableId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetails_ComparableId",
                table: "PropertyDetails",
                column: "ComparableId");

            migrationBuilder.CreateIndex(
                name: "IX_Comparables_BandClassBandName",
                table: "Comparables",
                column: "BandClassBandName");

            migrationBuilder.CreateIndex(
                name: "IX_Comparables_ComparableResultId",
                table: "Comparables",
                column: "ComparableResultId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comparables_LocalityId",
                table: "Comparables",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Comparables_LocationId",
                table: "Comparables",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Comparables_PlotId",
                table: "Comparables",
                column: "PlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDetails_Comparables_ComparableId",
                table: "PropertyDetails",
                column: "ComparableId",
                principalTable: "Comparables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyFeature_Comparables_ComparableId",
                table: "PropertyFeature",
                column: "ComparableId",
                principalTable: "Comparables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Comparables_ComparableId",
                table: "Rooms",
                column: "ComparableId",
                principalTable: "Comparables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDetails_Comparables_ComparableId",
                table: "PropertyDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyFeature_Comparables_ComparableId",
                table: "PropertyFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Comparables_ComparableId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "Comparables");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_ComparableId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_PropertyFeature_ComparableId",
                table: "PropertyFeature");

            migrationBuilder.DropIndex(
                name: "IX_PropertyDetails_ComparableId",
                table: "PropertyDetails");

            migrationBuilder.DropColumn(
                name: "ComparableId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ComparableId",
                table: "PropertyFeature");

            migrationBuilder.DropColumn(
                name: "ComparableId",
                table: "PropertyDetails");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "3e3a68bf-f0fc-4b75-b892-e89ffe33d1ec");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "8e37b4fe-92a8-4471-be56-ac31c1c661b8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "e89347a9-27ef-458c-9d46-7f8aec5c5a96");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "9133028e-6de1-43ca-bc98-7abb4745f64f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "6ca1524c-f0b9-43a9-83b8-2897056e24e2");
        }
    }
}
