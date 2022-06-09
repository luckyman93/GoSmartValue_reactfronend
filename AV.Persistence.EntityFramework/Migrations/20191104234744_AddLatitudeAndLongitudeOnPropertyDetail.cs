using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddLatitudeAndLongitudeOnPropertyDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "34209f83-15cf-48e3-8b09-26cf2f3918e1");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "4f0cd93c-3fcf-404e-93fe-d72c4aa75206");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "541d9eea-c929-4839-9a30-8ad25a27c9ba");

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "PropertyDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "PropertyDetails",
                nullable: true);

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3c92054c-860a-4312-9cfb-a8a50b04997b", "fbb88ed0-31c0-48fb-9087-6de1911b9394", "Admin", null });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2972bcc5-bdf2-4b53-85b9-1653974fe05b", "dbda7cd3-cc90-4120-a1e7-1fb2c31ed393", "Client", null });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d08a4b84-ca8d-4ada-932b-8a44ac978f8d", "27356dd0-77e4-4a7b-b6a8-f3fe78a97766", "Analyst", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "2972bcc5-bdf2-4b53-85b9-1653974fe05b");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "3c92054c-860a-4312-9cfb-a8a50b04997b");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "d08a4b84-ca8d-4ada-932b-8a44ac978f8d");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "PropertyDetails");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "PropertyDetails");

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "541d9eea-c929-4839-9a30-8ad25a27c9ba", "cd8b0b94-f525-421c-9b6f-5c159606159c", "Admin", null });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4f0cd93c-3fcf-404e-93fe-d72c4aa75206", "f6f10e81-5e4b-4536-824a-0d6cad2f48d2", "Client", null });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "34209f83-15cf-48e3-8b09-26cf2f3918e1", "a8f4e535-3c32-4b44-a87c-9f0346ce2f87", "Analyst", null });
        }
    }
}
