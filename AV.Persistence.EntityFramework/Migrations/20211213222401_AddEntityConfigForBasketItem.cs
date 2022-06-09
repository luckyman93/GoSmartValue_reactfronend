using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddEntityConfigForBasketItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItemInputData_BasketItems_BasketItemId",
                table: "BasketItemInputData");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_BasketItemInputData_InputDataId",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_InputDataId",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItemInputData_BasketItemId",
                table: "BasketItemInputData");

            migrationBuilder.AlterColumn<int>(
                name: "BasketItemId",
                table: "BasketItemInputData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "ab9aa287-3f4a-465c-a6fc-992dc0c5de08");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "3b068dc4-ef75-475f-ad74-77ed01729138");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "bd09d402-5dba-401e-a937-2f483db5300e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "3898c7f5-b1ed-452c-b50e-19e1b9139e96");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "99273a71-7c25-4995-a11c-32a4e8cf89a5");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItemInputData_BasketItemId",
                table: "BasketItemInputData",
                column: "BasketItemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItemInputData_BasketItems_BasketItemId",
                table: "BasketItemInputData",
                column: "BasketItemId",
                principalTable: "BasketItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItemInputData_BasketItems_BasketItemId",
                table: "BasketItemInputData");

            migrationBuilder.DropIndex(
                name: "IX_BasketItemInputData_BasketItemId",
                table: "BasketItemInputData");

            migrationBuilder.AlterColumn<int>(
                name: "BasketItemId",
                table: "BasketItemInputData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "bfdde45a-274b-4cfe-9e96-c1eb27d74ac9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "85acb4de-f4b6-4057-b8b7-865839401c1f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "fc447312-a45c-490f-9f51-f8684b788de5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "a8f5a7a1-daa9-4b8a-a4fb-f0fa99fe777d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "4db50787-b607-4d8f-a9d5-0100201e94d2");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_InputDataId",
                table: "BasketItems",
                column: "InputDataId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItemInputData_BasketItemId",
                table: "BasketItemInputData",
                column: "BasketItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItemInputData_BasketItems_BasketItemId",
                table: "BasketItemInputData",
                column: "BasketItemId",
                principalTable: "BasketItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_BasketItemInputData_InputDataId",
                table: "BasketItems",
                column: "InputDataId",
                principalTable: "BasketItemInputData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
