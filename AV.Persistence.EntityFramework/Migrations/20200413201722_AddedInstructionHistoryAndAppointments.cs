using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedInstructionHistoryAndAppointments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "REIBNumber",
                table: "AspNetUsers",
                newName: "ReibNumber");

            migrationBuilder.RenameColumn(
                name: "REACNumber",
                table: "AspNetUsers",
                newName: "ReacNumber");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentStatus",
                table: "Instructions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ValuerAccepted",
                table: "Instructions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "InstructionAppointment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    InstructionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructionAppointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstructionAppointment_Instructions_InstructionId",
                        column: x => x.InstructionId,
                        principalTable: "Instructions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstructionHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    InstructionId = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Summary = table.Column<string>(nullable: true),
                    GeneratedBy = table.Column<Guid>(nullable: true),
                    Internal = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructionHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstructionHistory_Instructions_InstructionId",
                        column: x => x.InstructionId,
                        principalTable: "Instructions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "7e768970-ffb3-4078-965a-9ce4c67337f7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "cab8d0be-882b-430c-8deb-7f53ca2291c9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "df156391-69aa-4a7d-8137-9911cb2c2715");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "fba13d3f-b841-42f5-a5b5-aa45340f5589");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "b26974f4-a48f-46a3-b42d-e2251c3e5584");

            migrationBuilder.CreateIndex(
                name: "IX_InstructionAppointment_InstructionId",
                table: "InstructionAppointment",
                column: "InstructionId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructionHistory_InstructionId",
                table: "InstructionHistory",
                column: "InstructionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstructionAppointment");

            migrationBuilder.DropTable(
                name: "InstructionHistory");

            migrationBuilder.DropColumn(
                name: "AppointmentStatus",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "ValuerAccepted",
                table: "Instructions");

            migrationBuilder.RenameColumn(
                name: "ReibNumber",
                table: "AspNetUsers",
                newName: "REIBNumber");

            migrationBuilder.RenameColumn(
                name: "ReacNumber",
                table: "AspNetUsers",
                newName: "REACNumber");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "60a9d0ac-5aa7-42f2-b0fc-b1987580cc86");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "aa11c1ee-0182-495f-bf62-fa461ec109eb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "d7923622-b192-41d6-b6ca-bfb8a7ccfdc9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "e102e559-50a4-4ec0-9133-9481431f7068");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "70cce743-0dd7-439a-9bd1-16a8ab4aeac7");
        }
    }
}
