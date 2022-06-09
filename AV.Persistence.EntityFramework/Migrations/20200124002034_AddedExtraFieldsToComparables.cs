using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedExtraFieldsToComparables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedOn",
                table: "ComparableResults",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "EstimatedOn",
                table: "ComparableResults",
                nullable: false,
                defaultValue: DateTimeOffset.UtcNow);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ComparableResults",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "ComparableResults",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "4dab1fff-afe0-4677-b89c-f710610f6faf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "339defe3-3a2e-483c-9ca5-70310b0d7792");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "e3afe8f0-c440-4001-879e-849f911ecced");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "ba3233c9-bb9f-4d54-86a9-303236000a44");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "933f3d73-1575-4644-a225-fcaa1b89a892");

            migrationBuilder.CreateIndex(
                name: "IX_ComparableResults_OwnerId",
                table: "ComparableResults",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComparableResults_AspNetUsers_OwnerId",
                table: "ComparableResults",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComparableResults_AspNetUsers_OwnerId",
                table: "ComparableResults");

            migrationBuilder.DropIndex(
                name: "IX_ComparableResults_OwnerId",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "EstimatedOn",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ComparableResults");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "a8307ff7-14fa-4046-af15-8164cb85337a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "57bc409d-b4f2-4360-89b7-f1478c017322");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "f51a16f8-6cf3-4520-8449-ea53868fd06a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "74ebac3b-8ec6-4ad3-a630-fa286e13c6b1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "903446a5-1f31-4899-b8cf-d7997aa9f015");
        }
    }
}
