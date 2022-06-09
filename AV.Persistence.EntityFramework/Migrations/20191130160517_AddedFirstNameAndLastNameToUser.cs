using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedFirstNameAndLastNameToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("252751e2-9094-4ab0-a6c3-f2801e7fe440"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4f085629-690e-45a9-883b-534a746a32e9"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e3d2f20-ed75-41c6-843a-b39b6beb7924"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e0ed1a49-d2f2-4a35-8e73-dca873f4d24b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fd82a9f6-5967-452c-b327-e07b1e3a030d"));

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("400eccc8-7712-4bc0-84d9-838fbe201142"), "3c3b59b1-a166-4a0a-955c-054a543e9cdc", "admin", "ADMIN" },
                    { new Guid("466fd40a-558c-4f8b-8b38-2040e7e90a27"), "be436842-790f-43ff-a9e0-9d5c23065130", "client", "CLIENT" },
                    { new Guid("8170fd8c-e5c0-43cd-9bea-c61750d92209"), "33efc487-0037-4782-acba-cfeb03509d55", "analyst", "ANALYST" },
                    { new Guid("bf791d76-d920-4998-959a-abacac69ecce"), "8ee8a017-325a-482a-863c-a3104777b895", "manager", "MANAGER" },
                    { new Guid("690de82a-ccb7-4b76-8cfc-f9f32e959876"), "3e4a3293-e1b3-4877-a2f4-04d1e30337ee", "user", "USER" }
                });
        }
    }
}
