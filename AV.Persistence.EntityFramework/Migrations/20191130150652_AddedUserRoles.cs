using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "74657ef5-00df-49a7-8d70-330a09d2860b", "6eee1128-d12c-4c4a-9f7f-4336c2451238", "Admin", null },
                    { "7f484253-d833-4eb8-86fd-0207a8b81196", "71760e52-36d4-4499-89ad-6023e4dd1494", "Client", null },
                    { "953b385c-40b6-4ec6-8cf8-95ac6bab3339", "d9a670c3-74d8-42dc-b3f4-58919f42ca75", "Analyst", null },
                    { "c847524f-b4ed-4ddb-9271-262b032133b5", "5514cfa6-d5e8-4c10-a5dd-2573acabf4bb", "Manager", null },
                    { "79726180-ee84-43ac-880e-3ca0957aebdd", "6355779c-fde5-462d-b011-3cf1e3942d23", "User", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "74657ef5-00df-49a7-8d70-330a09d2860b");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "79726180-ee84-43ac-880e-3ca0957aebdd");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "7f484253-d833-4eb8-86fd-0207a8b81196");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "953b385c-40b6-4ec6-8cf8-95ac6bab3339");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "c847524f-b4ed-4ddb-9271-262b032133b5");

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
    }
}
