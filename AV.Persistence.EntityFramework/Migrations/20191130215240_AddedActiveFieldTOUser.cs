using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedActiveFieldTOUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("c6719053-6f69-4f07-8f1c-1e48274c241d"), "c5b2bad0-bf7a-431a-ae8a-72e23eba7ef9", "admin", "ADMIN" },
                    { new Guid("112b90a5-f498-468e-8c69-d0872990c124"), "3bdd011a-3da7-43b9-82bd-41e0c7b1f483", "client", "CLIENT" },
                    { new Guid("4f4d4102-e3ba-4ae7-96d8-7ff4a783abdd"), "e8c280f6-ca7f-499e-bdab-7b82b707bbef", "analyst", "ANALYST" },
                    { new Guid("2187d0cb-0ac9-45e5-acd4-89dc6968bb71"), "2d07a4d9-984d-4eb7-bcfb-7edfdf6c54e6", "manager", "MANAGER" },
                    { new Guid("a15c3499-2e2e-4193-98a0-cce0867d1348"), "1c66bfdc-078a-4eab-9223-912e279230ab", "user", "USER" },
                    { new Guid("4a67c795-2e9d-4cd1-93fd-41e357eb772e"), "2a1136b7-7692-43f5-a14e-acae98784507", "corporate", "CORPORATE" },
                    { new Guid("2ba2717f-cef1-4c34-8c7a-66ce8522df65"), "5d9b9420-c9b8-47da-b4ff-191164f79ecf", "standard", "STANDARD" },
                    { new Guid("532d937a-c51d-447e-9478-49aa5ba55835"), "49967cf4-02ea-4aed-8751-3705cc3b6e47", "valuer", "VALUER" },
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("112b90a5-f498-468e-8c69-d0872990c124"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2187d0cb-0ac9-45e5-acd4-89dc6968bb71"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4f4d4102-e3ba-4ae7-96d8-7ff4a783abdd"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a15c3499-2e2e-4193-98a0-cce0867d1348"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c6719053-6f69-4f07-8f1c-1e48274c241d"));

            migrationBuilder.DropColumn(
                name: "Active",
                table: "AspNetUsers");
        }
    }
}
