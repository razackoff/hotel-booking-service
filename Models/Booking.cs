namespace hotel_booking_service.Models;

public class Booking
{
    public string Id { get; set; }
    public string HotelId { get; set; }
    public string RoomId { get; set; }
    public string UserId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
}