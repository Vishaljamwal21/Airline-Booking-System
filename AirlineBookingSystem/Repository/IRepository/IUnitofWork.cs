namespace AirlineBookingSystem.Repository.IRepository
{
    public interface IUnitofWork
    {
        public IAirlineRepository Airline { get; }
        public IFlightRepository Flight { get; }
        public IFlightCategoryRepository FlightCategory { get; }
        public IBookingRepository Booking { get; }
        public IBookingDetailRepository BookingDetail { get; }
        public IApplicationUserRepository ApplicationUser { get; }
        public IOfferRepository Offer { get; }
        void Save();
    }
}
