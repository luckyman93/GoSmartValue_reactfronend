using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class FixPackageFeatureFKToPackage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackageFeature_PackageFeature_PackageId",
                table: "PackageFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_PackageFeature_Packages_PackageId1",
                table: "PackageFeature");

            migrationBuilder.DropIndex(
                name: "IX_PackageFeature_PackageId1",
                table: "PackageFeature");

            migrationBuilder.DropColumn(
                name: "PackageId1",
                table: "PackageFeature");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "dcd1a182-2f4c-46cd-ac19-bed6e4c563b8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "54490829-3039-4987-be69-4fb09b72342b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "023f0963-01cd-4c91-afbd-834e5a3adf44");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "2a1304ca-4162-41a8-aa79-ad53e8f25152");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "bfa3c1f0-485c-4690-94da-6d010cc1e408");

            migrationBuilder.AddForeignKey(
                name: "FK_PackageFeature_Packages_PackageId",
                table: "PackageFeature",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackageFeature_Packages_PackageId",
                table: "PackageFeature");

            migrationBuilder.AddColumn<int>(
                name: "PackageId1",
                table: "PackageFeature",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "ab9aa287-3f4a-465c-a6fc-992dc0c5de08");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "3b068dc4-ef75-475f-ad74-77ed01729138");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "bd09d402-5dba-401e-a937-2f483db5300e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "3898c7f5-b1ed-452c-b50e-19e1b9139e96");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "99273a71-7c25-4995-a11c-32a4e8cf89a5");

            migrationBuilder.CreateIndex(
                name: "IX_PackageFeature_PackageId1",
                table: "PackageFeature",
                column: "PackageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PackageFeature_PackageFeature_PackageId",
                table: "PackageFeature",
                column: "PackageId",
                principalTable: "PackageFeature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PackageFeature_Packages_PackageId1",
                table: "PackageFeature",
                column: "PackageId1",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
