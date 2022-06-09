using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class InstructionEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instructions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    ValuerId = table.Column<Guid>(nullable: false),
                    AuthorityDocument = table.Column<byte[]>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.Id);
                });

                migrationBuilder.UpdateData(
                    table: "AspNetRoles",
                    keyColumn: "Id",
                    keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                    column: "ConcurrencyStamp",
                    value: "58d46087-4a80-4f58-99d9-e6327432d5ee");

                migrationBuilder.UpdateData(
                    table: "AspNetRoles",
                    keyColumn: "Id",
                    keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                    column: "ConcurrencyStamp",
                    value: "9475a1e3-ccf1-49ac-9d29-7a706d6cb460");

                migrationBuilder.UpdateData(
                    table: "AspNetRoles",
                    keyColumn: "Id",
                    keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                    column: "ConcurrencyStamp",
                    value: "2b530908-d452-4f38-879f-a5c8fc5cab91");

                migrationBuilder.UpdateData(
                    table: "AspNetRoles",
                    keyColumn: "Id",
                    keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                    column: "ConcurrencyStamp",
                    value: "4ded70ce-36cb-4ae1-97d2-27d2c13091be");

                migrationBuilder.UpdateData(
                    table: "AspNetRoles",
                    keyColumn: "Id",
                    keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                    column: "ConcurrencyStamp",
                    value: "5be31669-87a7-4586-86a9-0d854d6afbc6");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "c8ae848a-a251-4308-97ec-6985c3f25ef9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "5d2609e8-7e99-4be5-bee7-db0fd3a4dbdf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "53f2b2fc-4bd0-4110-bcfb-5b33cbc2524f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "eb316e13-42f3-466b-846f-84309602e8b3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "0cf92439-6543-4f96-9056-f78afc7ee865");
        }
    }
}
