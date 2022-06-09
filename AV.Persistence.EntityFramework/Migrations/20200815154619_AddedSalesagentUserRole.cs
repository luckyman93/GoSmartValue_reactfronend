using Microsoft.EntityFrameworkCore.Migrations;

namespace AV.Persistence.EntityFramework.Migrations
{
    public partial class AddedSalesagentUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "10a01d4b-6c7e-4c5f-a097-d759755fb773", "81baab57-1559-4a14-b2bf-98e04a6c48af", "salesagent", "SALESAGENT" }
                });
        }
        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
         
        }
    }
}
