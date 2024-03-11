using hotel_booking_service.Models;

namespace hotel_booking_service.Repositories;

public interface IHotelRepository
{
    List<Hotel> GetAll();
    Hotel GetById(string id);
    List<Hotel> GetHotelsByName(string name);
    List<Hotel> GetHotelsContainingName(string name);
    void Add(Hotel hotel);
    void Update(Hotel hotel);
    void Delete(Hotel hotel);
    List<Booking> GetBookings(string userId);
}