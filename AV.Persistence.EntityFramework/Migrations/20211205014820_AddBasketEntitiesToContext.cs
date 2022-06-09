using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddBasketEntitiesToContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<Guid>(type: "char(36)", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "char(36)", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: true),
                    OrderNo = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PromoCode = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    BuyerId = table.Column<Guid>(type: "char(36)", nullable: false),
                    NetTotal = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    DiscountTotal = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    GrossTotal = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BasketItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<Guid>(type: "char(36)", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "char(36)", nullable: false),
                    BasketId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    PictureUrl = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PromoCode = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    InputDataId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItems_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasketItemInputData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BasketItemId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    LocalityId = table.Column<int>(type: "int", nullable: true),
                    StreetId = table.Column<int>(type: "int", nullable: true),
                    StreetName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PlotNo = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PlotSize = table.Column<int>(type: "int", nullable: false),
                    PlotSizeMetric = table.Column<int>(type: "int", nullable: false),
                    Zoning = table.Column<int>(type: "int", nullable: false),
                    DevelopmentPhase = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItemInputData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItemInputData_BasketItems_BasketItemId",
                        column: x => x.BasketItemId,
                        principalTable: "BasketItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItemInputData_Localities_LocalityId",
                        column: x => x.LocalityId,
                        principalTable: "Localities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BasketItemInputData_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItemInputData_Streets_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Streets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "3750544d-6e36-43d4-9fd5-f47008be5fa2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "c6194390-c6d8-488b-8f4e-c0f946b024c5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "4d018412-94e3-4b91-a82f-1dbf9906dd4c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "ef537880-1394-41ef-81af-e31bd26915db");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "e896043c-4538-4d84-bcb0-12b064f70dbd");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItemInputData_BasketItemId",
                table: "BasketItemInputData",
                column: "BasketItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItemInputData_LocalityId",
                table: "BasketItemInputData",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItemInputData_LocationId",
                table: "BasketItemInputData",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItemInputData_StreetId",
                table: "BasketItemInputData",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketId",
                table: "BasketItems",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_InputDataId",
                table: "BasketItems",
                column: "InputDataId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_ProductId",
                table: "BasketItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_BasketItemInputData_InputDataId",
                table: "BasketItems",
                column: "InputDataId",
                principalTable: "BasketItemInputData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItemInputData_BasketItems_BasketItemId",
                table: "BasketItemInputData");

            migrationBuilder.DropTable(
                name: "BasketItems");

            migrationBuilder.DropTable(
                name: "BasketItemInputData");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "93b0bcc8-c018-4980-9939-0156c6229e37");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "4e3aa06b-f817-4cb6-aac3-57960bbef6f8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "04a83279-0158-4bb2-8abb-97a0b20f9ea7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "b1a73f67-55f8-443f-bcdf-d3b6358fd3c3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "27722bb1-6a2f-4a84-bf8b-3ce2f3732d28");
        }
    }
}
