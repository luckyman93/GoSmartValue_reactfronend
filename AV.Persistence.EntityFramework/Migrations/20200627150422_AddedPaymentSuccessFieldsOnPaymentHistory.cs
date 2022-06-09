using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedPaymentSuccessFieldsOnPaymentHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CCDapproval",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyRef",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PnrID",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransID",
                table: "Payments",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "387adbdf-4fa2-460b-982d-b92c82ca4d20");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "7308cc00-dac8-40f8-bdf6-1b2bed6cff03");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "f0bcd82a-b7f8-437b-9f3a-a535bee17257");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "8b770ab0-49a0-45ca-9c51-764abfa9bf7d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "4a95dac3-c176-47b4-ba32-e3580f98ac87");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CCDapproval",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CompanyRef",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PnrID",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "TransID",
                table: "Payments");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "900b2450-fc94-4460-b5ae-3eb011e3a721");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "81f0bb80-2659-43ea-bc3c-22baec120dcb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "7970e11f-94f8-49d4-bc62-c529f7d19d97");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "694f2abf-a576-4dc8-8de9-f5a51f022ef9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "9941feed-53ce-4fa0-a412-6a8e2c2f2dd5");
        }
    }
}
