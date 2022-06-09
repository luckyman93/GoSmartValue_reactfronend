using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedValuationReportRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ValuationRequests_AspNetUsers_ClientId",
                table: "ValuationRequests");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "ValuationRequests",
                newName: "RequesterUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ValuationRequests_ClientId",
                table: "ValuationRequests",
                newName: "IX_ValuationRequests_RequesterUserId");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientUserId",
                table: "ValuationRequests",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "6d2ea76c-1181-4ee6-9956-d191a1d9138a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "6b7459a2-4eca-4111-aa62-3af4ff9b282e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "92ca3361-0638-4dae-b48d-16881df2f0ff");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "d84afcfd-14d9-4e51-94bc-6f5bcd41084c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "19f8652e-d951-493a-87fb-414f665c4951");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequests_ClientUserId",
                table: "ValuationRequests",
                column: "ClientUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ValuationRequests_AspNetUsers_ClientUserId",
                table: "ValuationRequests",
                column: "ClientUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ValuationRequests_AspNetUsers_RequesterUserId",
                table: "ValuationRequests",
                column: "RequesterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ValuationRequests_AspNetUsers_ClientUserId",
                table: "ValuationRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ValuationRequests_AspNetUsers_RequesterUserId",
                table: "ValuationRequests");

            migrationBuilder.DropIndex(
                name: "IX_ValuationRequests_ClientUserId",
                table: "ValuationRequests");

            migrationBuilder.DropColumn(
                name: "ClientUserId",
                table: "ValuationRequests");

            migrationBuilder.RenameColumn(
                name: "RequesterUserId",
                table: "ValuationRequests",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_ValuationRequests_RequesterUserId",
                table: "ValuationRequests",
                newName: "IX_ValuationRequests_ClientId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "4dab1fff-afe0-4677-b89c-f710610f6faf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "339defe3-3a2e-483c-9ca5-70310b0d7792");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "e3afe8f0-c440-4001-879e-849f911ecced");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "ba3233c9-bb9f-4d54-86a9-303236000a44");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "933f3d73-1575-4644-a225-fcaa1b89a892");

            migrationBuilder.AddForeignKey(
                name: "FK_ValuationRequests_AspNetUsers_ClientId",
                table: "ValuationRequests",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
