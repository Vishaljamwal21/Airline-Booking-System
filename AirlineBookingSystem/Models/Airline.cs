using System.ComponentModel.DataAnnotations;

namespace AirlineBookingSystem.Models
{
    public class Airline
    {
        public int AirlineId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }
    }
}
