using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedDocumentstreamsToSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hasWhatsapp",
                table: "Instructions");

            migrationBuilder.RenameColumn(
                name: "isVideoWakthroughPossible",
                table: "Instructions",
                newName: "IsVideoWalkthroughPossible");

            migrationBuilder.CreateTable(
                name: "DocumentStreams",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MimeType = table.Column<string>(nullable: true),
                    FileStream = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentStreams", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "7d4a9eec-322c-4790-89ec-98b7832bda08");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "7ad662d4-9b14-49f7-aa4f-02e1ec8c5f4e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "dfc6ddff-25b5-4f3a-b1de-67f0327b8cc3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "3ecd5893-d2e7-484a-9b83-aa1d8cf47271");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "a3f3c3ab-8cc2-4e55-977f-26566c219795");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_DocumentStreams_DocumentStreamId",
                table: "Documents");

            migrationBuilder.DropTable(
                name: "DocumentStreams");

            migrationBuilder.DropIndex(
                name: "IX_Documents_DocumentStreamId",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "IsVideoWalkthroughPossible",
                table: "Instructions",
                newName: "isVideoWakthroughPossible");

            migrationBuilder.AddColumn<bool>(
                name: "hasWhatsapp",
                table: "Instructions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "bca667f7-502a-4058-bdde-831ba771361d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "6e3eb805-195a-4e49-b99d-f68a88a48d59");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "0d56f37e-0941-4b0a-9af4-383ce5c2746e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "be772fe4-f2b2-40d6-8776-1bc0f77ef0dc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "c0f3d99b-e508-4aef-abfc-113b1a3690a7");
        }
    }
}
