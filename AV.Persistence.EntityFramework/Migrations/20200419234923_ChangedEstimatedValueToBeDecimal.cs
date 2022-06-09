using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class ChangedEstimatedValueToBeDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstimatedPropertyValue",
                table: "ComparableResults",
                newName: "EstimatedValue");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "9fba8c53-0b0b-4cae-9ba5-6401cbc0b85c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "16ccbf2b-9819-4c82-a1ad-6eb0a0b1c134");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "2b9e4388-211c-4493-a216-a7827e80d775");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "63b637e4-2e5b-42f6-8a68-627d6610fdd2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "f42d1e4c-d3fa-43e6-8a19-d2edef11a069");

            migrationBuilder.CreateIndex(
                name: "IX_ComparableResults_ComparableId",
                table: "ComparableResults",
                column: "ComparableId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComparableResults_Comparables_ComparableId",
                table: "ComparableResults",
                column: "ComparableId",
                principalTable: "Comparables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComparableResults_Comparables_ComparableId",
                table: "ComparableResults");

            migrationBuilder.DropIndex(
                name: "IX_ComparableResults_ComparableId",
                table: "ComparableResults");

            migrationBuilder.RenameColumn(
                name: "EstimatedValue",
                table: "ComparableResults",
                newName: "EstimatedPropertyValue");

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
    }
}
