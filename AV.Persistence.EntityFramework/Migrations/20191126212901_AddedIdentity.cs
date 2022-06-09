using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRole");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "1d9038ed-a5ee-4155-88f0-76d573430ee1");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "78d0b496-5e64-4272-b671-b2137b44bf35");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "b49ed2c4-33da-4cb4-8ee0-ead73dc69948");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "f17bb6f7-265a-4e02-8444-622c5de8f078");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "f3efc205-0c59-45b8-9eca-c700b021f152");

            migrationBuilder.RenameTable(
                name: "UserRole",
                newName: "IdentityRole");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityRole",
                table: "IdentityRole",
                column: "Id");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c9767385-1654-4180-bd56-548af3cd0b54", "13aaa12a-a2a3-43f4-b472-a7feddf16ed4", "Admin", null },
                    { "0a514204-b054-4a25-ac5a-c46f37652d99", "a081429b-a6e2-4a37-a9e6-863b3743be23", "Client", null },
                    { "8d374a45-7ee1-4049-8c58-3207ec8b2dc7", "47f7a944-fec3-422a-a569-047bd2993ddf", "Analyst", null },
                    { "5219049f-451c-4fed-8d6e-96a4cc7737a9", "f7de1fed-cb94-49a8-8e52-832bfc86ab32", "Manager", null },
                    { "fcb02901-9b15-42bf-ac84-c39a99c1682a", "7cb7e4fd-7bf3-4069-b81e-5cab42fa35b6", "User", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityRole",
                table: "IdentityRole");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "0a514204-b054-4a25-ac5a-c46f37652d99");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "5219049f-451c-4fed-8d6e-96a4cc7737a9");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "8d374a45-7ee1-4049-8c58-3207ec8b2dc7");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "c9767385-1654-4180-bd56-548af3cd0b54");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "fcb02901-9b15-42bf-ac84-c39a99c1682a");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "IdentityRole",
                newName: "UserRole");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRole",
                column: "Id");

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "78d0b496-5e64-4272-b671-b2137b44bf35", "73baac4e-85c0-4566-bf4a-70110970a12a", "Admin", null },
                    { "f3efc205-0c59-45b8-9eca-c700b021f152", "2e21931a-1b9a-43f0-a143-588970a40ceb", "Client", null },
                    { "b49ed2c4-33da-4cb4-8ee0-ead73dc69948", "99e946cb-0927-4881-b7d1-493f4d717d1d", "Analyst", null },
                    { "f17bb6f7-265a-4e02-8444-622c5de8f078", "ecb32214-fe12-4f47-9db1-6b3becb4ffe7", "Manager", null },
                    { "1d9038ed-a5ee-4155-88f0-76d573430ee1", "09923b3f-e084-49cc-9cf4-8a8f1e1c4876", "User", null }
                });
        }
    }
}
