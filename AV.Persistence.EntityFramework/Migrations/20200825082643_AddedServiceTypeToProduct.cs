using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedServiceTypeToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServiceType",
                table: "Products",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "0aba412b-570b-45f0-9a79-ef863bcb6b26");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "a9030741-8f06-41a9-8c94-80a996b63a4a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "a2f42f9b-88fe-471f-9322-4140f9a974fa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "c3dfb551-d52f-44b8-b693-5ca5e333fd57");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "87c918e6-0d8d-4ba6-8b67-57f029dc6556");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "b8847b98-f9a9-4a83-9175-2eff0872bb2a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "cda2bdb0-fb83-4557-9f34-d44cffcfff35");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "371aa8eb-9b31-4c79-919c-f7338456d153");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "00026f0e-948e-4051-9a59-54fb778df2c6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "570902e2-c1c0-463e-96c7-80afded40002");
        }
    }
}
