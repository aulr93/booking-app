using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Presistence.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "msAdministrator",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<byte[]>(type: "bytea", nullable: false),
                    HashedPassword = table.Column<string>(type: "text", nullable: false),
                    UserIn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserUp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateUp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_msAdministrator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "msHotelRoom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomNumber = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Floor = table.Column<int>(type: "integer", nullable: false),
                    UserIn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserUp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateUp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_msHotelRoom", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "msVisitor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NIK = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<byte[]>(type: "bytea", nullable: false),
                    HashedPassword = table.Column<string>(type: "text", nullable: false),
                    UserIn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserUp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateUp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_msVisitor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "trHotelRoomBooking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    VisitorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ActualCheckInDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ActualCheckOutDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserIn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserUp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateUp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trHotelRoomBooking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trHotelRoomBooking_msHotelRoom_RoomId",
                        column: x => x.RoomId,
                        principalTable: "msHotelRoom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trHotelRoomBooking_msVisitor_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "msVisitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_msAdministrator_DateIn",
                table: "msAdministrator",
                column: "DateIn");

            migrationBuilder.CreateIndex(
                name: "IX_msAdministrator_DateUp",
                table: "msAdministrator",
                column: "DateUp");

            migrationBuilder.CreateIndex(
                name: "IX_msAdministrator_UserIn",
                table: "msAdministrator",
                column: "UserIn");

            migrationBuilder.CreateIndex(
                name: "IX_msAdministrator_UserUp",
                table: "msAdministrator",
                column: "UserUp");

            migrationBuilder.CreateIndex(
                name: "IX_msHotelRoom_DateIn",
                table: "msHotelRoom",
                column: "DateIn");

            migrationBuilder.CreateIndex(
                name: "IX_msHotelRoom_DateUp",
                table: "msHotelRoom",
                column: "DateUp");

            migrationBuilder.CreateIndex(
                name: "IX_msHotelRoom_UserIn",
                table: "msHotelRoom",
                column: "UserIn");

            migrationBuilder.CreateIndex(
                name: "IX_msHotelRoom_UserUp",
                table: "msHotelRoom",
                column: "UserUp");

            migrationBuilder.CreateIndex(
                name: "IX_msVisitor_DateIn",
                table: "msVisitor",
                column: "DateIn");

            migrationBuilder.CreateIndex(
                name: "IX_msVisitor_DateUp",
                table: "msVisitor",
                column: "DateUp");

            migrationBuilder.CreateIndex(
                name: "IX_msVisitor_UserIn",
                table: "msVisitor",
                column: "UserIn");

            migrationBuilder.CreateIndex(
                name: "IX_msVisitor_UserUp",
                table: "msVisitor",
                column: "UserUp");

            migrationBuilder.CreateIndex(
                name: "IX_trHotelRoomBooking_DateIn",
                table: "trHotelRoomBooking",
                column: "DateIn");

            migrationBuilder.CreateIndex(
                name: "IX_trHotelRoomBooking_DateUp",
                table: "trHotelRoomBooking",
                column: "DateUp");

            migrationBuilder.CreateIndex(
                name: "IX_trHotelRoomBooking_RoomId",
                table: "trHotelRoomBooking",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_trHotelRoomBooking_UserIn",
                table: "trHotelRoomBooking",
                column: "UserIn");

            migrationBuilder.CreateIndex(
                name: "IX_trHotelRoomBooking_UserUp",
                table: "trHotelRoomBooking",
                column: "UserUp");

            migrationBuilder.CreateIndex(
                name: "IX_trHotelRoomBooking_VisitorId",
                table: "trHotelRoomBooking",
                column: "VisitorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "msAdministrator");

            migrationBuilder.DropTable(
                name: "trHotelRoomBooking");

            migrationBuilder.DropTable(
                name: "msHotelRoom");

            migrationBuilder.DropTable(
                name: "msVisitor");
        }
    }
}
