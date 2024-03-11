using hotel_booking_service.Models;

namespace hotel_booking_service.Repositories;

public interface IRoomRepository
{
    IEnumerable<Room> GetAll();
    List<Room> GetRoomsByHotelId(string hotelId);
    List<Room> GetAvailableRoomsByHotelId(string hotelId);
    Room GetById(string id);
    List<Room> FindAvailableRooms(SearchCriteria criteria);
    void Add(Room room);
    void Update(Room room);
    void Delete(Room room);
}