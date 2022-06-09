using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class RefactoredProductPackageFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackageFeature_Packages_PackageId",
                table: "PackageFeature");

            migrationBuilder.DropColumn(
                name: "Audience",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DetailedReportsLimit",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "InstantReportsLimit",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Products",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "PackageFeature",
                newName: "PackageId1");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "SubscriptionOption",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(65,30)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AddColumn<string>(
                name: "SampleUrl",
                table: "Products",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DetailedReportLimit",
                table: "Packages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstantReportLimit",
                table: "Packages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentFrequency",
                table: "Packages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Packages",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "PackageId",
                table: "PackageFeature",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "Accounts",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.CreateTable(
                name: "ProductFeature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Description = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductFeature_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "462aa577-f3dc-4adb-ab0e-e0b744789873");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "9c4490d9-0c61-4e67-80dd-03314b7e7fe4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "5111836a-7149-4b3a-ad17-f60cfcff52dd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "092d6821-ae0b-4757-b4e3-01d30f11552a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "d7d78d07-20da-41e6-b074-5d53d498a02e");

            migrationBuilder.CreateIndex(
                name: "IX_PackageFeature_PackageId1",
                table: "PackageFeature",
                column: "PackageId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeature_ProductId",
                table: "ProductFeature",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_PackageFeature_PackageFeature_PackageId",
                table: "PackageFeature",
                column: "PackageId",
                principalTable: "PackageFeature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PackageFeature_Packages_PackageId1",
                table: "PackageFeature",
                column: "PackageId1",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackageFeature_PackageFeature_PackageId",
                table: "PackageFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_PackageFeature_Packages_PackageId1",
                table: "PackageFeature");

            migrationBuilder.DropTable(
                name: "ProductFeature");

            migrationBuilder.DropIndex(
                name: "IX_PackageFeature_PackageId1",
                table: "PackageFeature");

            migrationBuilder.DropColumn(
                name: "SampleUrl",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DetailedReportLimit",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "InstantReportLimit",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "PaymentFrequency",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Packages");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Products",
                newName: "Position");

            migrationBuilder.RenameColumn(
                name: "PackageId1",
                table: "PackageFeature",
                newName: "GroupId");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "SubscriptionOption",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Audience",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AlterColumn<int>(
                name: "PackageId",
                table: "PackageFeature",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "Cost",
                table: "Accounts",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PackageFeature_Packages_PackageId",
                table: "PackageFeature",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
