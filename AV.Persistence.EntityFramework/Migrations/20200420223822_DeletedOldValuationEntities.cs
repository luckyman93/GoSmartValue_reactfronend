using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class DeletedOldValuationEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plots_Shapes_polygonShapeId",
                table: "Plots");

            migrationBuilder.DropTable(
                name: "RealEstateProfessionalCategory");

            migrationBuilder.DropTable(
                name: "ValuationRequests");

            migrationBuilder.DropTable(
                name: "ValuationRequestsDelete");

            migrationBuilder.DropTable(
                name: "Professionals");

            migrationBuilder.DropTable(
                name: "Valuations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shapes",
                table: "Shapes");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "Shapes",
                newName: "PlotShape");

            migrationBuilder.AlterColumn<double>(
                name: "EstimatedValue",
                table: "ComparableResults",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlotShape",
                table: "PlotShape",
                column: "ShapeId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "425e9815-02cd-41ce-b658-d79ffdd405fd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "39385642-504e-490e-89e2-f8bedb8e7adb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "b5ddb1bc-3324-4011-a827-bf5887d373b1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "8b9d8973-af3c-4f00-9631-04cc8d77dca5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "2928f4c7-924c-48ff-9f0b-357c65780bea");

            migrationBuilder.AddForeignKey(
                name: "FK_Plots_PlotShape_polygonShapeId",
                table: "Plots",
                column: "polygonShapeId",
                principalTable: "PlotShape",
                principalColumn: "ShapeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plots_PlotShape_polygonShapeId",
                table: "Plots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlotShape",
                table: "PlotShape");

            migrationBuilder.RenameTable(
                name: "PlotShape",
                newName: "Shapes");

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatedValue",
                table: "ComparableResults",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shapes",
                table: "Shapes",
                column: "ShapeId");

            migrationBuilder.CreateTable(
                name: "Professionals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    OrganisationId = table.Column<Guid>(nullable: false),
                    ProfessionalId = table.Column<Guid>(nullable: false),
                    REACNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professionals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Professionals_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Professionals_AspNetUsers_ProfessionalId",
                        column: x => x.ProfessionalId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValuationRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ComparableRequestId = table.Column<Guid>(nullable: false),
                    ComparableResultId = table.Column<Guid>(nullable: false),
                    RequestedOn = table.Column<DateTimeOffset>(nullable: false),
                    RequesterId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValuationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValuationRequests_ComparableResults_ComparableResultId",
                        column: x => x.ComparableResultId,
                        principalTable: "ComparableResults",
                        principalColumn: "ReferenceNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Valuations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DifferentiatorNotes = table.Column<string>(nullable: true),
                    Host = table.Column<string>(nullable: true),
                    LandUse = table.Column<string>(nullable: true),
                    LocalityName = table.Column<string>(nullable: true),
                    LocationId = table.Column<int>(nullable: false),
                    PlotNo = table.Column<string>(nullable: true),
                    PropertyType = table.Column<string>(nullable: true),
                    RequestDate = table.Column<DateTimeOffset>(nullable: true),
                    RequestProperties = table.Column<string>(nullable: true),
                    RequestUri = table.Column<string>(nullable: true),
                    Size = table.Column<decimal>(nullable: false),
                    SizeUnitsTypeID = table.Column<int>(nullable: false),
                    Source = table.Column<string>(nullable: true),
                    ValuationAmount = table.Column<int>(nullable: false),
                    ValuationDate = table.Column<DateTime>(nullable: false),
                    ValuationType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valuations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Valuations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RealEstateProfessionalCategory",
                columns: table => new
                {
                    CategoryName = table.Column<string>(nullable: false),
                    RealEstateProfessionalId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstateProfessionalCategory", x => x.CategoryName);
                    table.ForeignKey(
                        name: "FK_RealEstateProfessionalCategory_Professionals_RealEstateProfe~",
                        column: x => x.RealEstateProfessionalId,
                        principalTable: "Professionals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ValuationRequestsDelete",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientUserId = table.Column<Guid>(nullable: true),
                    DateOfSale = table.Column<DateTime>(nullable: false),
                    Host = table.Column<string>(nullable: true),
                    LandUse = table.Column<int>(nullable: false),
                    LocalityId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    PlotNo = table.Column<string>(nullable: true),
                    PlotSize = table.Column<int>(nullable: false),
                    PropertySaleId = table.Column<Guid>(nullable: true),
                    PropertyType = table.Column<string>(nullable: true),
                    PurchasePrice = table.Column<int>(nullable: false),
                    RequestDate = table.Column<DateTime>(nullable: false),
                    RequesterUserId = table.Column<Guid>(nullable: true),
                    StreetName = table.Column<string>(nullable: true),
                    ValuationId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValuationRequestsDelete", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValuationRequestsDelete_AspNetUsers_ClientUserId",
                        column: x => x.ClientUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ValuationRequestsDelete_Localities_LocalityId",
                        column: x => x.LocalityId,
                        principalTable: "Localities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ValuationRequestsDelete_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ValuationRequestsDelete_PropertyDetails_PropertySaleId",
                        column: x => x.PropertySaleId,
                        principalTable: "PropertyDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ValuationRequestsDelete_AspNetUsers_RequesterUserId",
                        column: x => x.RequesterUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ValuationRequestsDelete_Valuations_ValuationId",
                        column: x => x.ValuationId,
                        principalTable: "Valuations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08d7809a-443e-6a79-090d-0de147013b55"),
                column: "ConcurrencyStamp",
                value: "9fba8c53-0b0b-4cae-9ba5-6401cbc0b85c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15733086-823e-452e-be44-e49a975f3964"),
                column: "ConcurrencyStamp",
                value: "16ccbf2b-9819-4c82-a1ad-6eb0a0b1c134");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4eeebad8-3100-4103-8757-5e60780fb160"),
                column: "ConcurrencyStamp",
                value: "2b9e4388-211c-4493-a216-a7827e80d775");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"),
                column: "ConcurrencyStamp",
                value: "63b637e4-2e5b-42f6-8a68-627d6610fdd2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"),
                column: "ConcurrencyStamp",
                value: "f42d1e4c-d3fa-43e6-8a19-d2edef11a069");

            migrationBuilder.CreateIndex(
                name: "IX_Professionals_OrganisationId",
                table: "Professionals",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Professionals_ProfessionalId",
                table: "Professionals",
                column: "ProfessionalId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateProfessionalCategory_RealEstateProfessionalId",
                table: "RealEstateProfessionalCategory",
                column: "RealEstateProfessionalId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequests_ComparableResultId",
                table: "ValuationRequests",
                column: "ComparableResultId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequestsDelete_ClientUserId",
                table: "ValuationRequestsDelete",
                column: "ClientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequestsDelete_LocalityId",
                table: "ValuationRequestsDelete",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequestsDelete_LocationId",
                table: "ValuationRequestsDelete",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequestsDelete_PropertySaleId",
                table: "ValuationRequestsDelete",
                column: "PropertySaleId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequestsDelete_RequesterUserId",
                table: "ValuationRequestsDelete",
                column: "RequesterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequestsDelete_ValuationId",
                table: "ValuationRequestsDelete",
                column: "ValuationId");

            migrationBuilder.CreateIndex(
                name: "IX_Valuations_LocationId",
                table: "Valuations",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plots_Shapes_polygonShapeId",
                table: "Plots",
                column: "polygonShapeId",
                principalTable: "Shapes",
                principalColumn: "ShapeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
