using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedValuationReportRequestCorrected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ValuationRequests_AspNetUsers_ClientUserId",
                table: "ValuationRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ValuationRequests_Localities_LocalityId",
                table: "ValuationRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ValuationRequests_Locations_LocationId",
                table: "ValuationRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ValuationRequests_PropertyDetails_PropertyDetailId",
                table: "ValuationRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ValuationRequests_AspNetUsers_RequesterUserId",
                table: "ValuationRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ValuationRequests_Valuations_ValuationId",
                table: "ValuationRequests");

            migrationBuilder.DropIndex(
                name: "IX_ValuationRequests_ClientUserId",
                table: "ValuationRequests");

            migrationBuilder.DropIndex(
                name: "IX_ValuationRequests_LocalityId",
                table: "ValuationRequests");

            migrationBuilder.DropIndex(
                name: "IX_ValuationRequests_LocationId",
                table: "ValuationRequests");

            migrationBuilder.DropIndex(
                name: "IX_ValuationRequests_PropertyDetailId",
                table: "ValuationRequests");

            migrationBuilder.DropIndex(
                name: "IX_ValuationRequests_RequesterUserId",
                table: "ValuationRequests");

            migrationBuilder.DropIndex(
                name: "IX_ValuationRequests_ValuationId",
                table: "ValuationRequests");

            migrationBuilder.DropColumn(
                name: "ClientUserId",
                table: "ValuationRequests");

            migrationBuilder.DropColumn(
                name: "DateOfSale",
                table: "ValuationRequests");

            migrationBuilder.DropColumn(
                name: "Host",
                table: "ValuationRequests");

            migrationBuilder.DropColumn(
                name: "LandUse",
                table: "ValuationRequests");

            migrationBuilder.DropColumn(
                name: "LocalityId",
                table: "ValuationRequests");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "ValuationRequests");

            migrationBuilder.DropColumn(
                name: "PlotNo",
                table: "ValuationRequests");

            migrationBuilder.DropColumn(
                name: "PlotSize",
                table: "ValuationRequests");

            migrationBuilder.DropColumn(
                name: "PropertyDetailId",
                table: "ValuationRequests");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                table: "ValuationRequests");

            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "ValuationRequests");

            migrationBuilder.DropColumn(
                name: "RequestDate",
                table: "ValuationRequests");

            migrationBuilder.DropColumn(
                name: "RequesterUserId",
                table: "ValuationRequests");

            migrationBuilder.DropColumn(
                name: "StreetName",
                table: "ValuationRequests");

            migrationBuilder.DropColumn(
                name: "ValuationId",
                table: "ValuationRequests");

            migrationBuilder.AddColumn<Guid>(
                name: "ComparableRequestId",
                table: "ValuationRequests",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ComparableResultId",
                table: "ValuationRequests",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RequestedOn",
                table: "ValuationRequests",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "RequesterId",
                table: "ValuationRequests",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ValuationRequestsDelete",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RequestDate = table.Column<DateTime>(nullable: false),
                    StreetName = table.Column<string>(nullable: true),
                    PlotNo = table.Column<string>(nullable: true),
                    PlotSize = table.Column<int>(nullable: false),
                    LandUse = table.Column<int>(nullable: false),
                    DateOfSale = table.Column<DateTime>(nullable: false),
                    PropertyDetailId = table.Column<Guid>(nullable: true),
                    ValuationId = table.Column<long>(nullable: true),
                    RequesterUserId = table.Column<Guid>(nullable: true),
                    Host = table.Column<string>(nullable: true),
                    PropertyType = table.Column<string>(nullable: true),
                    LocationId = table.Column<int>(nullable: false),
                    PurchasePrice = table.Column<int>(nullable: false),
                    LocalityId = table.Column<int>(nullable: false),
                    ClientUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValuationRequestsDelete", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValuationRequestsDelete_AspNetUsers_ClientUserId",
                        column: x => x.ClientUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ValuationRequestsDelete_Localities_LocalityId",
                        column: x => x.LocalityId,
                        principalTable: "Localities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ValuationRequestsDelete_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ValuationRequestsDelete_PropertyDetails_PropertyDetailId",
                        column: x => x.PropertyDetailId,
                        principalTable: "PropertyDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ValuationRequestsDelete_AspNetUsers_RequesterUserId",
                        column: x => x.RequesterUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ValuationRequestsDelete_Valuations_ValuationId",
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
                value: "71a1e0da-18ad-46d0-a7e1-b77fc2c72250");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "72e84061-ebd2-420c-ba3c-8c2ee50f4102");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "dd51ce52-a37e-4fa8-b4c2-75313e1c7337");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "ed173bbe-19df-4d62-bc58-77bb41e9a9aa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "846eacdd-f248-47db-8ca7-616514bbb2ed");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequests_ComparableResultId",
                table: "ValuationRequests",
                column: "ComparableResultId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequestsDelete_ClientUserId",
                table: "ValuationRequestsDelete",
                column: "ClientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequestsDelete_LocalityId",
                table: "ValuationRequestsDelete",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequestsDelete_LocationId",
                table: "ValuationRequestsDelete",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequestsDelete_PropertyDetailId",
                table: "ValuationRequestsDelete",
                column: "PropertyDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequestsDelete_RequesterUserId",
                table: "ValuationRequestsDelete",
                column: "RequesterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequestsDelete_ValuationId",
                table: "ValuationRequestsDelete",
                column: "ValuationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ValuationRequests_ComparableResults_ComparableResultId",
                table: "ValuationRequests",
                column: "ComparableResultId",
                principalTable: "ComparableResults",
                principalColumn: "ReferenceNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ValuationRequests_ComparableResults_ComparableResultId",
                table: "ValuationRequests");

            migrationBuilder.DropTable(
                name: "ValuationRequestsDelete");

            migrationBuilder.DropIndex(
                name: "IX_ValuationRequests_ComparableResultId",
                table: "ValuationRequests");

            migrationBuilder.DropColumn(
                name: "ComparableRequestId",
                table: "ValuationRequests");

            migrationBuilder.DropColumn(
                name: "ComparableResultId",
                table: "ValuationRequests");

            migrationBuilder.DropColumn(
                name: "RequestedOn",
                table: "ValuationRequests");

            migrationBuilder.DropColumn(
                name: "RequesterId",
                table: "ValuationRequests");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientUserId",
                table: "ValuationRequests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfSale",
                table: "ValuationRequests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Host",
                table: "ValuationRequests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LandUse",
                table: "ValuationRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocalityId",
                table: "ValuationRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "ValuationRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PlotNo",
                table: "ValuationRequests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlotSize",
                table: "ValuationRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyDetailId",
                table: "ValuationRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropertyType",
                table: "ValuationRequests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PurchasePrice",
                table: "ValuationRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestDate",
                table: "ValuationRequests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "RequesterUserId",
                table: "ValuationRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetName",
                table: "ValuationRequests",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ValuationId",
                table: "ValuationRequests",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "6d2ea76c-1181-4ee6-9956-d191a1d9138a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "6b7459a2-4eca-4111-aa62-3af4ff9b282e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "92ca3361-0638-4dae-b48d-16881df2f0ff");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "d84afcfd-14d9-4e51-94bc-6f5bcd41084c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "19f8652e-d951-493a-87fb-414f665c4951");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequests_ClientUserId",
                table: "ValuationRequests",
                column: "ClientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequests_LocalityId",
                table: "ValuationRequests",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequests_LocationId",
                table: "ValuationRequests",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequests_PropertyDetailId",
                table: "ValuationRequests",
                column: "PropertyDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequests_RequesterUserId",
                table: "ValuationRequests",
                column: "RequesterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequests_ValuationId",
                table: "ValuationRequests",
                column: "ValuationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ValuationRequests_AspNetUsers_ClientUserId",
                table: "ValuationRequests",
                column: "ClientUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ValuationRequests_Localities_LocalityId",
                table: "ValuationRequests",
                column: "LocalityId",
                principalTable: "Localities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ValuationRequests_Locations_LocationId",
                table: "ValuationRequests",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ValuationRequests_PropertyDetails_PropertyDetailId",
                table: "ValuationRequests",
                column: "PropertyDetailId",
                principalTable: "PropertyDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ValuationRequests_AspNetUsers_RequesterUserId",
                table: "ValuationRequests",
                column: "RequesterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ValuationRequests_Valuations_ValuationId",
                table: "ValuationRequests",
                column: "ValuationId",
                principalTable: "Valuations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
