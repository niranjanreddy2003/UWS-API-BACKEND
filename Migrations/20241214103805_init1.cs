using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UWS_BACK.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authentications",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    phonenumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authentications", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    routeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    routeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.routeId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    locationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    routeId = table.Column<int>(type: "int", nullable: false),
                    locationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    latitude = table.Column<double>(type: "float", nullable: false),
                    longitude = table.Column<double>(type: "float", nullable: false),
                    locationOrder = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.locationId);
                    table.ForeignKey(
                        name: "FK_Locations_Routes_routeId",
                        column: x => x.routeId,
                        principalTable: "Routes",
                        principalColumn: "routeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    TruckId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TruckType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TruckNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TruckStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: true),
                    DriverId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.TruckId);
                    table.ForeignKey(
                        name: "FK_Trucks_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "routeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Pincode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    routeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Authentications_UserId",
                        column: x => x.UserId,
                        principalTable: "Authentications",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Users_Routes_routeId",
                        column: x => x.routeId,
                        principalTable: "Routes",
                        principalColumn: "routeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: true),
                    TruckId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "routeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Drivers_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "TruckId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    feedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    feedbackType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    feedbackSubject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    feedbackDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    feedbackResponse = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    feedbackStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    feedbackSentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.feedbackId);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PublicReports",
                columns: table => new
                {
                    reportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: true),
                    reportType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    reportDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    reportImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reportScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    reportAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reportSentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    reportStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicReports", x => x.reportId);
                    table.ForeignKey(
                        name: "FK_PublicReports_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpecialPickups",
                columns: table => new
                {
                    pickupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: true),
                    pickupType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    pickupWeight = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    pickupDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    pickupImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pickupPreferedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    pickupScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    pickupSentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    pickupStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialPickups", x => x.pickupId);
                    table.ForeignKey(
                        name: "FK_SpecialPickups_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleModel",
                columns: table => new
                {
                    scheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MetalWasteDates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaperWasteDates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElectricalWasteDates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    driverId = table.Column<int>(type: "int", nullable: true),
                    routeId = table.Column<int>(type: "int", nullable: true),
                    truckId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleModel", x => x.scheduleId);
                    table.ForeignKey(
                        name: "FK_ScheduleModel_Drivers_driverId",
                        column: x => x.driverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleModel_Routes_routeId",
                        column: x => x.routeId,
                        principalTable: "Routes",
                        principalColumn: "routeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleModel_Trucks_truckId",
                        column: x => x.truckId,
                        principalTable: "Trucks",
                        principalColumn: "TruckId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_RouteId",
                table: "Drivers",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_TruckId",
                table: "Drivers",
                column: "TruckId",
                unique: true,
                filter: "[TruckId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_userId",
                table: "Feedbacks",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_routeId",
                table: "Locations",
                column: "routeId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicReports_userId",
                table: "PublicReports",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleModel_driverId",
                table: "ScheduleModel",
                column: "driverId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleModel_routeId",
                table: "ScheduleModel",
                column: "routeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleModel_truckId",
                table: "ScheduleModel",
                column: "truckId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialPickups_userId",
                table: "SpecialPickups",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_RouteId",
                table: "Trucks",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_routeId",
                table: "Users",
                column: "routeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "PublicReports");

            migrationBuilder.DropTable(
                name: "ScheduleModel");

            migrationBuilder.DropTable(
                name: "SpecialPickups");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Trucks");

            migrationBuilder.DropTable(
                name: "Authentications");

            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}
