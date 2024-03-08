namespace hotel_booking_service.Models;

public class Room
{
    public string Id { get; set; }
    public string HotelId { get; set; }
    public string Type { get; set; }
    public decimal Price { get; set; }
    public decimal Capacity { get; set; }
    public bool IsAvailable { get; set; }
}