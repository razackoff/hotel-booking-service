using hotel_booking_service.DTOs;
using hotel_booking_service.Models;

namespace hotel_booking_service.Services;

public interface IHotelManagementService
{
    List<Hotel> GetAllHotels();
    Hotel GetHotelById(string hotelId);
    List<Room> GetRoomsByHotelId(string hotelId);   
    List<Hotel> GetHotelsByName(string name);
    List<Hotel> GetHotelsContainingName(string name);
    bool AddHotel(Hotel hotel);
    bool UpdateHotel(string hotelId, Hotel hotel);
    bool RemoveHotel(string hotelId);
    bool UpdateRoomAvailability(string roomId, bool available);
    bool UpdateRoomPrice(string roomId, decimal price);
    bool AddRoom(Room room);
}
