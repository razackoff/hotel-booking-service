using hotel_booking_service.Models;

namespace hotel_booking_service.Repositories;

public interface IBookingRepository
{
    IEnumerable<Booking> GetAll();
    Booking GetById(string id);
    void Add(Booking booking);
    void Update(Booking booking);
    void Delete(Booking booking);
    List<Booking> GetBookings(string userId); // Добавленный метод
}