using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class RenamedPropertDetailToPropertySale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComparableResults_PropertyDetails_PropertyDetailId",
                table: "ComparableResults");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDetails_PropertyDetails_PropertyDetailId",
                table: "PropertyDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ValuationRequestsDelete_PropertyDetails_PropertyDetailId",
                table: "ValuationRequestsDelete");

            migrationBuilder.RenameColumn(
                name: "PropertyDetailId",
                table: "ValuationRequestsDelete",
                newName: "PropertySaleId");

            migrationBuilder.RenameIndex(
                name: "IX_ValuationRequestsDelete_PropertyDetailId",
                table: "ValuationRequestsDelete",
                newName: "IX_ValuationRequestsDelete_PropertySaleId");

            migrationBuilder.RenameColumn(
                name: "PropertyDetailId",
                table: "PropertyDetails",
                newName: "PropertySaleId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyDetails_PropertyDetailId",
                table: "PropertyDetails",
                newName: "IX_PropertyDetails_PropertySaleId");

            migrationBuilder.RenameColumn(
                name: "PropertyDetailId",
                table: "ComparableResults",
                newName: "PropertySaleId");

            migrationBuilder.RenameIndex(
                name: "IX_ComparableResults_PropertyDetailId",
                table: "ComparableResults",
                newName: "IX_ComparableResults_PropertySaleId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ComparableResults_PropertyDetails_PropertySaleId",
                table: "ComparableResults",
                column: "PropertySaleId",
                principalTable: "PropertyDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDetails_PropertyDetails_PropertySaleId",
                table: "PropertyDetails",
                column: "PropertySaleId",
                principalTable: "PropertyDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ValuationRequestsDelete_PropertyDetails_PropertySaleId",
                table: "ValuationRequestsDelete",
                column: "PropertySaleId",
                principalTable: "PropertyDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComparableResults_PropertyDetails_PropertySaleId",
                table: "ComparableResults");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDetails_PropertyDetails_PropertySaleId",
                table: "PropertyDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ValuationRequestsDelete_PropertyDetails_PropertySaleId",
                table: "ValuationRequestsDelete");

            migrationBuilder.RenameColumn(
                name: "PropertySaleId",
                table: "ValuationRequestsDelete",
                newName: "PropertyDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_ValuationRequestsDelete_PropertySaleId",
                table: "ValuationRequestsDelete",
                newName: "IX_ValuationRequestsDelete_PropertyDetailId");

            migrationBuilder.RenameColumn(
                name: "PropertySaleId",
                table: "PropertyDetails",
                newName: "PropertyDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyDetails_PropertySaleId",
                table: "PropertyDetails",
                newName: "IX_PropertyDetails_PropertyDetailId");

            migrationBuilder.RenameColumn(
                name: "PropertySaleId",
                table: "ComparableResults",
                newName: "PropertyDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_ComparableResults_PropertySaleId",
                table: "ComparableResults",
                newName: "IX_ComparableResults_PropertyDetailId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "4d41b227-f304-4b10-aeab-42c3166ea7ca");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "f3994235-7b40-4318-a8df-79836d722062");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "a50a2345-6d54-44ff-896e-6fd4080a8e41");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "6be01cf4-6012-4839-bcaa-135daac132e0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "763f27b7-1af6-4a8e-821a-1029254223c7");

            migrationBuilder.AddForeignKey(
                name: "FK_ComparableResults_PropertyDetails_PropertyDetailId",
                table: "ComparableResults",
                column: "PropertyDetailId",
                principalTable: "PropertyDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDetails_PropertyDetails_PropertyDetailId",
                table: "PropertyDetails",
                column: "PropertyDetailId",
                principalTable: "PropertyDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ValuationRequestsDelete_PropertyDetails_PropertyDetailId",
                table: "ValuationRequestsDelete",
                column: "PropertyDetailId",
                principalTable: "PropertyDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
