using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedCanReIssuedFlagOnInstruction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthorityDocument",
                table: "Instructions",
                newName: "AuthorityDoc");

            migrationBuilder.AddColumn<bool>(
                name: "CanBeReIssued",
                table: "Instructions",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentInstructionId",
                table: "Instructions",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "60a9d0ac-5aa7-42f2-b0fc-b1987580cc86");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "aa11c1ee-0182-495f-bf62-fa461ec109eb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "d7923622-b192-41d6-b6ca-bfb8a7ccfdc9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "e102e559-50a4-4ec0-9133-9481431f7068");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "70cce743-0dd7-439a-9bd1-16a8ab4aeac7");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_ParentInstructionId",
                table: "Instructions",
                column: "ParentInstructionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_Instructions_ParentInstructionId",
                table: "Instructions",
                column: "ParentInstructionId",
                principalTable: "Instructions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_Instructions_ParentInstructionId",
                table: "Instructions");

            migrationBuilder.DropIndex(
                name: "IX_Instructions_ParentInstructionId",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "CanBeReIssued",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "ParentInstructionId",
                table: "Instructions");

            migrationBuilder.RenameColumn(
                name: "AuthorityDoc",
                table: "Instructions",
                newName: "AuthorityDocument");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "10d0e124-85b2-4862-8990-e90ae803ddbc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "595b369d-1562-4199-9707-b1fbd8f9eb92");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "14e9805e-9506-4e06-baa2-f8fa15c92d74");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "6e27a9bf-d34c-452f-8bc9-e57eb4a3b9e4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "948906e1-bcea-48a7-bd10-f24137daa8ad");
        }
    }
}
