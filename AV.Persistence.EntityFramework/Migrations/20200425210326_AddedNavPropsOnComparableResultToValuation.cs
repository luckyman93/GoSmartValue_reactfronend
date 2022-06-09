using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedNavPropsOnComparableResultToValuation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComparableResults_PropertyDetails_PropertySaleId",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "PropertyDetailsId",
                table: "ComparableResults");

            migrationBuilder.RenameColumn(
                name: "PropertySaleId",
                table: "ComparableResults",
                newName: "ValuationId");

            migrationBuilder.RenameIndex(
                name: "IX_ComparableResults_PropertySaleId",
                table: "ComparableResults",
                newName: "IX_ComparableResults_ValuationId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "fee65219-c5de-4ca6-8a2e-ed3ff69c547b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "70ea97cd-a52e-4964-aa38-1b09e09843bb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "4db01760-94e4-4524-885e-4206a845bf7f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "71d8a932-c99c-4636-870f-157a35571876");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "dd5b969b-052e-4ba8-a923-201709fe2413");

            migrationBuilder.AddForeignKey(
                name: "FK_ComparableResults_Valuations_ValuationId",
                table: "ComparableResults",
                column: "ValuationId",
                principalTable: "Valuations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComparableResults_Valuations_ValuationId",
                table: "ComparableResults");

            migrationBuilder.RenameColumn(
                name: "ValuationId",
                table: "ComparableResults",
                newName: "PropertySaleId");

            migrationBuilder.RenameIndex(
                name: "IX_ComparableResults_ValuationId",
                table: "ComparableResults",
                newName: "IX_ComparableResults_PropertySaleId");

            migrationBuilder.AddColumn<int>(
                name: "PropertyDetailsId",
                table: "ComparableResults",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "8264c77b-72d7-4127-9f43-ceecc2a3e019");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "53252ba8-8691-4d89-b939-ee765dd92fdb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "f308656f-5181-4157-966a-4c88b4a31474");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "b78c63fe-2fd1-441c-b441-9d6462bfb043");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "1809e4b6-3e3c-42b5-946f-081e853e45f2");

            migrationBuilder.AddForeignKey(
                name: "FK_ComparableResults_PropertyDetails_PropertySaleId",
                table: "ComparableResults",
                column: "PropertySaleId",
                principalTable: "PropertyDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
