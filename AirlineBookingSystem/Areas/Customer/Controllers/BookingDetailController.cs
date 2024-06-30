using System.Linq;
using AirlineBookingSystem.Data;
using AirlineBookingSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AirlineBookingSystem.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class BookingDetailController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingDetailController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var userEmail = User.Identity.Name;
            var bookingsQuery = _context.Bookings
                .Where(b => User.IsInRole("Admin") || b.PassengerEmail == userEmail)
                .Select(b => new BookingDetailVM
                {
                    BookingId = b.BookingId,
                    FlightId = b.FlightId,
                    PassengerName = b.PassengerName,
                    PassengerEmail = b.PassengerEmail,
                    NumberOfSeats = (int)b.NumberOfSeats,
                    TotalPrice = (double)b.TotalPrice,
                    FlightNumber = b.Flight.FlightNumber,
                    DepartureAirport = b.Flight.DepartureAirport,
                    DestinationAirport = b.Flight.DestinationAirport,
                    DepartureTime = b.Flight.DepartureTime,
                    ArrivalTime = b.Flight.ArrivalTime
                });
            var bookingsWithFlightDetails = bookingsQuery.ToList();
            return View(bookingsWithFlightDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null) return NotFound();
            _context.Bookings.Remove(booking);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}