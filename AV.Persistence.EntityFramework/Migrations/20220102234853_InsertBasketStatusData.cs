using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class InsertBasketStatusData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Baskets");
            migrationBuilder.Sql("Delete from BasketStatus");
            migrationBuilder.InsertData(
                table: "BasketStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    {1,"Draft"},
                    {2,"Abandoned"},
                    {3,"Confirmed"},
                    {4,"Paid"},
                    {5,"Completed"},
                    {6,"Cancelled"},
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "a0a61096-6254-484a-be70-c56cf588740f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "ff5ab339-a030-41e5-8d7c-dfc0eecea633");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "01e53f2e-4c3b-411c-936b-cd87bae43681");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "2a82d92e-1f1a-47c0-a93f-935dd46e3695");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "23ba479c-c391-44ff-8420-d04b9117056d");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
