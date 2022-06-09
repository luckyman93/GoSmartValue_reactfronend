using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedAdditionalPropertyDetailsOnComparableResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ValuationRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BathRooms",
                table: "ComparableResults",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BedRooms",
                table: "ComparableResults",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ComparableResults",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "ComparableResults",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Garages",
                table: "ComparableResults",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Kitchens",
                table: "ComparableResults",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "ComparableResults",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "ComparableResults",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportType",
                table: "ComparableResults",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SittingRooms",
                table: "ComparableResults",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Toilets",
                table: "ComparableResults",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "d7df0e7f-fe5d-40f1-b8b8-cb6b5d1ac810");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "d49d38f6-27d8-4c80-9ed2-4da4dd0c0bef");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "15997cf5-f397-46b5-a201-67857a33d87a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "b819a354-5a9d-49d6-8f00-2870c605cbd1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "2e7f9064-e268-4c4c-a563-2c0928eac3f2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ValuationRequests");

            migrationBuilder.DropColumn(
                name: "BathRooms",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "BedRooms",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "Garages",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "Kitchens",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "ReportType",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "SittingRooms",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "Toilets",
                table: "ComparableResults");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "9a991b31-48ae-488a-9383-a2abe8eb1e77");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "4c9d09e2-f9df-45e8-bbbf-1ad9b991469f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "3d1fe7bc-9f04-4104-82dd-3c917cffc434");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "7bc348a5-9282-4b3a-bb6f-6ca7d1fa53ac");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "3f3d660e-d1c6-4735-9955-79c19f0389a5");
        }
    }
}
