using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UWS_BACK.Migrations
{
    /// <inheritdoc />
    public partial class niranjan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Trucks_TruckId",
                table: "Drivers");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "feedbackSentDate",
                table: "Feedbacks",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Trucks_TruckId",
                table: "Drivers",
                column: "TruckId",
                principalTable: "Trucks",
                principalColumn: "TruckId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Trucks_TruckId",
                table: "Drivers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "feedbackSentDate",
                table: "Feedbacks",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Trucks_TruckId",
                table: "Drivers",
                column: "TruckId",
                principalTable: "Trucks",
                principalColumn: "TruckId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
