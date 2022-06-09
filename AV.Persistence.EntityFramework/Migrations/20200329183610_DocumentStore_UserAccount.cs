using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class DocumentStore_UserAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Organisations_OrganisationId",
                table: "Account");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Document",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentStreamId",
                table: "Document",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Document",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganisationId",
                table: "Account",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateTable(
                name: "DocumentStream",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MimeType = table.Column<string>(nullable: true),
                    FileStream = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentStream", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "b83d4709-f027-419f-ba6b-e1070fa2dd79");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "6806a11d-50b3-4a8d-9f11-9f9e643194a6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "c253292e-546c-4f08-8933-2c3fec9c75f5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "2dad996d-2a3b-4895-8bc3-3d843318fcc8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "db730c53-6377-4cc4-b383-c7c4ab6fc94e");

            migrationBuilder.CreateIndex(
                name: "IX_Document_DocumentStreamId",
                table: "Document",
                column: "DocumentStreamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Organisations_OrganisationId",
                table: "Account",
                column: "OrganisationId",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_DocumentStream_DocumentStreamId",
                table: "Document",
                column: "DocumentStreamId",
                principalTable: "DocumentStream",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Organisations_OrganisationId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_DocumentStream_DocumentStreamId",
                table: "Document");

            migrationBuilder.DropTable(
                name: "DocumentStream");

            migrationBuilder.DropIndex(
                name: "IX_Document_DocumentStreamId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "DocumentStreamId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Document");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganisationId",
                table: "Account",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "6f93af2f-9b82-4fc6-88fa-87c401e3ce39");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "4c3d665a-c71e-40cb-8007-23268e752b53");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "3f05dea4-3183-4559-a81f-c0e81fecaee0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "2a488d83-81f5-48c1-9c2c-fac5aa820185");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "eb6d28ce-b824-4faf-b6b0-d201ba84659a");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Organisations_OrganisationId",
                table: "Account",
                column: "OrganisationId",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
