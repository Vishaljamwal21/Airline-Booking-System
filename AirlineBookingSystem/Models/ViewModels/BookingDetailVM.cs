namespace AirlineBookingSystem.Models.ViewModels
{
    public class BookingDetailVM
    {
        public int BookingId { get; set; }
        public int FlightId { get; set; }
        public string PassengerName { get; set; }
        public string PassengerEmail { get; set; }
        public int NumberOfSeats { get; set; }
        public double TotalPrice { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string DestinationAirport { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
