using AirlineBookingSystem.Models;
using AirlineBookingSystem.Repository;
using AirlineBookingSystem.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AirlineBookingSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class FlightCategoryController : Controller
    {
        private readonly IUnitofWork _unitofWork;

        public FlightCategoryController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region APIs

        public IActionResult GetAll()
        {
            var flightCategoryList = _unitofWork.FlightCategory.GetAll();
            return Json(new { data = flightCategoryList });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var flightCategoryInDb = _unitofWork.FlightCategory.Get(id);
            if (flightCategoryInDb == null)
                return Json(new { success = false, message = "Something Went Wrong While Deleting Data" });

            _unitofWork.FlightCategory.Remove(flightCategoryInDb);
            _unitofWork.Save();
            return Json(new { success = true, message = "Data Successfully Deleted" });
        }

        #endregion

        public IActionResult SaveOrEdit(int? id)
        {
            FlightCategory flightCategory = id == null ? new FlightCategory() : _unitofWork.FlightCategory.Get(id.GetValueOrDefault());
            if (flightCategory == null)
                return NotFound();

            return View(flightCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveOrEdit(FlightCategory flightCategory)
        {         

            if (!ModelState.IsValid)
                return View(flightCategory);

            if (flightCategory.FlightCategoryId == 0)
                _unitofWork.FlightCategory.Add(flightCategory);
            else
                _unitofWork.FlightCategory.Update(flightCategory);

            _unitofWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
