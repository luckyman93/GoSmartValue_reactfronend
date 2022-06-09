using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddValuationComparablesM2M : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comparables_Comparables_ComparableId",
                table: "Comparables");

            migrationBuilder.DropForeignKey(
                name: "FK_Comparables_ComparableResults_ComparableResultReferenceNumber",
                table: "Comparables");

            migrationBuilder.DropForeignKey(
                name: "FK_Comparables_Valuations_ValuationId",
                table: "Comparables");

            migrationBuilder.DropTable(
                name: "ValuationRequests");

            migrationBuilder.DropIndex(
                name: "IX_Comparables_ComparableId",
                table: "Comparables");

            migrationBuilder.DropIndex(
                name: "IX_Comparables_ComparableResultReferenceNumber",
                table: "Comparables");

            migrationBuilder.DropIndex(
                name: "IX_Comparables_ValuationId",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "ComparableId",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "ComparableResultReferenceNumber",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "ValuationId",
                table: "Comparables");

            migrationBuilder.AddColumn<Guid>(
                name: "ReportRequestId",
                table: "PropertyFeature",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ComparableResultComparable",
                columns: table => new
                {
                    ComparableId = table.Column<Guid>(nullable: false),
                    ComparableResultId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComparableResultComparable", x => new { x.ComparableId, x.ComparableResultId });
                    table.ForeignKey(
                        name: "FK_ComparableResultComparable_Comparables_ComparableId",
                        column: x => x.ComparableId,
                        principalTable: "Comparables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComparableResultComparable_ComparableResults_ComparableResul~",
                        column: x => x.ComparableResultId,
                        principalTable: "ComparableResults",
                        principalColumn: "ReferenceNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    ReferenceNumber = table.Column<Guid>(nullable: false),
                    RequesterUserId = table.Column<Guid>(nullable: false),
                    EstimatedOn = table.Column<DateTimeOffset>(nullable: false),
                    EstimatedValue = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Toilets = table.Column<int>(nullable: false),
                    Garages = table.Column<int>(nullable: false),
                    BedRooms = table.Column<int>(nullable: false),
                    Kitchens = table.Column<int>(nullable: false),
                    SittingRooms = table.Column<int>(nullable: false),
                    BathRooms = table.Column<int>(nullable: false),
                    ReportRequested = table.Column<bool>(nullable: false),
                    ReportType = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
                    PaymentStatus = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportRequests", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "ceac3b67-120f-43ec-9dbf-8698257e3e46");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "628bb088-c9b3-4ad3-b6af-4aa5f246cfd0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "88e2647e-5cfb-4a4d-94fd-c99f4148a0aa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "5d5f0962-bea3-4d60-b1fe-f642a955f441");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "c56c8449-6913-4716-8da3-bad4218d1153");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeature_ReportRequestId",
                table: "PropertyFeature",
                column: "ReportRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ComparableResultComparable_ComparableResultId",
                table: "ComparableResultComparable",
                column: "ComparableResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyFeature_ReportRequests_ReportRequestId",
                table: "PropertyFeature",
                column: "ReportRequestId",
                principalTable: "ReportRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyFeature_ReportRequests_ReportRequestId",
                table: "PropertyFeature");

            migrationBuilder.DropTable(
                name: "ComparableResultComparable");

            migrationBuilder.DropTable(
                name: "ReportRequests");

            migrationBuilder.DropIndex(
                name: "IX_PropertyFeature_ReportRequestId",
                table: "PropertyFeature");

            migrationBuilder.DropColumn(
                name: "ReportRequestId",
                table: "PropertyFeature");

            migrationBuilder.AddColumn<Guid>(
                name: "ComparableId",
                table: "Comparables",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComparableResultReferenceNumber",
                table: "Comparables",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ValuationId",
                table: "Comparables",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ValuationRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ComparableRequestId = table.Column<Guid>(nullable: false),
                    ComparableResultId = table.Column<Guid>(nullable: false),
                    RequestedOn = table.Column<DateTimeOffset>(nullable: false),
                    RequesterId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValuationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValuationRequests_ComparableResults_ComparableResultId",
                        column: x => x.ComparableResultId,
                        principalTable: "ComparableResults",
                        principalColumn: "ReferenceNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "494e26ca-0dfd-4996-965e-e154aaa44d1b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "129eb97b-81c8-48d1-9fce-b91fa1606ec2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "1f312b50-64c8-4b7e-affe-760351c8c1ef");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "70e43de3-1be7-41e9-8de1-c3dc12ed7451");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "5d668b80-9bfd-4b1b-afd0-af5d4efb28be");

            migrationBuilder.CreateIndex(
                name: "IX_Comparables_ComparableId",
                table: "Comparables",
                column: "ComparableId");

            migrationBuilder.CreateIndex(
                name: "IX_Comparables_ComparableResultReferenceNumber",
                table: "Comparables",
                column: "ComparableResultReferenceNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Comparables_ValuationId",
                table: "Comparables",
                column: "ValuationId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequests_ComparableResultId",
                table: "ValuationRequests",
                column: "ComparableResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comparables_Comparables_ComparableId",
                table: "Comparables",
                column: "ComparableId",
                principalTable: "Comparables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comparables_ComparableResults_ComparableResultReferenceNumber",
                table: "Comparables",
                column: "ComparableResultReferenceNumber",
                principalTable: "ComparableResults",
                principalColumn: "ReferenceNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comparables_Valuations_ValuationId",
                table: "Comparables",
                column: "ValuationId",
                principalTable: "Valuations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
