using AirlineBookingSystem.Data;
using AirlineBookingSystem.Models;
using AirlineBookingSystem.Repository.IRepository;

namespace AirlineBookingSystem.Repository
{
    public class FlightRepository:Repository<Flight>,IFlightRepository
    {
        private readonly ApplicationDbContext _context;
        public FlightRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        
    }
}
