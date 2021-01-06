using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingFlight.Migrations
{
    public partial class reservation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlightTicketID",
                table: "Reservations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_FlightTicketID",
                table: "Reservations",
                column: "FlightTicketID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_FlightTickets_FlightTicketID",
                table: "Reservations",
                column: "FlightTicketID",
                principalTable: "FlightTickets",
                principalColumn: "FlightTicketID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_FlightTickets_FlightTicketID",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_FlightTicketID",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "FlightTicketID",
                table: "Reservations");
        }
    }
}
