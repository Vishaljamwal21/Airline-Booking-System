using AirlineBookingSystem.Models;
using AirlineBookingSystem.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class AirlineController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        public AirlineController(IUnitofWork unitofWork)
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
            var airlineList = _unitofWork.Airline.GetAll();
            return Json(new { data = airlineList });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var airlineInDb = _unitofWork.Airline.Get(id);
            if (airlineInDb == null)
             return Json(new { success = false, message = "Something Went Wrong While Delete Data" });
            _unitofWork.Airline.Remove(airlineInDb);
            _unitofWork.Save();
            return Json(new { success = true, message = "Data Successfully Deleted" });
        }
        #endregion

        public IActionResult SaveOrEdit(int? id)
        {
            Airline airline = new Airline();
            if (id == null) return View(airline);
            airline = _unitofWork.Airline.Get(id.GetValueOrDefault());
            if (airline == null) return NotFound();
            return View(airline);
        }


        [HttpPost]
        public IActionResult SaveOrEdit(Airline airline)
        {
            if (airline == null)
            {
                return BadRequest();
            }

            if (airline.AirlineId != 0)
            {
                var existingAirline = _unitofWork.Airline.Get(airline.AirlineId);
                if (existingAirline == null)
                {
                    return NotFound();
                }
                existingAirline.Name = airline.Name;
                existingAirline.Code = airline.Code;
                _unitofWork.Airline.Update(existingAirline);
            }
            else
            {
                var existingAirlineWithSameCode = _unitofWork.Airline.FirstOrDefault(a => a.Code == airline.Code);
                if (existingAirlineWithSameCode != null)
                {
                    ModelState.AddModelError(nameof(Airline.Code), "An airline with the same code already exists.");
                    return Json(new { success = false, message = "An airline with the same code already exists." });
                }
                _unitofWork.Airline.Add(airline);
            }
            _unitofWork.Save();
            return Json(new { success = true, message = "Data updated successfully." });
        }

    }
}
