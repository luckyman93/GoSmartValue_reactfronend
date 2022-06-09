using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddSubscriptEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackageFeature",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    GroupId = table.Column<int>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    PackageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageFeature_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Frequency = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    PackageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionOption_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "5d0b6438-ea74-4694-86a1-8120c5b07b74");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "dcad14ba-8855-4d8e-bf6c-5e9228e832dd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "7cec481a-5fac-4857-8fdb-083d4644bf48");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "4c5cb51a-0de7-41a9-9cac-f5eb839f5c13");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "f41ef90e-36ad-48c8-af6c-9ad9e98aa4e1");

            migrationBuilder.CreateIndex(
                name: "IX_PackageFeature_PackageId",
                table: "PackageFeature",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionOption_PackageId",
                table: "SubscriptionOption",
                column: "PackageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plots_PlotShape_polygonShapeId",
                table: "Plots");

            migrationBuilder.DropTable(
                name: "PackageFeature");

            migrationBuilder.DropTable(
                name: "SubscriptionOption");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.AlterColumn<double>(
                name: "polygonShapeId",
                table: "Plots",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<Guid>(
                name: "BankAccountId",
                table: "Accounts",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "83f5a14d-3e6f-4c2b-bfdd-d112de67e9b5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "438aaf9d-5281-4ff7-b6aa-7925a5fdc895");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "e1c354ef-b558-47a7-a1e7-ee3d0501cb14");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "4ec6b901-5764-43ce-a768-b75a4dcc1b25");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "c3e46232-1003-4cf9-a0df-6b55aa16ee66");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_BankAccountId",
                table: "Accounts",
                column: "BankAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_BankAccounts_BankAccountId",
                table: "Accounts",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Plots_PlotShape_polygonShapeId",
                table: "Plots",
                column: "polygonShapeId",
                principalTable: "PlotShape",
                principalColumn: "ShapeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
