using hotel_booking_service.Models;

namespace hotel_booking_service.Services;

public interface IHotelService
{
    List<Hotel> SearchHotels(SearchCriteria criteria);
    bool BookRoom(RoomBookingInfo bookingInfo);
    bool CancelBooking(string bookingId);
    List<Booking> GetBookings(string userId);
}