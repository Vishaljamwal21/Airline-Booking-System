using AirlineBookingSystem.Data;
using AirlineBookingSystem.Models;
using AirlineBookingSystem.Repository.IRepository;

namespace AirlineBookingSystem.Repository
{
    public class OfferRepository:Repository<Offer>,IOfferRepository
    {
        private readonly ApplicationDbContext _context;
        public OfferRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
