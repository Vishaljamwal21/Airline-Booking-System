using AirlineBookingSystem.Data;
using AirlineBookingSystem.Models;
using AirlineBookingSystem.Repository.IRepository;

namespace AirlineBookingSystem.Repository
{
    public class BookingDetailRepository:Repository<BookingDetail>,IBookingDetailRepository
    {
        private ApplicationDbContext _context;
        public BookingDetailRepository(ApplicationDbContext context):base(context)
        {
          _context = context;      
        }
        
       
    }
}
