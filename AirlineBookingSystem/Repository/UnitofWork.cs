using AirlineBookingSystem.Data;
using AirlineBookingSystem.Repository.IRepository;

namespace AirlineBookingSystem.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext _context;
        public UnitofWork(ApplicationDbContext context)
        {
         _context = context; 
         Airline = new AirlineRepository(context);
         Flight = new FlightRepository(context);
         FlightCategory = new FlightCategoryRepository(context);
        
         Booking = new BookingRepository(context);
         BookingDetail = new BookingDetailRepository(context);
          ApplicationUser = new ApplicationUserRepository(context);
            Offer = new OfferRepository(context);
        
        }       
        
        public IAirlineRepository Airline { get; private set; }

        public IFlightRepository Flight { get; private set; }

        public IFlightCategoryRepository FlightCategory { get; private set; }



        public IBookingRepository Booking { get; private set; }

        public IBookingDetailRepository BookingDetail { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IOfferRepository Offer { get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
