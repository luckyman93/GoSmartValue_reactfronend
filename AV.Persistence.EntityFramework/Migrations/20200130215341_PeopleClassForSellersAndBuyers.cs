using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class PeopleClassForSellersAndBuyers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "PropertyDetails",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "PropertyDetails",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "PropertyDetails",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BondAmount",
                table: "PropertyDetails",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "BuyerId",
                table: "PropertyDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Metric",
                table: "PropertyDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "SellerId",
                table: "PropertyDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "PropertyDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Buyer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Salutation = table.Column<int>(nullable: false),
                    IdentityNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seller",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Salutation = table.Column<int>(nullable: false),
                    IdentityNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seller", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "90780de0-fcf4-4cdb-a638-a9fdc67f56c0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "e1ba3f8d-4a15-4f48-b87c-01e1c6e2c9cd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "1e507ef6-8dc5-4b73-9cc2-dafd23bdc4ac");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "1fca13de-3b83-4f08-be13-697227c2fcab");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "25d5b55c-4297-4196-a508-71204a95a6aa");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetails_BuyerId",
                table: "PropertyDetails",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetails_SellerId",
                table: "PropertyDetails",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDetails_Buyer_BuyerId",
                table: "PropertyDetails",
                column: "BuyerId",
                principalTable: "Buyer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDetails_Seller_SellerId",
                table: "PropertyDetails",
                column: "SellerId",
                principalTable: "Seller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDetails_Buyer_BuyerId",
                table: "PropertyDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDetails_Seller_SellerId",
                table: "PropertyDetails");

            migrationBuilder.DropTable(
                name: "Buyer");

            migrationBuilder.DropTable(
                name: "Seller");

            migrationBuilder.DropIndex(
                name: "IX_PropertyDetails_BuyerId",
                table: "PropertyDetails");

            migrationBuilder.DropIndex(
                name: "IX_PropertyDetails_SellerId",
                table: "PropertyDetails");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "PropertyDetails");

            migrationBuilder.DropColumn(
                name: "BondAmount",
                table: "PropertyDetails");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "PropertyDetails");

            migrationBuilder.DropColumn(
                name: "Metric",
                table: "PropertyDetails");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "PropertyDetails");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "PropertyDetails");

            migrationBuilder.AlterColumn<string>(
                name: "Longitude",
                table: "PropertyDetails",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Latitude",
                table: "PropertyDetails",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "47c823b0-8f24-4591-bbb6-25e50ab46c9c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "2baef0aa-9c79-48c4-bee5-d542fe59b1c9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "7dbe6b7f-eef3-4e3a-8bf4-51da30966797");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "9d016f76-3e1d-4970-8a34-ee3146ad8d25");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "0ef751da-d815-44a0-b565-c8f862b3d642");
        }
    }
}
