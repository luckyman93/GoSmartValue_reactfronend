using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedAccountBankDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_BankAccount_BankAccountId",
                table: "Accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccount",
                table: "BankAccount");

            migrationBuilder.RenameTable(
                name: "BankAccount",
                newName: "BankAccounts");

            migrationBuilder.RenameColumn(
                name: "bankName",
                table: "BankAccounts",
                newName: "BankName");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Documents",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BankAccountId",
                table: "Documents",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "BankAccounts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BankAccounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "BankAccounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccounts",
                table: "BankAccounts",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "3c69a36a-a587-4b25-b170-e0f1ea8a81e5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "b9d0b6e6-b247-476b-8a5b-d7fa95a695ae");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "11e6a0eb-4080-43c0-8242-0f87c997465c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "e7e4a252-c72d-4b2c-adf7-3ed6d2398e49");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "c72df65b-6e68-419b-ad24-4665a1b2eadd");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_AccountId",
                table: "Documents",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_BankAccountId",
                table: "Documents",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_AccountId",
                table: "BankAccounts",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_BankAccounts_BankAccountId",
                table: "Accounts",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Accounts_AccountId",
                table: "BankAccounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Accounts_AccountId",
                table: "Documents",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_BankAccounts_BankAccountId",
                table: "Documents",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_BankAccounts_BankAccountId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Accounts_AccountId",
                table: "BankAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Accounts_AccountId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_BankAccounts_BankAccountId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_AccountId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_BankAccountId",
                table: "Documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccounts",
                table: "BankAccounts");

            migrationBuilder.DropIndex(
                name: "IX_BankAccounts_AccountId",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "BankAccountId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "BankAccounts");

            migrationBuilder.RenameTable(
                name: "BankAccounts",
                newName: "BankAccount");

            migrationBuilder.RenameColumn(
                name: "BankName",
                table: "BankAccount",
                newName: "bankName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccount",
                table: "BankAccount",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "e9b75d03-919f-4e52-97c9-2940da1c99c0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "4febdc97-7af4-4164-8f95-004698d608c0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "8972b652-5352-4abb-bf6d-b4a0b12a4c91");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "7d36c87b-23ae-4e2f-9a23-ede67c33103b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "79a3db85-9132-415c-a588-d6dca415a3b1");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_BankAccount_BankAccountId",
                table: "Accounts",
                column: "BankAccountId",
                principalTable: "BankAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
