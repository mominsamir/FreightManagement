using Microsoft.EntityFrameworkCore.Migrations;

namespace FreightManagement.Infrastructure.Persistence.Migrations
{
    public partial class user_to_driver_schedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
/*            migrationBuilder.DropForeignKey(
                name: "FK_driver_truck_trailer_schedules_users_DriverId",
                table: "driver_truck_trailer_schedules");
*/
            migrationBuilder.AddColumn<long>(
                name: "DriverId",
                table: "driver_truck_trailer_schedules",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_driver_truck_trailer_schedules_users_DriverId",
                table: "driver_truck_trailer_schedules",
                column: "DriverId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropColumn(
                name: "Driver",
                table: "driver_truck_trailer_schedules");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
