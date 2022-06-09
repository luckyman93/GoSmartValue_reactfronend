using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AssociatedUsersToOrganisation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryUserId",
                table: "Organisations",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_PrimaryUserId",
                table: "Organisations",
                column: "PrimaryUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organisations_AspNetUsers_PrimaryUserId",
                table: "Organisations",
                column: "PrimaryUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organisations_AspNetUsers_PrimaryUserId",
                table: "Organisations");

            migrationBuilder.DropIndex(
                name: "IX_Organisations_PrimaryUserId",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "PrimaryUserId",
                table: "Organisations");
        }
    }
}
