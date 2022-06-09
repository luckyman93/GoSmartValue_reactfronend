using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedForeignKeyToInstructionFromItsDocuments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Documents",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "InstructionId",
                table: "Documents",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "2a4df6c5-2d76-46e1-9644-56da581c471e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "78a7ac7d-bb48-4555-97de-b64bb4b09236");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "ce0739d6-c1ce-4bcb-986c-23a44784dc2d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "85381025-ad85-4347-a3b0-1109c91d24a5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "3eda2eb5-ed8a-45c0-adab-e1e3d4f1cfc7");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_InstructionId",
                table: "Documents",
                column: "InstructionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Instructions_InstructionId",
                table: "Documents",
                column: "InstructionId",
                principalTable: "Instructions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Instructions_InstructionId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_InstructionId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "InstructionId",
                table: "Documents");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "affc1537-12fc-479b-9624-7d01cd665b11");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "4e777bfd-ba18-4f1f-8936-e5ca15637d59");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "1832bb23-4742-49cc-81de-b9e3960bdd1e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "c8d8225d-b368-4456-9411-659e4e484f84");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "55501862-de21-467b-991f-b7b6401a787b");
        }
    }
}
