using Microsoft.EntityFrameworkCore.Migrations;

namespace FreightManagement.Infrastructure.Persistence.Migrations
{
    public partial class SampleMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customer_locations_customers_CustomerId",
                table: "customer_locations");

            migrationBuilder.DropIndex(
                name: "IX_customer_locations_CustomerId",
                table: "customer_locations");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "customer_locations");

            migrationBuilder.CreateTable(
                name: "CustomerLocation",
                columns: table => new
                {
                    CustomersId = table.Column<long>(type: "bigint", nullable: false),
                    LocationsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLocation", x => new { x.CustomersId, x.LocationsId });
                    table.ForeignKey(
                        name: "FK_CustomerLocation_customer_locations_LocationsId",
                        column: x => x.LocationsId,
                        principalTable: "customer_locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerLocation_customers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLocation_LocationsId",
                table: "CustomerLocation",
                column: "LocationsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerLocation");

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "customer_locations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_customer_locations_CustomerId",
                table: "customer_locations",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_customer_locations_customers_CustomerId",
                table: "customer_locations",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
