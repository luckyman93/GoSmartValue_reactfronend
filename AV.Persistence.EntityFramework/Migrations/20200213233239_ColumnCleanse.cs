using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class ColumnCleanse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comparables_Plots_PlotId",
                table: "Comparables");

            migrationBuilder.DropIndex(
                name: "IX_Comparables_PlotId",
                table: "Comparables");

            migrationBuilder.AlterColumn<int>(
                name: "PlotId",
                table: "Comparables",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlotId1",
                table: "Comparables",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StreetId",
                table: "Comparables",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "20737432-f0fd-4b26-8745-c97f33bd8249");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "e8bbcdd4-380f-40cf-9108-90ef6bea8161");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "738f6856-07a8-4026-9d83-9d71c841e38a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "f6d6506f-62ea-4a1a-a733-43372063ead5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "db5e0fa4-d0ad-439e-9eee-7ce757542164");

            migrationBuilder.CreateIndex(
                name: "IX_Comparables_PlotId1",
                table: "Comparables",
                column: "PlotId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comparables_Plots_PlotId1",
                table: "Comparables",
                column: "PlotId1",
                principalTable: "Plots",
                principalColumn: "PlotId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comparables_Plots_PlotId1",
                table: "Comparables");

            migrationBuilder.DropIndex(
                name: "IX_Comparables_PlotId1",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "PlotId1",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "StreetId",
                table: "Comparables");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlotId",
                table: "Comparables",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "b3cb4c22-5b79-43d3-97ee-56fae8aa4a9e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "98273af2-fe91-4dc0-9b07-4e6374856c40");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "8265f17f-802d-459a-bea7-c44e56bcd175");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "d32c4c2e-fdaf-49e6-8441-178b2885850b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "0c866753-18b7-4cfe-9bca-3e85159e19e9");

            migrationBuilder.CreateIndex(
                name: "IX_Comparables_PlotId",
                table: "Comparables",
                column: "PlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comparables_Plots_PlotId",
                table: "Comparables",
                column: "PlotId",
                principalTable: "Plots",
                principalColumn: "PlotId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
