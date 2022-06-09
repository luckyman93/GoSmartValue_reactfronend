using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class MadeDateNullableOnPropertyDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "PropertyDetails",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "94df12bf-df8e-4441-b8ea-f56eed6f61fc", "e5a31850-5893-44e9-9602-941843275699", "Admin", null });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3c4a9937-90ad-498e-8145-998dab69954f", "944e37eb-3b1a-42bf-adea-8a1e5a8a1c7b", "Client", null });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cdadf338-cb7f-43c6-ab01-b5b70d4fffc7", "57194498-2777-40d6-b56c-9fe1bfafbb31", "Analyst", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "3c4a9937-90ad-498e-8145-998dab69954f");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "94df12bf-df8e-4441-b8ea-f56eed6f61fc");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "cdadf338-cb7f-43c6-ab01-b5b70d4fffc7");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "PropertyDetails",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

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
    }
}
