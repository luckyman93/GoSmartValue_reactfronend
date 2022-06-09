using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedMoreOrganisationProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocalityId",
                table: "Organisations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Organisations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PlotNo",
                table: "Organisations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StreetId",
                table: "Organisations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetName",
                table: "Organisations",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Account",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_LocalityId",
                table: "Organisations",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_LocationId",
                table: "Organisations",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organisations_Localities_LocalityId",
                table: "Organisations",
                column: "LocalityId",
                principalTable: "Localities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Organisations_Locations_LocationId",
                table: "Organisations",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_AspNetUsers_UserId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Organisations_Localities_LocalityId",
                table: "Organisations");

            migrationBuilder.DropForeignKey(
                name: "FK_Organisations_Locations_LocationId",
                table: "Organisations");

            migrationBuilder.DropIndex(
                name: "IX_Organisations_LocalityId",
                table: "Organisations");

            migrationBuilder.DropIndex(
                name: "IX_Organisations_LocationId",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "LocalityId",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "PlotNo",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "StreetId",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "StreetName",
                table: "Organisations");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Account",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "1c2efe48-155b-4dcf-ae85-e16bfd666dd0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "06ba3359-c2e6-435c-836e-82d7492d7dab");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "c19dff0e-cb98-41a0-b222-beb4f43fedf3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "74313aaf-fc22-418b-be98-e2907731ac6f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "7d938109-1235-4a1b-80ff-f30fd38aeb6b");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_AspNetUsers_UserId",
                table: "Account",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
