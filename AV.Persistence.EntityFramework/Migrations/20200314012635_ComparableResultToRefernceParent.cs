using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class ComparableResultToRefernceParent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comparables_ComparableResults_ComparableResultId",
                table: "Comparables");

            migrationBuilder.DropIndex(
                name: "IX_Comparables_ComparableResultId",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "ComparableResultId",
                table: "Comparables");

            migrationBuilder.AddColumn<Guid>(
                name: "ComparableId",
                table: "ComparableResults",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ComparableRequestId",
                table: "ComparableResults",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "9cd9bb0b-4807-4174-a564-5816dd71cebd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "3ebe0f04-957a-4a7c-88f4-76705c8fe5ae");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "03cfadb2-92ff-4b57-a2d4-fab991dca70b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "a58bdab7-6a68-4878-86b4-7dbce562fe37");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "ad1375f7-08e4-48c5-8660-f240c9280b45");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComparableId",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "ComparableRequestId",
                table: "ComparableResults");

            migrationBuilder.AddColumn<Guid>(
                name: "ComparableResultId",
                table: "Comparables",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "e69b939f-6ec7-4802-9d11-d903af1d42a3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "78491f1a-5121-4d55-82a3-579da8485b96");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "e9fa5650-44d4-40d2-9d45-60dd58bb9427");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "37d90cea-c69b-4d99-b7b4-2a8c56c75a75");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "00e72ec2-44fd-4a59-9506-ffb7b4affe5e");

            migrationBuilder.CreateIndex(
                name: "IX_Comparables_ComparableResultId",
                table: "Comparables",
                column: "ComparableResultId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comparables_ComparableResults_ComparableResultId",
                table: "Comparables",
                column: "ComparableResultId",
                principalTable: "ComparableResults",
                principalColumn: "ReferenceNumber",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
