using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class ModifiedInstructionToHoldListOfDocumentIdsInstead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorityDoc",
                table: "Instructions");

            migrationBuilder.AddColumn<Guid>(
                name: "InstructionId",
                table: "Document",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "d50855f3-3d18-4f79-ab80-165fa2a12d0d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "734d9008-22fd-48a1-aa4b-3d95c060d7a3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "74b142fc-c2dd-446f-aadd-2ab3024a890b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "4bf1b234-1071-4502-8232-a980fe0713d9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "52203037-406c-4a62-a034-77f5765c8021");

            migrationBuilder.CreateIndex(
                name: "IX_Document_InstructionId",
                table: "Document",
                column: "InstructionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Instructions_InstructionId",
                table: "Document",
                column: "InstructionId",
                principalTable: "Instructions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Instructions_InstructionId",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_InstructionId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "InstructionId",
                table: "Document");

            migrationBuilder.AddColumn<byte[]>(
                name: "AuthorityDoc",
                table: "Instructions",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "5cae3e34-981c-4402-a7ce-276fa43b437c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "350b3028-0877-467e-8529-c14624655b3a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "aa1243c4-84ed-435d-a891-00f010aae651");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "fbf12ce5-60cf-4e49-821a-afd702ac7e3a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "a42a43b4-a97a-45d5-b51f-dc89afb85c14");
        }
    }
}
