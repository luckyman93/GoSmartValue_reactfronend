using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedMoreFieldsOnComparableEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComparableRequestId",
                table: "ComparableResults");

            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedValue",
                table: "Valuations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Valuations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Valuations",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "Valuations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BathRooms",
                table: "Comparables",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BedRooms",
                table: "Comparables",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ComparableResultReferenceNumber",
                table: "Comparables",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Garages",
                table: "Comparables",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Kitchens",
                table: "Comparables",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SittingRooms",
                table: "Comparables",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Toilets",
                table: "Comparables",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "5c80920d-4e4a-4155-b159-45763b805d9a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "f28b2afa-e757-4236-bdfa-70c0addcfedf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "afd22d87-e4c3-4c3d-aa10-549e3a8bea6c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "78885343-8c7c-4a55-8afe-44a0b52fca4b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "91d6f5be-efd5-4d7f-923c-eab9a842afb0");

            migrationBuilder.CreateIndex(
                name: "IX_Comparables_ComparableResultReferenceNumber",
                table: "Comparables",
                column: "ComparableResultReferenceNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Comparables_ComparableResults_ComparableResultReferenceNumber",
                table: "Comparables",
                column: "ComparableResultReferenceNumber",
                principalTable: "ComparableResults",
                principalColumn: "ReferenceNumber",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comparables_ComparableResults_ComparableResultReferenceNumber",
                table: "Comparables");

            migrationBuilder.DropIndex(
                name: "IX_Comparables_ComparableResultReferenceNumber",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "EstimatedValue",
                table: "Valuations");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Valuations");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Valuations");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Valuations");

            migrationBuilder.DropColumn(
                name: "BathRooms",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "BedRooms",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "ComparableResultReferenceNumber",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "Garages",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "Kitchens",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "SittingRooms",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "Toilets",
                table: "Comparables");

            migrationBuilder.AddColumn<Guid>(
                name: "ComparableRequestId",
                table: "ComparableResults",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
        }
    }
}
