using AirlineBookingSystem.Models;
using AirlineBookingSystem.Models.ViewModels;
using AirlineBookingSystem.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirlineBookingSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class FlightController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FlightController(IUnitofWork unitofWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitofWork = unitofWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            var flightList = _unitofWork.Flight.GetAll(includeProperties: "Airline");
            return Json(new { data = flightList });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var flightInDb = _unitofWork.Flight.Get(id);
            if (flightInDb == null)
                return Json(new { success = false, message = "Flight not found." });
            var webRootPath = _webHostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, flightInDb.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _unitofWork.Flight.Remove(flightInDb);
            _unitofWork.Save();
            return Json(new { success = true, message = "Flight deleted successfully." });
        }
        #endregion


        public IActionResult SaveorEdit(int? id)
        {
            FlightVM flightVM = new FlightVM
            {
                AirlineList = _unitofWork.Airline.GetAll().Select(al => new SelectListItem
                {
                    Text = al.Name,
                    Value = al.AirlineId.ToString()
                }),
                Flight = new Flight()
            };
            if (id == null)
                return View(flightVM);
            else
            {
                flightVM.Flight = _unitofWork.Flight.Get(id.Value);
                if (flightVM.Flight == null)
                    return NotFound();
                else
                    return View(flightVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveorEdit(FlightVM flightVM)
        {
            var existingFlight = _unitofWork.Flight.FirstOrDefault(f => f.FlightNumber == flightVM.Flight.FlightNumber && f.FlightId != flightVM.Flight.FlightId);

            if (existingFlight != null)
            {
                ModelState.AddModelError("Flight.FlightNumber", "Flight number already exists.");
                flightVM.AirlineList = _unitofWork.Airline.GetAll().Select(al => new SelectListItem
                {
                    Text = al.Name,
                    Value = al.AirlineId.ToString()
                });
                return View(flightVM);
            }

            var webRootPath = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (files.Count > 0)
            {
                var fileName = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(files[0].FileName);
                var uploads = Path.Combine(webRootPath, @"images\flights");
                // Deleting old image if exists
                if (!string.IsNullOrEmpty(flightVM.Flight.ImageUrl))
                {
                    var oldImagePath = Path.Combine(webRootPath, flightVM.Flight.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                        System.IO.File.Delete(oldImagePath);
                }
                // Saving new image
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                flightVM.Flight.ImageUrl = @"\images\flights\" + fileName + extension;
            }

            if (flightVM.Flight.FlightId == 0)
                _unitofWork.Flight.Add(flightVM.Flight);
            else
                _unitofWork.Flight.Update(flightVM.Flight);
            _unitofWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
