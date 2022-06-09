using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class LinkValuationResultToComparables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDetails_Comparables_ComparableId",
                table: "PropertyDetails");

            migrationBuilder.DropIndex(
                name: "IX_PropertyDetails_ComparableId",
                table: "PropertyDetails");

            migrationBuilder.DropColumn(
                name: "ComparableId",
                table: "PropertyDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "ComparableId",
                table: "Comparables",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "2ea3a6fd-0611-45d3-a653-4779dc46ea9c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "4347da42-0cfe-425c-bb2c-3f97a92349ba");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "3bd1be48-1e95-4f5b-bf77-27d248e8618c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "586b0e32-a53a-4852-aab7-b8adcafebcfe");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "c4c8668e-345b-43bd-a263-4beeb67ecafd");

            migrationBuilder.CreateIndex(
                name: "IX_Comparables_ComparableId",
                table: "Comparables",
                column: "ComparableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comparables_Comparables_ComparableId",
                table: "Comparables",
                column: "ComparableId",
                principalTable: "Comparables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comparables_Comparables_ComparableId",
                table: "Comparables");

            migrationBuilder.DropIndex(
                name: "IX_Comparables_ComparableId",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "ComparableId",
                table: "Comparables");

            migrationBuilder.AddColumn<Guid>(
                name: "ComparableId",
                table: "PropertyDetails",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "f7ea41f8-9ebe-4c12-8a9f-831925347a4f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "2ae09323-b1b8-42db-9745-5db59e96cc10");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "1de9888a-472f-4f84-9328-9f633cff19d5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "8ca864ea-3c71-4070-9403-6f0b85f07efe");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "13b83c27-66fc-487a-a57f-d83cc73d91ac");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetails_ComparableId",
                table: "PropertyDetails",
                column: "ComparableId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDetails_Comparables_ComparableId",
                table: "PropertyDetails",
                column: "ComparableId",
                principalTable: "Comparables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
