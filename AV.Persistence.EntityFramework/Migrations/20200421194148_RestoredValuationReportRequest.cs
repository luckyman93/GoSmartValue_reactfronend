using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class RestoredValuationReportRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ValuationRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RequestedOn = table.Column<DateTimeOffset>(nullable: false),
                    RequesterId = table.Column<Guid>(nullable: true),
                    ComparableRequestId = table.Column<Guid>(nullable: false),
                    ComparableResultId = table.Column<Guid>(nullable: false),
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
                value: "b7a76ac2-cc21-4587-be64-06e9a86ee53c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "50c68779-f953-4f16-a91e-5808aea165c2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "b9ea07cd-8b22-40d9-a965-f2d01fb48be1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "44fc551a-849b-4e2d-8130-412596f8026e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "4bb21b2e-d069-493d-808f-fc9db9dfde86");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequests_ComparableResultId",
                table: "ValuationRequests",
                column: "ComparableResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ValuationRequests");

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
        }
    }
}
