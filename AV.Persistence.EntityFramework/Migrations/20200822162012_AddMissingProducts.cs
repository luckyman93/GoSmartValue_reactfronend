using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddMissingProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Name", "Description", "Type", "Audience", "Price" },
                values: new object[,]
                {
                    { 7, "InstantReport", "Instant Report", 3, 2, 750 },
                    { 8, "DetailedReport", "Detailed Report", 4, 3, 250 },
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
