using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddStandardUserFeatureMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_UserId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionOption_Packages_PackageId",
                table: "SubscriptionOption");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_UserId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetRoles");

            migrationBuilder.AlterColumn<int>(
                name: "PackageId",
                table: "SubscriptionOption",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DetailedReportsLimit",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InstantReportsLimit",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "DiscountPerReferral",
                table: "Packages",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "MaximumSubAccounts",
                table: "Packages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "Instructions",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "paymentStatus",
                table: "Instructions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "Comparables",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "paymentStatus",
                table: "Comparables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "AspNetUsers",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserRoles",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false);

            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "Accounts",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "DetailedReportsLimit",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "DiscountPerReferral",
                table: "Accounts",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Frequency",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InstantReportsLimit",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaximumSubAccounts",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "Accounts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionOptionId",
                table: "Accounts",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "f05bc11e-95af-4fd1-b52c-0b06b2f241bb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "b12ae5d1-102c-45db-8fdc-e6439f29b5d2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "5bd36fc7-aef6-4041-b460-0de0173dc187");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "72ddffbe-b03e-46cd-9688-159d00c105c9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "794aac33-cb68-4a37-b6b2-1ce47001f64d");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PackageId",
                table: "Products",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AccountId",
                table: "AspNetUsers",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PackageId",
                table: "Accounts",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_SubscriptionOptionId",
                table: "Accounts",
                column: "SubscriptionOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Packages_PackageId",
                table: "Accounts",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_SubscriptionOption_SubscriptionOptionId",
                table: "Accounts",
                column: "SubscriptionOptionId",
                principalTable: "SubscriptionOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Accounts_AccountId",
                table: "AspNetUsers",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Packages_PackageId",
                table: "Products",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionOption_Packages_PackageId",
                table: "SubscriptionOption",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Packages_PackageId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_SubscriptionOption_SubscriptionOptionId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Accounts_AccountId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Packages_PackageId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionOption_Packages_PackageId",
                table: "SubscriptionOption");

            migrationBuilder.DropIndex(
                name: "IX_Products_PackageId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AccountId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_PackageId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_SubscriptionOptionId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "DetailedReportsLimit",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "InstantReportsLimit",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DiscountPerReferral",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "MaximumSubAccounts",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "paymentStatus",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "paymentStatus",
                table: "Comparables");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "DetailedReportsLimit",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "DiscountPerReferral",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "InstantReportsLimit",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "MaximumSubAccounts",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "SubscriptionOptionId",
                table: "Accounts");

            migrationBuilder.AlterColumn<int>(
                name: "PackageId",
                table: "SubscriptionOption",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "AspNetRoles",
                type: "char(36)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "d802badb-594e-4f74-b090-b2efc3ca230e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "fbeed515-0e16-408b-a738-e4cb0db42674");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "1efa81b5-3b08-4540-b7ac-5951e2933ea0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "dba15b56-de1e-4664-b154-eca38261115c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "5630df6f-b869-41d7-b7c7-b437d06b6b90");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_UserId",
                table: "AspNetRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_UserId",
                table: "AspNetRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionOption_Packages_PackageId",
                table: "SubscriptionOption",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
