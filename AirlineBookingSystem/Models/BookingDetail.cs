namespace AirlineBookingSystem.Models
{
    public class BookingDetail
    {
        public int BookingDetailId { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
    }
}
