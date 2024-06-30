using AirlineBookingSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace AirlineBookingSystem.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {         
          var currentTime = DateTime.Now;
            var flightList = _context.Flights
                .Include(f => f.Airline)
                .Include(f => f.Offers) 
                .Where(f => f.DepartureTime > currentTime)
                .ToList();
            foreach (var flight in flightList)
            {                
                flight.Offers = flight.Offers.Where(o => o.AirlineId == flight.AirlineId && o.FlightId == flight.FlightId).ToList();                
                flight.Bookings = _context.Bookings.Where(b => b.FlightId == flight.FlightId).ToList();
            }
            return View(flightList);
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
