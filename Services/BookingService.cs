using hotel_booking_service.Models;
using hotel_booking_service.Repositories;

namespace hotel_booking_service.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public IEnumerable<Booking> GetAllBookings()
    {
        return _bookingRepository.GetAll();
    }

    public Booking GetBookingById(string id)
    {
        return _bookingRepository.GetById(id);
    }

    public void AddBooking(Booking booking)
    {
        _bookingRepository.Add(booking);
    }

    public void UpdateBooking(Booking booking)
    {
        _bookingRepository.Update(booking);
    }

    public void DeleteBooking(string id)
    {
        var booking = _bookingRepository.GetById(id);
        if (booking != null)
        {
            _bookingRepository.Delete(booking);
        }
    }
}