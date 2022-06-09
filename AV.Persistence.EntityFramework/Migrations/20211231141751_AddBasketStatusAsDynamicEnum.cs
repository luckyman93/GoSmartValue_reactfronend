using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddBasketStatusAsDynamicEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Baskets");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "Baskets",
                newName: "StatusId");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentId",
                table: "Baskets",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "BasketStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketStatus", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "77ccd045-9b0b-4f06-a0ae-28ccb3b6b586");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "be9f3896-fd0b-4bd5-9194-d3effaeeecdc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "3678b6f6-407b-42b3-affb-ce56ad269158");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "dfc65b2d-e860-419d-a110-9a96e3689e86");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "cf0cf96f-6e51-4087-80fa-ec5da02e6b73");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_StatusId",
                table: "Baskets",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_BasketStatus_StatusId",
                table: "Baskets",
                column: "StatusId",
                principalTable: "BasketStatus",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_BasketStatus_StatusId",
                table: "Baskets");

            migrationBuilder.DropTable(
                name: "BasketStatus");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_StatusId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Baskets");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Baskets",
                newName: "InvoiceId");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Baskets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "33695e10-215c-4c9b-aa11-2e0ca9ab310a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "c6e71f88-48aa-4f68-9d91-565efe468a3c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "585b51d9-cbf2-4eee-b5da-b66be845d554");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "af226dcb-7dfa-45ab-b101-ee041853f7b2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "e13c2e82-6aa1-4352-a1f4-36675b5ea85e");
        }
    }
}
