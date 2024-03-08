using hotel_booking_service.Models;
using hotel_booking_service.Services;
using Microsoft.AspNetCore.Mvc;

namespace hotel_booking_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        // Метод для поиска отелей по критериям
        [HttpGet("Search")]
        public IActionResult SearchHotels(SearchCriteria criteria)
        {
            var hotels = _hotelService.SearchHotels(criteria);
            return Ok(hotels);
        }

        // Метод для бронирования комнаты
        [HttpPost("Book")]
        public IActionResult BookRoom(RoomBookingInfo bookingInfo)
        {
            var success = _hotelService.BookRoom(bookingInfo);
            if (success)
                return Ok("Room booked successfully.");
            else
                return BadRequest("Booking failed.");
        }

        // Метод для отмены бронирования
        [HttpDelete("Cancel/{bookingId}")]
        public IActionResult CancelBooking(string bookingId)
        {
            var success = _hotelService.CancelBooking(bookingId);
            if (success)
                return Ok("Booking canceled successfully.");
            else
                return NotFound("Booking not found or cancellation failed.");
        }

        // Метод для получения всех бронирований пользователя
        [HttpGet("Bookings/{userId}")]
        public IActionResult GetBookings(string userId)
        {
            var bookings = _hotelService.GetBookings(userId);
            return Ok(bookings);
        }
    }
}