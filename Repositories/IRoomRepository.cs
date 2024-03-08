using hotel_booking_service.Models;

namespace hotel_booking_service.Repositories;

public interface IRoomRepository
{
    IEnumerable<Room> GetAll();
    List<Room> GetRoomsByHotelId(string hotelId);
    Room GetById(string id);
    void Add(Room room);
    void Update(Room room);
    void Delete(Room room);
}