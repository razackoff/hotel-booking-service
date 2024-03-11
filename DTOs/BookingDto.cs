namespace hotel_booking_service.DTOs;

public class BookingDto
{
    public string RoomId { get; set; }
    public string UserId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
}