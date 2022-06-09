using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class RemovedDuplicateIdentityRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("400eccc8-7712-4bc0-84d9-838fbe201142"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("466fd40a-558c-4f8b-8b38-2040e7e90a27"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("690de82a-ccb7-4b76-8cfc-f9f32e959876"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8170fd8c-e5c0-43cd-9bea-c61750d92209"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bf791d76-d920-4998-959a-abacac69ecce"));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9b21cd20-3ac7-4a80-abc5-740e9819d640", "d614e2bb-4af3-4fa9-a8a8-4c4df81bfb35", "Admin", null },
                    { "a1c84f5d-36aa-4ced-9703-480aa49126a8", "bff6a29b-1e32-44b7-bd89-c0e5f6962859", "Client", null },
                    { "2b50c23e-6443-4b2f-869f-114c85744a11", "b7da1598-25fb-4e45-a2c9-08c4549972d2", "Analyst", null },
                    { "52cb2d84-77b2-40ad-b81c-d1c1b1d3adf3", "8f8d3c65-0b7a-4427-bd3e-a0188185978d", "Manager", null },
                    { "7e577ae7-2045-4168-9526-4407b958816e", "1e6aa383-13ab-45f0-a1f2-b8a31e4f8bc0", "User", null }
                });
        }
    }
}
