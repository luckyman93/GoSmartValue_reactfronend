using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class IntroducedPropertyFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ComparableResultReferenceNumber",
                table: "PropertyFeature",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "a2e13994-4bbc-4c69-9959-8033680db739");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "d39db8cf-6ece-46c7-8732-be9e622da874");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "56872dae-58c1-4823-8fd1-db0cadf48338");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "f312138f-2fc6-4be9-9789-ace9ee054176");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "f95a0169-2600-414c-aca4-541aef270450");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeature_ComparableResultReferenceNumber",
                table: "PropertyFeature",
                column: "ComparableResultReferenceNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyFeature_ComparableResults_ComparableResultReferenceN~",
                table: "PropertyFeature",
                column: "ComparableResultReferenceNumber",
                principalTable: "ComparableResults",
                principalColumn: "ReferenceNumber",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyFeature_ComparableResults_ComparableResultReferenceN~",
                table: "PropertyFeature");

            migrationBuilder.DropIndex(
                name: "IX_PropertyFeature_ComparableResultReferenceNumber",
                table: "PropertyFeature");

            migrationBuilder.DropColumn(
                name: "ComparableResultReferenceNumber",
                table: "PropertyFeature");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "f3340b61-6be8-4eb0-bb8e-13f737da2cb5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "0271a671-8731-42ae-b0fd-b4db32304de8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "c4fa6a14-56f5-4da6-a8f1-92ee859002d2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "c901cb22-9325-479d-886c-5f40d628fa7f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "c305fa4b-dd6e-4efe-b27f-5bb5983185be");
        }
    }
}
