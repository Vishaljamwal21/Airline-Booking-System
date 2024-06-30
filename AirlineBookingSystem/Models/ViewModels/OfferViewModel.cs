using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AirlineBookingSystem.Models.ViewModels
{
    public class OfferViewModel
    {
        public Offer Offer { get; set; }

        [Required(ErrorMessage = "Please select an airline.")]
        public int SelectedAirlineId { get; set; }
        public IEnumerable<SelectListItem> Airlines { get; set; }

        [Required(ErrorMessage = "Please select a flight.")]
        public int SelectedFlightId { get; set; }
        public IEnumerable<SelectListItem> Flights { get; set; }

    }
}
