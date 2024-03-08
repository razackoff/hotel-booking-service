using hotel_booking_service.Models;

namespace hotel_booking_service.Services;

public interface IBookingService
{
    IEnumerable<Booking> GetAllBookings();
    Booking GetBookingById(string id);
    void AddBooking(Booking booking);
    void UpdateBooking(Booking booking);
    void DeleteBooking(string id);
}