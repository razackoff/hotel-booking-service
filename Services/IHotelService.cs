using hotel_booking_service.Models;

namespace hotel_booking_service.Services;

public interface IHotelService
{
    List<Hotel> GetAllHotels();
    Hotel GetHotelById(string hotelId);  
    List<Hotel> GetHotelsByName(string name);
    List<Hotel> GetHotelsContainingName(string name);
    List<Room> FindAvailableRooms(SearchCriteria criteria);
    List<Room> GetAvailableRooms(string hotelId);
    bool BookRoom(RoomBookingInfo bookingInfo);
    bool CancelBooking(string bookingId);
    List<Booking> GetBookings(string userId);
}