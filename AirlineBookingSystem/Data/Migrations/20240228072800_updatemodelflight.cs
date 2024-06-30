using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineBookingSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatemodelflight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "FlightCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlightCategories_FlightId",
                table: "FlightCategories",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightCategories_Flights_FlightId",
                table: "FlightCategories",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightCategories_Flights_FlightId",
                table: "FlightCategories");

            migrationBuilder.DropIndex(
                name: "IX_FlightCategories_FlightId",
                table: "FlightCategories");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "FlightCategories");
        }
    }
}
