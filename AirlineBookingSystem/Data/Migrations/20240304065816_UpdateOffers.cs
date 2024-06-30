using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineBookingSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOffers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Flights_FlightId",
                table: "Offers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FlightId",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AirlineId",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_AirlineId",
                table: "Offers",
                column: "AirlineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Airlines_AirlineId",
                table: "Offers",
                column: "AirlineId",
                principalTable: "Airlines",
                principalColumn: "AirlineId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Flights_FlightId",
                table: "Offers",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Airlines_AirlineId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Flights_FlightId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_AirlineId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "AirlineId",
                table: "Offers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "FlightId",
                table: "Offers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Flights_FlightId",
                table: "Offers",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId");
        }
    }
}
