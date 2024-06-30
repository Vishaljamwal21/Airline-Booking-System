using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirlineBookingSystem.Models.ViewModels
{
    public class FlightVM
    {
        public Flight Flight { get; set; }
        public IEnumerable<SelectListItem> AirlineList { get; set; }
    }
}
