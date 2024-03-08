namespace hotel_booking_service.Models;

public class SearchCriteria
{
    public string City { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int NumberOfGuests { get; set; }
}