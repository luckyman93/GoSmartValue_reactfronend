using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class Instruction_addedClientAndValuerDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Instructions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ClientMobileNumber",
                table: "Instructions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "Instructions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocalityId",
                table: "Instructions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LocalityName",
                table: "Instructions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationName",
                table: "Instructions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlotNumber",
                table: "Instructions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PreferredAccessDate",
                table: "Instructions",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "b82e9220-0d38-41c1-b6e0-87054add3166");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "a7df1ab5-9c01-4a6f-9507-f23b2a04c5d2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "ffffb1f8-1812-4e86-b91f-71263bb93e93");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "3593c452-11b1-4d48-8353-4a4abeb0fd70");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "ec768f13-e945-4ea9-8285-7906650e6a23");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_AccountId",
                table: "Instructions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_LocalityId",
                table: "Instructions",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_LocationId",
                table: "Instructions",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_ValuerId",
                table: "Instructions",
                column: "ValuerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_Accounts_AccountId",
                table: "Instructions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_Localities_LocalityId",
                table: "Instructions",
                column: "LocalityId",
                principalTable: "Localities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_Locations_LocationId",
                table: "Instructions",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_AspNetUsers_ValuerId",
                table: "Instructions",
                column: "ValuerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_Accounts_AccountId",
                table: "Instructions");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_Localities_LocalityId",
                table: "Instructions");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_Locations_LocationId",
                table: "Instructions");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_AspNetUsers_ValuerId",
                table: "Instructions");

            migrationBuilder.DropIndex(
                name: "IX_Instructions_AccountId",
                table: "Instructions");

            migrationBuilder.DropIndex(
                name: "IX_Instructions_LocalityId",
                table: "Instructions");

            migrationBuilder.DropIndex(
                name: "IX_Instructions_LocationId",
                table: "Instructions");

            migrationBuilder.DropIndex(
                name: "IX_Instructions_ValuerId",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "ClientMobileNumber",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "LocalityId",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "LocalityName",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "PlotNumber",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "PreferredAccessDate",
                table: "Instructions");

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
    }
}
