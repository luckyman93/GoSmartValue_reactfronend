using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedMorePropertiesToCOmparables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "Comparables",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BondAmount",
                table: "Comparables",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BondNumber",
                table: "Comparables",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BuyerId",
                table: "Comparables",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BuyerSalutation",
                table: "Comparables",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SellerId",
                table: "Comparables",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SellerSalutation",
                table: "Comparables",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "Comparables",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "3c89e7ad-733f-4d4f-8bba-3451ff9c34b2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "202e1b9b-ea7f-46eb-b51c-c7cc44bb7a8c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "5902a291-6b52-41e9-b738-4f8378e2f996");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "87582e30-765b-4219-995a-93998ecfd7d9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "d59173bf-6c7d-4b48-8d3e-feaa0194b6dc");

            migrationBuilder.CreateIndex(
                name: "IX_Comparables_BuyerId",
                table: "Comparables",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comparables_SellerId",
                table: "Comparables",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comparables_Buyer_BuyerId",
                table: "Comparables",
                column: "BuyerId",
                principalTable: "Buyer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comparables_Seller_SellerId",
                table: "Comparables",
                column: "SellerId",
                principalTable: "Seller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comparables_Buyer_BuyerId",
                table: "Comparables");

            migrationBuilder.DropForeignKey(
                name: "FK_Comparables_Seller_SellerId",
                table: "Comparables");

            migrationBuilder.DropIndex(
                name: "IX_Comparables_BuyerId",
                table: "Comparables");

            migrationBuilder.DropIndex(
                name: "IX_Comparables_SellerId",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "BondAmount",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "BondNumber",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "BuyerSalutation",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "SellerSalutation",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Comparables");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "2ea3a6fd-0611-45d3-a653-4779dc46ea9c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "4347da42-0cfe-425c-bb2c-3f97a92349ba");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "3bd1be48-1e95-4f5b-bf77-27d248e8618c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "586b0e32-a53a-4852-aab7-b8adcafebcfe");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "c4c8668e-345b-43bd-a263-4beeb67ecafd");
        }
    }
}
