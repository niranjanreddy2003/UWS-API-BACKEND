using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UWS_BACK.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleModel_Drivers_driverId",
                table: "ScheduleModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleModel_Routes_routeId",
                table: "ScheduleModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleModel_Trucks_truckId",
                table: "ScheduleModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleModel",
                table: "ScheduleModel");

            migrationBuilder.RenameTable(
                name: "ScheduleModel",
                newName: "Schedules");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleModel_truckId",
                table: "Schedules",
                newName: "IX_Schedules_truckId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleModel_routeId",
                table: "Schedules",
                newName: "IX_Schedules_routeId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleModel_driverId",
                table: "Schedules",
                newName: "IX_Schedules_driverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules",
                column: "scheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Drivers_driverId",
                table: "Schedules",
                column: "driverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Routes_routeId",
                table: "Schedules",
                column: "routeId",
                principalTable: "Routes",
                principalColumn: "routeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Trucks_truckId",
                table: "Schedules",
                column: "truckId",
                principalTable: "Trucks",
                principalColumn: "TruckId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Drivers_driverId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Routes_routeId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Trucks_truckId",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules");

            migrationBuilder.RenameTable(
                name: "Schedules",
                newName: "ScheduleModel");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_truckId",
                table: "ScheduleModel",
                newName: "IX_ScheduleModel_truckId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_routeId",
                table: "ScheduleModel",
                newName: "IX_ScheduleModel_routeId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_driverId",
                table: "ScheduleModel",
                newName: "IX_ScheduleModel_driverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleModel",
                table: "ScheduleModel",
                column: "scheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleModel_Drivers_driverId",
                table: "ScheduleModel",
                column: "driverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleModel_Routes_routeId",
                table: "ScheduleModel",
                column: "routeId",
                principalTable: "Routes",
                principalColumn: "routeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleModel_Trucks_truckId",
                table: "ScheduleModel",
                column: "truckId",
                principalTable: "Trucks",
                principalColumn: "TruckId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
