using hotel_booking_service.Models;
using hotel_booking_service.Services;
using Microsoft.AspNetCore.Mvc;

namespace hotel_booking_service.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet("GetAllHotels")]
        public IActionResult GetAllHotels()
        {
            var hotels = _hotelService.GetAllHotels();
            if (hotels != null && hotels.Count > 0)
                return Ok(hotels);
            else
                return NotFound("No hotels found.");
        }

        [HttpGet("GetHotelById")]
        public IActionResult GetHotel([FromQuery] string hotelId)
        {
            // Получаем информацию об отеле по его идентификатору с помощью сервиса
            var hotel = _hotelService.GetHotelById(hotelId);

            // Проверяем, был ли найден отель
            if (hotel == null)
            {
                return NotFound("Hotel not found.");
            }

            // Возвращаем информацию об отеле в виде ответа
            return Ok(hotel);
        }

        [HttpGet("GetHotelsByName")]
        public IActionResult GetHotelsByName(string name)
        {
            var hotels = _hotelService.GetHotelsByName(name);
            if (hotels != null && hotels.Count > 0)
                return Ok(hotels);
            else
                return NotFound("Hotels not found by name.");
        }

        [HttpGet("GetHotelsContainingName")]
        public IActionResult GetHotelsContainingName(string name)
        {
            var hotels = _hotelService.GetHotelsContainingName(name);
            if (hotels.Any())
            {
                return Ok(hotels);
            }
            else
            {
                return NotFound("No hotels found with the specified name.");
            }
        }

        [HttpGet("GetAvailableRooms")]
        public IActionResult GetAvailableRooms([FromQuery] string hotelId)
        {
            var availableRooms = _hotelService.GetAvailableRooms(hotelId);
            if (availableRooms != null)
                return Ok(availableRooms);
            else
                return NotFound($"No available rooms found for hotel with ID: {hotelId}");
        }
        
        // Метод для получения всех бронирований пользователя
        [HttpGet("Bookings")]
        public IActionResult GetBookings([FromQuery] string userId)
        {
            var bookings = _hotelService.GetBookings(userId);
            return Ok(bookings);
        }
        
        // Метод для поиска отелей по критериям
        [HttpPost("FindAvailableRooms")]
        public IActionResult SearchHotels([FromBody] SearchCriteria criteria)
        {
            var rooms = _hotelService.FindAvailableRooms(criteria);
            return Ok(rooms);
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
        [HttpDelete("Cancel")]
        public IActionResult CancelBooking([FromQuery] string bookingId)
        {
            var success = _hotelService.CancelBooking(bookingId);
            if (success)
                return Ok("Booking canceled successfully.");
            else
                return NotFound("Booking not found or cancellation failed.");
        }
    }
}