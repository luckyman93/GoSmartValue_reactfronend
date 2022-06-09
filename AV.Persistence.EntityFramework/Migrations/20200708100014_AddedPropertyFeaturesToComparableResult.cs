using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedPropertyFeaturesToComparableResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BoundaryWall",
                table: "ComparableResults",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ElectricFence",
                table: "ComparableResults",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FirePlace",
                table: "ComparableResults",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MotorizedGate",
                table: "ComparableResults",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OtherSpecialFeatures",
                table: "ComparableResults",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OutdoorEntertainmentArea",
                table: "ComparableResults",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Paved",
                table: "ComparableResults",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SwimmingPool",
                table: "ComparableResults",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "2af619f5-00c6-48ea-bbe1-62caadd1856c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "d8f2c51b-feef-4ddf-b32c-9e416d1790cb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "fc9909a3-6cd2-4b24-a3b8-481fcf11cbf6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "c231944d-3147-407b-9500-95cd9640e24e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "c596902e-376d-48a4-ae29-3e57195837ea");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoundaryWall",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "ElectricFence",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "FirePlace",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "MotorizedGate",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "OtherSpecialFeatures",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "OutdoorEntertainmentArea",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "Paved",
                table: "ComparableResults");

            migrationBuilder.DropColumn(
                name: "SwimmingPool",
                table: "ComparableResults");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "d11b47b3-eb06-4818-a24d-caccab06c8ff");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "7b601ea5-dc88-4ba6-9b29-83107111310b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "a3ca8ce2-9e97-4456-9bd1-7e7d993e773d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "38c5c3b2-b905-4437-a3a9-154cf627a67c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "528f2ad5-2b4d-49bf-9f08-3ef44e4ac0b2");
        }
    }
}
