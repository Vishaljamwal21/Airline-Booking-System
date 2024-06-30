using AirlineBookingSystem.Data;
using AirlineBookingSystem.Models;
using AirlineBookingSystem.Repository.IRepository;

namespace AirlineBookingSystem.Repository
{
    public class FlightCategoryRepository:Repository<FlightCategory>,IFlightCategoryRepository
    {
        private ApplicationDbContext _context;
        public FlightCategoryRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
       
    }
}
