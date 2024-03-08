namespace hotel_booking_service.Models;

public class RoomBookingInfo
{
    public string HotelId { get; set; }
    public string RoomId { get; set; }
    public string UserId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int NumberOfGuests { get; set; }
}