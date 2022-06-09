using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedFieldsToCorporateAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Insurance",
                table: "Accounts",
                newName: "IsInsurance");

            migrationBuilder.RenameColumn(
                name: "GovernmentAgency",
                table: "Accounts",
                newName: "IsGovernmentAgency");

            migrationBuilder.RenameColumn(
                name: "Developer",
                table: "Accounts",
                newName: "IsDeveloper");

            migrationBuilder.RenameColumn(
                name: "Banker",
                table: "Accounts",
                newName: "IsBanker");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsInsurance",
                table: "Accounts",
                newName: "Insurance");

            migrationBuilder.RenameColumn(
                name: "IsGovernmentAgency",
                table: "Accounts",
                newName: "GovernmentAgency");

            migrationBuilder.RenameColumn(
                name: "IsDeveloper",
                table: "Accounts",
                newName: "Developer");

            migrationBuilder.RenameColumn(
                name: "IsBanker",
                table: "Accounts",
                newName: "Banker");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "9fe262cf-ba35-4626-8241-03d7ac73fd87");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "8468ff64-d6e6-4220-9d53-45dccbea1129");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "772d7433-0afc-4828-b923-a2a95afb2080");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "bf929b16-08c0-44f0-9df9-575207552bb8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "d5334ca5-a250-43c9-933d-26da6022073a");
        }
    }
}
