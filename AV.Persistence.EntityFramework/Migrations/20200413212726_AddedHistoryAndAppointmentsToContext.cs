using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedHistoryAndAppointmentsToContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstructionAppointment_Instructions_InstructionId",
                table: "InstructionAppointment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstructionAppointment",
                table: "InstructionAppointment");

            migrationBuilder.RenameTable(
                name: "InstructionAppointment",
                newName: "InstructionAppointments");

            migrationBuilder.RenameIndex(
                name: "IX_InstructionAppointment_InstructionId",
                table: "InstructionAppointments",
                newName: "IX_InstructionAppointments_InstructionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstructionAppointments",
                table: "InstructionAppointments",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "818b416c-ad17-4366-9de2-b167465bb485");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "ca773249-850c-4cea-b39a-3d49f3ffc3e1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "6cdab099-93a1-468e-a221-e6c01602e235");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "f70634d0-1e98-4316-b885-d7b6412d23ae");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "20c6fbd4-e0cf-40fa-a067-9a972dfdbab2");

            migrationBuilder.AddForeignKey(
                name: "FK_InstructionAppointments_Instructions_InstructionId",
                table: "InstructionAppointments",
                column: "InstructionId",
                principalTable: "Instructions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstructionAppointments_Instructions_InstructionId",
                table: "InstructionAppointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstructionAppointments",
                table: "InstructionAppointments");

            migrationBuilder.RenameTable(
                name: "InstructionAppointments",
                newName: "InstructionAppointment");

            migrationBuilder.RenameIndex(
                name: "IX_InstructionAppointments_InstructionId",
                table: "InstructionAppointment",
                newName: "IX_InstructionAppointment_InstructionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstructionAppointment",
                table: "InstructionAppointment",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "1bd510cb-f20f-4437-b5b4-013115be0b0e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "f13661a7-d391-4913-b07b-c42f095107b0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "04f135cd-ff9a-4be7-8f78-8361940993f3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "f996d348-22ba-4a61-bc5c-c0ff7159f86a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "06da82e8-a156-4ffd-9d34-08f12a19a483");

            migrationBuilder.AddForeignKey(
                name: "FK_InstructionAppointment_Instructions_InstructionId",
                table: "InstructionAppointment",
                column: "InstructionId",
                principalTable: "Instructions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
