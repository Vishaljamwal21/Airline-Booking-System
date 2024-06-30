using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AirlineBookingSystem.Models.ViewModels
{
    public class BookingVM
    {
        public Booking Booking { get; set; }
        public IEnumerable<SelectListItem> FlightCategoryList { get; set; }
        public decimal Price { get; set; } 
        public int AvailableSeats { get; set; }
      
    }
}
