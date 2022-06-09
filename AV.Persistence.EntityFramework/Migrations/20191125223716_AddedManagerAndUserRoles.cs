using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedManagerAndUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "00117d33-f209-4763-8dce-7dcf0549dd51");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "210db5af-a265-468e-8707-f6f735539865");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "c879d5b3-4ccb-4e03-8afd-a055344ecc7f");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c879d5b3-4ccb-4e03-8afd-a055344ecc7f", "d309a96b-7d6b-4bc1-b81a-b2b49c6cb521", "Admin", null });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "210db5af-a265-468e-8707-f6f735539865", "02f0d260-f8c0-453c-968d-8e1951cc993a", "Client", null });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "00117d33-f209-4763-8dce-7dcf0549dd51", "4a12173d-b440-4f6d-9af9-38aa9130030b", "Analyst", null });
        }
    }
}
