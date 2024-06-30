using AirlineBookingSystem.Models;
using AirlineBookingSystem.Models.ViewModels;
using AirlineBookingSystem.Repository;
using AirlineBookingSystem.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirlineBookingSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class OfferController : Controller
    {
        private readonly IUnitofWork _unitOfWork;

        public OfferController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var offers = _unitOfWork.Offer.GetAll(includeProperties: "Airline,Flight");
            return View(offers);
          
        }
        #region APIs
      
        [HttpGet]
        public IActionResult GetFlightsByAirlineId(int airlineId)
        {
            var flights = _unitOfWork.Flight.GetAll(f => f.AirlineId == airlineId)
                .Select(f => new
                {
                    value = f.FlightId,
                    text = f.FlightNumber
                });
            return Json(flights);
        }
        #endregion

        public IActionResult SaveOrEdit(int? id)
        {
            var viewModel = new OfferViewModel
            {
                Offer = new Offer(),
                Airlines = _unitOfWork.Airline.GetAll()
                    .Select(a => new SelectListItem
                    {
                        Value = a.AirlineId.ToString(),
                        Text = a.Name
                    }),
               
            };

            if (id.HasValue)
            {
                viewModel.Offer = _unitOfWork.Offer.Get(id.Value);
                if (viewModel.Offer == null)
                {
                    return NotFound();
                }
            }
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult SaveOrEdit(OfferViewModel offerViewModel)
        {
            var selectedFlight = _unitOfWork.Flight.Get(offerViewModel.SelectedFlightId);
            if (selectedFlight == null)
            {
                ModelState.AddModelError("", "Selected flight does not exist.");
                return View(offerViewModel);
            }
            var offer = new Offer
            {
                OfferId=offerViewModel.Offer.OfferId,
                Name = offerViewModel.Offer.Name,
                DiscountPercentage = offerViewModel.Offer.DiscountPercentage,
                ValidityPeriod = offerViewModel.Offer.ValidityPeriod,
                AirlineId = offerViewModel.Offer.AirlineId,
                FlightId = offerViewModel.SelectedFlightId
            };
            if (offer.OfferId == 0)
            {
                _unitOfWork.Offer.Add(offer);
            }       
            else
            {
                _unitOfWork.Offer.Update(offer);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var offer = _unitOfWork.Offer.Get(id);
            if (offer == null)
            {
                return NotFound();
            }
            _unitOfWork.Offer.Remove(offer);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
