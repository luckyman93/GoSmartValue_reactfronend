using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class MadeLocalityNullableOnInstruction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_Localities_LocalityId",
                table: "Instructions");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyFeature_PropertyDetails_PropertySaleId",
                table: "PropertyFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_PropertyDetails_PropertySaleId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "PropertyDetails");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_PropertySaleId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_PropertyFeature_PropertySaleId",
                table: "PropertyFeature");

            migrationBuilder.DropColumn(
                name: "PropertySaleId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "PropertySaleId",
                table: "PropertyFeature");

            migrationBuilder.AlterColumn<int>(
                name: "LocalityId",
                table: "Instructions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "444e3aa6-6a82-405d-8e67-2392b4377cbc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "33e9aa1e-04f9-4c20-8542-764aa766f3ab");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "1687bd6b-027f-4411-a82d-995a0efde298");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "4d2a147b-f208-413d-970b-b2e05bd0b680");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "bee1762e-85d3-4309-85a7-77e484db63d7");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_Localities_LocalityId",
                table: "Instructions",
                column: "LocalityId",
                principalTable: "Localities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComparableResults_Valuations_ValuationId",
                table: "ComparableResults");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_Localities_LocalityId",
                table: "Instructions");

            migrationBuilder.RenameColumn(
                name: "ValuationId",
                table: "ComparableResults",
                newName: "PropertySaleId");

            migrationBuilder.RenameIndex(
                name: "IX_ComparableResults_ValuationId",
                table: "ComparableResults",
                newName: "IX_ComparableResults_PropertySaleId");

            migrationBuilder.AddColumn<Guid>(
                name: "PropertySaleId",
                table: "Rooms",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PropertySaleId",
                table: "PropertyFeature",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LocalityId",
                table: "Instructions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PropertyDetailsId",
                table: "ComparableResults",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PropertyDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AddedBy = table.Column<Guid>(nullable: false),
                    AddedOn = table.Column<DateTimeOffset>(nullable: false),
                    BandClassBandName = table.Column<string>(nullable: true),
                    BankName = table.Column<string>(nullable: true),
                    BondAmount = table.Column<decimal>(nullable: true),
                    BondNumber = table.Column<string>(nullable: true),
                    BuyerId = table.Column<Guid>(nullable: true),
                    BuyerName = table.Column<string>(nullable: true),
                    DataState = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: true),
                    DateOfSale = table.Column<DateTimeOffset>(nullable: true),
                    LandUse = table.Column<int>(nullable: true),
                    LastUpdatedBy = table.Column<Guid>(nullable: true),
                    LastUpdatedOn = table.Column<DateTimeOffset>(nullable: false),
                    Latitude = table.Column<double>(nullable: true),
                    LocalityId = table.Column<int>(nullable: true),
                    LocationId = table.Column<int>(nullable: false),
                    Longitude = table.Column<double>(nullable: true),
                    Metric = table.Column<int>(nullable: false),
                    PlotId = table.Column<int>(nullable: true),
                    PlotId1 = table.Column<Guid>(nullable: true),
                    PlotNo = table.Column<string>(nullable: true),
                    PlotSize = table.Column<decimal>(nullable: true),
                    PropertySaleId = table.Column<Guid>(nullable: true),
                    PropertyType = table.Column<int>(nullable: true),
                    SalePrice = table.Column<decimal>(nullable: true),
                    SellerId = table.Column<Guid>(nullable: true),
                    SellerName = table.Column<string>(nullable: true),
                    StreetId = table.Column<int>(nullable: true),
                    StreetName = table.Column<string>(nullable: true),
                    TransactionType = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyDetails_ComparableBandSizes_BandClassBandName",
                        column: x => x.BandClassBandName,
                        principalTable: "ComparableBandSizes",
                        principalColumn: "BandName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyDetails_Buyer_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyDetails_Localities_LocalityId",
                        column: x => x.LocalityId,
                        principalTable: "Localities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyDetails_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyDetails_Plots_PlotId1",
                        column: x => x.PlotId1,
                        principalTable: "Plots",
                        principalColumn: "PlotId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyDetails_PropertyDetails_PropertySaleId",
                        column: x => x.PropertySaleId,
                        principalTable: "PropertyDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyDetails_Seller_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Seller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyDetails_Streets_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Streets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "f79b9de5-d61e-492d-a32b-1283cc3c4cdc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "b9871142-304d-493f-8190-fb3c5d06e217");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "b9fc47a3-19f5-48d2-b15a-1410d1378e34");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "0c6d0ddf-bd2d-4d53-a55a-2a15dc65b340");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "ff1a3a6e-cb1d-4144-a955-d8755bbbbdd1");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_PropertySaleId",
                table: "Rooms",
                column: "PropertySaleId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeature_PropertySaleId",
                table: "PropertyFeature",
                column: "PropertySaleId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetails_BandClassBandName",
                table: "PropertyDetails",
                column: "BandClassBandName");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetails_BuyerId",
                table: "PropertyDetails",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetails_LocalityId",
                table: "PropertyDetails",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetails_LocationId",
                table: "PropertyDetails",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetails_PlotId1",
                table: "PropertyDetails",
                column: "PlotId1");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetails_PropertySaleId",
                table: "PropertyDetails",
                column: "PropertySaleId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetails_SellerId",
                table: "PropertyDetails",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetails_StreetId",
                table: "PropertyDetails",
                column: "StreetId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComparableResults_PropertyDetails_PropertySaleId",
                table: "ComparableResults",
                column: "PropertySaleId",
                principalTable: "PropertyDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_Localities_LocalityId",
                table: "Instructions",
                column: "LocalityId",
                principalTable: "Localities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyFeature_PropertyDetails_PropertySaleId",
                table: "PropertyFeature",
                column: "PropertySaleId",
                principalTable: "PropertyDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_PropertyDetails_PropertySaleId",
                table: "Rooms",
                column: "PropertySaleId",
                principalTable: "PropertyDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
