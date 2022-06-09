using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedNavigationToValuationFromInstruction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Valuations_Instructions_InstructionId",
                table: "Valuations");

            migrationBuilder.DropIndex(
                name: "IX_Valuations_InstructionId",
                table: "Valuations");

            migrationBuilder.AddColumn<Guid>(
                name: "ValuationId",
                table: "Instructions",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "785b7a39-7a55-4543-99e9-e0fc5b6a0dfc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "dbfb7891-16d8-43a2-a213-3a0ddbc572e9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "70438399-f22b-447a-b71d-4590d8906cd6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "6df88f2c-6c35-4b2b-83aa-5cd6765c47d4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "ac2b141b-9e06-44a4-af19-5705b0f8cb48");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_ValuationId",
                table: "Instructions",
                column: "ValuationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_Valuations_ValuationId",
                table: "Instructions",
                column: "ValuationId",
                principalTable: "Valuations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_Valuations_ValuationId",
                table: "Instructions");

            migrationBuilder.DropIndex(
                name: "IX_Instructions_ValuationId",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "ValuationId",
                table: "Instructions");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "2ae86949-6f97-4ba1-9f6a-91ecc583a540");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "4794bcfd-d464-45f8-8622-2753127a6be3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "75fa87e3-2172-447b-9ada-29c56d38a4f1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "cc417dc9-ff02-49ed-803a-6640169a2568");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "94eaadd7-9a57-4e6c-baaf-171d338d9a8e");

            migrationBuilder.CreateIndex(
                name: "IX_Valuations_InstructionId",
                table: "Valuations",
                column: "InstructionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Valuations_Instructions_InstructionId",
                table: "Valuations",
                column: "InstructionId",
                principalTable: "Instructions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
