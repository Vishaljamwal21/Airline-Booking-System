using System.ComponentModel.DataAnnotations;

namespace AirlineBookingSystem.Models
{
    public class Offer
    {
        public int OfferId { get; set; }

        [Required(ErrorMessage = "Please select an airline.")]
        public int AirlineId { get; set; }

        [Required(ErrorMessage = "Please select a flight.")]
        public int FlightId { get; set; }

        [Required(ErrorMessage = "Please enter the offer name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the discount percentage.")]
        public decimal DiscountPercentage { get; set; }

        [Required(ErrorMessage = "Please enter the validity period.")]
        public DateTime ValidityPeriod { get; set; }

        // Navigation properties
        public Airline Airline { get; set; }
        public Flight Flight { get; set; }
    }
}
