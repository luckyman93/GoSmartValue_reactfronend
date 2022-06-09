using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedNewLocalitySeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "3c4a9937-90ad-498e-8145-998dab69954f");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "94df12bf-df8e-4441-b8ea-f56eed6f61fc");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "cdadf338-cb7f-43c6-ab01-b5b70d4fffc7");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Block 4");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Block 5");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Block 6");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "block 7");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "block 8");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Block 9");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Block 10");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Broadhurst (Ginger)");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Commerce park");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "Ext 11");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Ext 12");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "Ext 14");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "Ext 19");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 17,
                column: "Name",
                value: "Ext 16");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 18,
                column: "Name",
                value: "Ext 25");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 19,
                column: "Name",
                value: "Ext 9");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 20,
                column: "Name",
                value: "Gaborone west");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 21,
                column: "Name",
                value: "Gaborone");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 22,
                column: "Name",
                value: "Kgale view");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 23,
                column: "Name",
                value: "Mogoditshane");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 24,
                column: "Name",
                value: "Notwane");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 25,
                column: "Name",
                value: "Old Naledi");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 26,
                column: "Name",
                value: "Phakalane");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 27,
                column: "Name",
                value: "Phase 1");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 28,
                column: "Name",
                value: "Phase 2");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 29,
                column: "Name",
                value: "Phase 3");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 33, "Phase 4" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 33, "Tlokweng" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 33, "Tshweneng" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 33, "Tsholofelo West" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 33, "Tsholofelo East" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 33, "Tsholofelo" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 33, "Taung" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 33, "Tawana" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 33, "whitecity" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 4, "Flowertown" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 43, "Gaphatshwa" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 27, "Lekgwapheng" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 2, "Mafenyatlala" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 45, "Matebeleng" });

            migrationBuilder.InsertData(
                table: "Localities",
                columns: new[] { "Id", "LocationId", "Name" },
                values: new object[,]
                {
                    { 61, 33, "Gaborone North" },
                    { 60, 33, "Block 3 (Industrial)" },
                    { 59, 33, "Ledumang" },
                    { 58, 33, "Tawana" },
                    { 57, 33, "Ext 27" },
                    { 56, 33, "Ext 2" },
                    { 55, 33, "Village" },
                    { 54, 33, "Ext 10" },
                    { 53, 33, "Partial " },
                    { 49, 13, "Morula" },
                    { 51, 28, "Mine stone" },
                    { 50, 15, "matlhabana" },
                    { 48, 13, "Mere" },
                    { 47, 46, "West Extension" },
                    { 46, 36, "Lesetlhana" },
                    { 45, 36, "Boatle" },
                    { 44, 45, "Modipane" },
                    { 52, 28, "Tati River" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "210db5af-a265-468e-8707-f6f735539865", "02f0d260-f8c0-453c-968d-8e1951cc993a", "Client", null },
                    { "c879d5b3-4ccb-4e03-8afd-a055344ecc7f", "d309a96b-7d6b-4bc1-b81a-b2b49c6cb521", "Admin", null },
                    { "00117d33-f209-4763-8dce-7dcf0549dd51", "4a12173d-b440-4f6d-9af9-38aa9130030b", "Analyst", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "00117d33-f209-4763-8dce-7dcf0549dd51");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "210db5af-a265-468e-8707-f6f735539865");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "c879d5b3-4ccb-4e03-8afd-a055344ecc7f");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "block 3");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Block 4");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Block 5");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Block 6");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "block 7");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "block 8");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Block 9");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Commerce park");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Ext 12");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "Ext 14");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Ext 19");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "Ext 25");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "Ext 9");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 17,
                column: "Name",
                value: "Gaborone west");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 18,
                column: "Name",
                value: "Gaborone");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 19,
                column: "Name",
                value: "Kgale view");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 20,
                column: "Name",
                value: "Mogoditshane");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 21,
                column: "Name",
                value: "Notwane");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 22,
                column: "Name",
                value: "Phakalane");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 23,
                column: "Name",
                value: "Phase 1");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 24,
                column: "Name",
                value: "Phase 2");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 25,
                column: "Name",
                value: "Phase 3");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 26,
                column: "Name",
                value: "Phase 4");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 27,
                column: "Name",
                value: "Tlokweng");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 28,
                column: "Name",
                value: "Tshweneng");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 29,
                column: "Name",
                value: "whitecity");

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 4, "Flowertown" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 43, "Gaphatshwa" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 27, "Lekgwapheng" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 2, "Mafenyatlala" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 45, "Matebeleng" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 45, "Modipane" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 36, "Boatle" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 36, "Lesetlhana" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 46, "West Extension" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 13, "Mere" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 13, "Morula" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 15, "matlhabana" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 28, "Mine stone" });

            migrationBuilder.UpdateData(
                table: "Localities",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "LocationId", "Name" },
                values: new object[] { 28, "Tati River" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "94df12bf-df8e-4441-b8ea-f56eed6f61fc", "e5a31850-5893-44e9-9602-941843275699", "Admin", null },
                    { "3c4a9937-90ad-498e-8145-998dab69954f", "944e37eb-3b1a-42bf-adea-8a1e5a8a1c7b", "Client", null },
                    { "cdadf338-cb7f-43c6-ab01-b5b70d4fffc7", "57194498-2777-40d6-b56c-9fe1bfafbb31", "Analyst", null }
                });
        }
    }
}
