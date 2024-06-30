using System.ComponentModel.DataAnnotations;

namespace AirlineBookingSystem.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Required]
        public int FlightId { get; set; }

        public Flight Flight { get; set; }

        [Required]
        public string PassengerName { get; set; }

        [Required]
        public string PassengerEmail { get; set; }        
               
       
        public int? AdultCount { get; set; }
        public int? ChildCount { get; set; }
        public int? InfantsCount { get; set; }
      
        [Required(ErrorMessage = "Number of seats is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of seats must be greater than 0.")]
        public int? NumberOfSeats { get; set; }

        [Required(ErrorMessage = "Total price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total price must be greater than 0.")]
        public decimal? TotalPrice { get; set; }
        [Required]
        public int FlightCategoryId { get; set; }
        public FlightCategory FlightCategory { get; set; }

        public string TransactionId { get; set; }
    }
}
