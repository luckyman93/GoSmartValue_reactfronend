using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedNewCreditProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Name", "Description", "Type", "Audience", "Price","ServiceType" },
                values: new object[,]
                {
                    { 9, "Property Management", "Property Management", 0, 3, 0,"37981" },
                    { 10, "Other", "Monitoring and evaluation", 0, 3, 0,"32618"},
                    { 11, "Data Inquiry", "Data Inquiry", 0, 3, 0,"38637"},
                });

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            
        }
    }
}
