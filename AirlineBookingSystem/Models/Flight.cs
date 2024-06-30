using System.ComponentModel.DataAnnotations;

namespace AirlineBookingSystem.Models
{
    public class Flight
    {
        public int FlightId { get; set; }

        [Required]
        public string FlightNumber { get; set; }

        [Required]
        public int AirlineId { get; set; }

        public Airline Airline { get; set; }
        public string ImageUrl { get; set; }

        [Required]
        public string DepartureAirport { get; set; }

        [Required]
        public string DestinationAirport { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required]
        public int AvailableSeats { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<FlightCategory> FlightCategories { get; set; }
        public ICollection<Offer> Offers { get; set; }

    }
}
