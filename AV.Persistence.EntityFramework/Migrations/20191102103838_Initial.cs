using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComparableBandSizes",
                columns: table => new
                {
                    BandName = table.Column<string>(nullable: false),
                    LowerBandLimit = table.Column<int>(nullable: false),
                    UpperBandLimit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComparableBandSizes", x => x.BandName);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Population = table.Column<int>(nullable: false),
                    Area = table.Column<double>(nullable: false),
                    Density = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shapes",
                columns: table => new
                {
                    ShapeId = table.Column<double>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    Width = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shapes", x => x.ShapeId);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    DistrictId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Localities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LocationId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Localities_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Valuations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LocationId = table.Column<int>(nullable: false),
                    PlotNo = table.Column<string>(nullable: true),
                    LandUse = table.Column<string>(nullable: true),
                    PropertyType = table.Column<string>(nullable: true),
                    ValuationType = table.Column<string>(nullable: true),
                    SizeUnitsTypeID = table.Column<int>(nullable: false),
                    Size = table.Column<decimal>(nullable: false),
                    ValuationDate = table.Column<DateTime>(nullable: false),
                    ValuationAmount = table.Column<int>(nullable: false),
                    DifferentiatorNotes = table.Column<string>(nullable: true),
                    RequestProperties = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Host = table.Column<string>(nullable: true),
                    LocalityName = table.Column<string>(nullable: true),
                    RequestUri = table.Column<string>(nullable: true),
                    RequestDate = table.Column<DateTimeOffset>(nullable: true)
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
                name: "Streets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StreetName = table.Column<string>(nullable: true),
                    LocalityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Streets_Localities_LocalityId",
                        column: x => x.LocalityId,
                        principalTable: "Localities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plots",
                columns: table => new
                {
                    PlotId = table.Column<Guid>(nullable: false),
                    PlotNo = table.Column<string>(nullable: true),
                    StreetId = table.Column<int>(nullable: false),
                    PolygonId = table.Column<int>(nullable: false),
                    polygonShapeId = table.Column<double>(nullable: true),
                    Size = table.Column<int>(nullable: false),
                    LandUse = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plots", x => x.PlotId);
                    table.ForeignKey(
                        name: "FK_Plots_Streets_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Streets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plots_Shapes_polygonShapeId",
                        column: x => x.polygonShapeId,
                        principalTable: "Shapes",
                        principalColumn: "ShapeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AddedOn = table.Column<DateTime>(nullable: false),
                    DataState = table.Column<int>(nullable: false),
                    DateOfSale = table.Column<DateTime>(nullable: false),
                    SalePrice = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    LocalityId = table.Column<int>(nullable: false),
                    StreetId = table.Column<int>(nullable: true),
                    StreetName = table.Column<string>(nullable: true),
                    PlotSize = table.Column<int>(nullable: false),
                    PlotId = table.Column<int>(nullable: false),
                    PlotId1 = table.Column<Guid>(nullable: true),
                    LandUse = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    PropertyType = table.Column<int>(nullable: false),
                    BandClassBandName = table.Column<string>(nullable: true),
                    PlotNo = table.Column<string>(nullable: true),
                    PropertyDetailId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyDetails_ComparableBandSizes_BandClassBandName",
                        column: x => x.BandClassBandName,
                        principalTable: "ComparableBandSizes",
                        principalColumn: "BandName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyDetails_Localities_LocalityId",
                        column: x => x.LocalityId,
                        principalTable: "Localities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyDetails_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyDetails_Plots_PlotId1",
                        column: x => x.PlotId1,
                        principalTable: "Plots",
                        principalColumn: "PlotId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyDetails_PropertyDetails_PropertyDetailId",
                        column: x => x.PropertyDetailId,
                        principalTable: "PropertyDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComparableResults",
                columns: table => new
                {
                    ReferenceNumber = table.Column<Guid>(nullable: false),
                    PropertyDetailsId = table.Column<int>(nullable: false),
                    PropertyDetailId = table.Column<Guid>(nullable: true),
                    EstimatedPropertyValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComparableResults", x => x.ReferenceNumber);
                    table.ForeignKey(
                        name: "FK_ComparableResults_PropertyDetails_PropertyDetailId",
                        column: x => x.PropertyDetailId,
                        principalTable: "PropertyDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ValuationRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RequestDate = table.Column<DateTime>(nullable: false),
                    StreetName = table.Column<string>(nullable: true),
                    PlotNo = table.Column<string>(nullable: true),
                    PlotSize = table.Column<int>(nullable: false),
                    LandUse = table.Column<int>(nullable: false),
                    DateOfSale = table.Column<DateTime>(nullable: false),
                    PropertyDetailId = table.Column<Guid>(nullable: true),
                    ValuationId = table.Column<long>(nullable: true),
                    ClientId = table.Column<string>(nullable: true),
                    Host = table.Column<string>(nullable: true),
                    PropertyType = table.Column<string>(nullable: true),
                    LocationId = table.Column<int>(nullable: false),
                    PurchasePrice = table.Column<int>(nullable: false),
                    LocalityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValuationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValuationRequests_Users_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ValuationRequests_Localities_LocalityId",
                        column: x => x.LocalityId,
                        principalTable: "Localities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ValuationRequests_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ValuationRequests_PropertyDetails_PropertyDetailId",
                        column: x => x.PropertyDetailId,
                        principalTable: "PropertyDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ValuationRequests_Valuations_ValuationId",
                        column: x => x.ValuationId,
                        principalTable: "Valuations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ComparableBandSizes",
                columns: new[] { "BandName", "LowerBandLimit", "UpperBandLimit" },
                values: new object[,]
                {
                    { "LowIncome", 460, 600 },
                    { "MiddleIncome", 601, 800 },
                    { "HighIncome", 800, 2147483647 }
                });

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "Id", "Area", "Density", "Name", "Population" },
                values: new object[,]
                {
                    { 1, 28470.0, 6.9465000000000003, "Southern", 197767 },
                    { 2, 1780.0, 47.759999999999998, "South-East", 85014 },
                    { 3, 31100.0, 9.7929999999999993, "Kweneng", 304549 },
                    { 4, 7960.0, 11.515000000000001, "Kgatleng", 91660 },
                    { 5, 142076.0, 4.0546189999999998, "Central", 576064 },
                    { 6, 550.0, 11.77, "North-East", 60264 },
                    { 7, 109130.0, 1.39544, "Ngamiland", 152284 },
                    { 8, 20800.0, 1.5249999999999999, "Chobe", 23347 },
                    { 9, 117910.0, 0.36549100000000001, "Ghanzi", 43095 },
                    { 10, 105200.0, 0.48243000000000003, "Kgalagadi", 50752 }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "541d9eea-c929-4839-9a30-8ad25a27c9ba", "cd8b0b94-f525-421c-9b6f-5c159606159c", "Admin", null },
                    { "4f0cd93c-3fcf-404e-93fe-d72c4aa75206", "f6f10e81-5e4b-4536-824a-0d6cad2f48d2", "Client", null },
                    { "34209f83-15cf-48e3-8b09-26cf2f3918e1", "a8f4e535-3c32-4b44-a87c-9f0346ce2f87", "Analyst", null }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "DistrictId", "Name" },
                values: new object[,]
                {
                    { 37, 1, "Goodhope" },
                    { 7, 5, "Mogorosi" },
                    { 8, 5, "Nata" },
                    { 9, 5, "Paje" },
                    { 10, 5, "Rakops" },
                    { 11, 5, "Sebina" },
                    { 12, 5, "Sefhare" },
                    { 13, 5, "Serowe" },
                    { 14, 5, "Palapye" },
                    { 15, 5, "Shoshong" },
                    { 16, 5, "Taupye" },
                    { 17, 5, "Tonota" },
                    { 18, 5, "Tutume" },
                    { 48, 5, "Sandveld" },
                    { 27, 6, "Francistown" },
                    { 28, 6, "Masunga" },
                    { 29, 7, "Ngamiland East" },
                    { 30, 7, "Ngamiland West" },
                    { 31, 7, "Okavango" },
                    { 19, 9, "Charleshill" },
                    { 20, 9, "Ghanzi" },
                    { 21, 10, "Hukuntsi" },
                    { 6, 5, "Mmaphashalala" },
                    { 22, 10, "Kang" },
                    { 5, 5, "Mmadinare" },
                    { 3, 5, "Lerala" },
                    { 38, 1, "Jwaneng" },
                    { 39, 1, "Kanye" },
                    { 40, 1, "Mabutsane" },
                    { 41, 1, "Moshupa" },
                    { 33, 2, "Gaborone" },
                    { 32, 2, "Mogobane" },
                    { 34, 2, "Otse" },
                    { 35, 2, "Ramotswa" },
                    { 36, 2, "Tlokweng" },
                    { 25, 3, "Letlhakeng" },
                    { 26, 3, "Molepolole" },
                    { 43, 3, "Metsimotlhabe" },
                    { 44, 3, "Mmopane" },
                    { 47, 3, "Thamaga" },
                    { 23, 4, "Mmathubudukwane" },
                    { 24, 4, "Mochudi" },
                    { 42, 4, "Bokaa" },
                    { 45, 4, "Oodi" },
                    { 46, 4, "Rasesa" },
                    { 1, 5, "Bobirwa" },
                    { 2, 5, "Boteti" },
                    { 4, 5, "Mahalapye" },
                    { 50, 10, "Tshabong" }
                });

            migrationBuilder.InsertData(
                table: "Localities",
                columns: new[] { "Id", "LocationId", "Name" },
                values: new object[,]
                {
                    { 1, 33, "BBS Mall" },
                    { 24, 33, "Phase 2" },
                    { 25, 33, "Phase 3" },
                    { 26, 33, "Phase 4" },
                    { 27, 33, "Tlokweng" },
                    { 28, 33, "Tshweneng" },
                    { 29, 33, "whitecity" },
                    { 36, 36, "Boatle" },
                    { 37, 36, "Lesetlhana" },
                    { 23, 33, "Phase 1" },
                    { 31, 43, "Gaphatshwa" },
                    { 35, 45, "Modipane" },
                    { 38, 46, "West Extension" },
                    { 33, 2, "Mafenyatlala" },
                    { 30, 4, "Flowertown" },
                    { 39, 13, "Mere" },
                    { 40, 13, "Morula" },
                    { 41, 15, "matlhabana" },
                    { 32, 27, "Lekgwapheng" },
                    { 34, 45, "Matebeleng" },
                    { 42, 28, "Mine stone" },
                    { 22, 33, "Phakalane" },
                    { 20, 33, "Mogoditshane" },
                    { 2, 33, "block 3" },
                    { 3, 33, "block 3" },
                    { 4, 33, "Block 4" },
                    { 5, 33, "Block 5" },
                    { 6, 33, "Block 6" },
                    { 7, 33, "block 7" },
                    { 8, 33, "block 8" },
                    { 9, 33, "Block 9" },
                    { 21, 33, "Notwane" },
                    { 10, 33, "Broadhurst" },
                    { 12, 33, "Ext 12" },
                    { 13, 33, "Ext 14" },
                    { 14, 33, "Ext 19" },
                    { 15, 33, "Ext 25" },
                    { 16, 33, "Ext 9" },
                    { 17, 33, "Gaborone west" },
                    { 18, 33, "Gaborone" },
                    { 19, 33, "Kgale view" },
                    { 11, 33, "Commerce park" },
                    { 43, 28, "Tati River" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComparableResults_PropertyDetailId",
                table: "ComparableResults",
                column: "PropertyDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Localities_LocationId",
                table: "Localities",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_DistrictId",
                table: "Locations",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Plots_StreetId",
                table: "Plots",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_Plots_polygonShapeId",
                table: "Plots",
                column: "polygonShapeId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetails_BandClassBandName",
                table: "PropertyDetails",
                column: "BandClassBandName");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetails_LocalityId",
                table: "PropertyDetails",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetails_LocationId",
                table: "PropertyDetails",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetails_PlotId1",
                table: "PropertyDetails",
                column: "PlotId1");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetails_PropertyDetailId",
                table: "PropertyDetails",
                column: "PropertyDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Streets_LocalityId",
                table: "Streets",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequests_ClientId",
                table: "ValuationRequests",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequests_LocalityId",
                table: "ValuationRequests",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequests_LocationId",
                table: "ValuationRequests",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequests_PropertyDetailId",
                table: "ValuationRequests",
                column: "PropertyDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationRequests_ValuationId",
                table: "ValuationRequests",
                column: "ValuationId");

            migrationBuilder.CreateIndex(
                name: "IX_Valuations_LocationId",
                table: "Valuations",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComparableResults");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "ValuationRequests");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PropertyDetails");

            migrationBuilder.DropTable(
                name: "Valuations");

            migrationBuilder.DropTable(
                name: "ComparableBandSizes");

            migrationBuilder.DropTable(
                name: "Plots");

            migrationBuilder.DropTable(
                name: "Streets");

            migrationBuilder.DropTable(
                name: "Shapes");

            migrationBuilder.DropTable(
                name: "Localities");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Districts");
        }
    }
}
