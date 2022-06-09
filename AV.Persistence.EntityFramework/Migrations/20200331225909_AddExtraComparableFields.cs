using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddExtraComparableFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuyerName",
                table: "Comparables",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Comparables",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Comparables",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerName",
                table: "Comparables",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "accda93e-fe61-4788-a6c5-43690c60b878");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "baf6bb91-049c-4fe0-ba45-0d526b337df9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "a261fd96-d0c1-46d0-8804-ba347533aa2e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "51822b5b-1d0a-4b5f-9fe4-c1a23f26928f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "1811a40e-7b59-4782-887d-30e41d3915c1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerName",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "SellerName",
                table: "Comparables");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "6b392007-7b84-45b2-b93e-9681666c7923");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "259cde05-3ebd-4226-bc9a-70d0fed13142");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "bd99e916-566d-4187-ba14-cbb331b2f803");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "95986163-cebd-45de-b7bc-24f0c13bf43f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "f2d3e2e1-86d8-49a3-930a-8366e44f7312");
        }
    }
}
