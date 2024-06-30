namespace AirlineBookingSystem.Models.ViewModels
{
    public class FlightWithBookingsViewModel
    {
        public Flight Flight { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
