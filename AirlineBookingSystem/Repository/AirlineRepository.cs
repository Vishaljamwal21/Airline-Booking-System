using AirlineBookingSystem.Data;
using AirlineBookingSystem.Models;
using AirlineBookingSystem.Repository.IRepository;

namespace AirlineBookingSystem.Repository
{
    public class AirlineRepository:Repository<Airline>,IAirlineRepository
    {
        private readonly ApplicationDbContext _context;
        public AirlineRepository(ApplicationDbContext context) : base(context)
        {
           _context = context;     
        }
        
     
    }
}
