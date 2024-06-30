using AirlineBookingSystem.Data;
using AirlineBookingSystem.Models.ViewModels;
using AirlineBookingSystem.Models;
using AirlineBookingSystem.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using AirlineBookingSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Stripe;
using System.Text;

namespace AirlineBookingSystem.Areas.Customer.Controllers
{
     [Area("Customer")]    
    public class BookingController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private readonly UserManager<IdentityUser> _userManager;
        public BookingController(IUnitofWork unitofWork, UserManager<IdentityUser> userManager)
        {
            _unitofWork = unitofWork;
            _userManager = userManager;

        }
        public IActionResult Index(int flightId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            var flight = _unitofWork.Flight.Get(flightId);
            var bookingVM = new BookingVM
            {
                Booking = new Booking(),
                AvailableSeats = flight.AvailableSeats
            };
            bookingVM.Booking.FlightId = flightId;
            var flightCategories = _unitofWork.FlightCategory.GetAll(); 
            ViewBag.FlightCategoryList = flightCategories.Select(fc => new SelectListItem
            {
                Text = $"{fc.Name} - ${fc.Price}",
                Value = $"{fc.FlightCategoryId}|{fc.Price}"
            });
            var defaultCategory = flightCategories.FirstOrDefault();
            if (defaultCategory != null)
            {
                bookingVM.Booking.FlightCategoryId = defaultCategory.FlightCategoryId;
            }
            return View(bookingVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string stripeToken, BookingVM bookingVM)
        {
            
            if (!User.Identity.IsAuthenticated)
            {                
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }          
            string userEmail = User.Identity.Name;           
            if (bookingVM.Booking.PassengerEmail != userEmail)
            {                
                ModelState.AddModelError("Booking.PassengerEmail", "Invalid passenger email.");                
                var flightCategories = _unitofWork.FlightCategory.GetAll();
                ViewBag.FlightCategoryList = flightCategories.Select(fc => new SelectListItem
                {
                    Text = $"{fc.Name} - ${fc.Price}",
                    Value = $"{fc.FlightCategoryId}|{fc.Price}"
                });
                return View(bookingVM);
            }            
            var booking = new Booking
            {
                FlightId = bookingVM.Booking.FlightId,
                PassengerName = bookingVM.Booking.PassengerName,
                PassengerEmail = bookingVM.Booking.PassengerEmail,
                AdultCount = bookingVM.Booking.AdultCount,
                ChildCount = bookingVM.Booking.ChildCount,
                InfantsCount = bookingVM.Booking.InfantsCount,
                NumberOfSeats = bookingVM.Booking.NumberOfSeats,
                TotalPrice = bookingVM.Booking.TotalPrice,
                FlightCategoryId = bookingVM.Booking.FlightCategoryId
            };
            _unitofWork.Booking.Add(booking);
            _unitofWork.Save();

            #region Stripe

            try
            {
                var options = new ChargeCreateOptions
                {
                    Amount = Convert.ToInt32(bookingVM.Booking.TotalPrice * 100), 
                    Currency = "usd",
                    Description = $"OrderId: {bookingVM.Booking.FlightId}",
                    Source = stripeToken
                };
                var service = new ChargeService();
                Charge charge = await service.CreateAsync(options);
                booking.TransactionId = charge.BalanceTransactionId;

                if (charge.Status.ToLower() == "succeeded")
                {
                    // Update payment status (example)
                    // ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusApproved;
                    // ShoppingCartVM.OrderHeader.OrderStatus = SD.OrderStatusApproved;
                    // ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Payment failed. Please try again.");
                    return View(bookingVM);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Payment error: {ex.Message}");
                return View(bookingVM);
            }

            _unitofWork.Save();

            return RedirectToAction("Confirm", new { bookingId = booking.BookingId });

            #endregion
        }

        public IActionResult Confirm(int bookingId)
        {
            var booking = _unitofWork.Booking.Get(bookingId);
            if (booking == null)
            {                
                return RedirectToAction("Index");
            }           
            ViewBag.BookingId = booking.BookingId;
            ViewBag.Message = "Your ticket is confirmed. Thank you!";

            return View();
        }
    }
  
}

