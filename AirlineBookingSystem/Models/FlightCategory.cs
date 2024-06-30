using System.ComponentModel.DataAnnotations;

namespace AirlineBookingSystem.Models
{
    public class FlightCategory
    {
        public int FlightCategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
