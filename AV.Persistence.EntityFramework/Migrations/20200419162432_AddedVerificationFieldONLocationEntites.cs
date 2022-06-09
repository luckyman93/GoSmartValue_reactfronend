using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedVerificationFieldONLocationEntites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "Streets",
                nullable: false,
                defaultValue: false);
            migrationBuilder.Sql("Update Streets Set `Verified` = 1");

            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "Locations",
                nullable: false,
                defaultValue: false);
            migrationBuilder.Sql("Update Locations Set `Verified` = 1");

            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "Localities",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql("Update Localities Set `Verified` = 1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "62fc7148-2535-4c7a-bca3-393fed0972b9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "2dd0f039-06a8-4046-9523-7a51b1959638");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "4e23debd-e9a0-41fc-944e-b36c1923c10c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "0d1a7723-9fc8-402d-9941-81789759b557");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "110fcf10-7e0e-4d74-b62d-b1a296be00db");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Verified",
                table: "Streets");

            migrationBuilder.DropColumn(
                name: "Verified",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Verified",
                table: "Localities");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "8c44ea90-c5ab-4719-840f-6fb4908132a4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "0301d5ab-88de-4117-8d42-e06def6db8b2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "f3bdb532-4938-4e95-9e7a-8d347fef3fa5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "25d30371-3654-465e-ad29-c8cc5fde0084");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "7c2e7b89-58fc-4dbb-8357-2c6b1a2ff937");
        }
    }
}
