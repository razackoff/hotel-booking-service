using hotel_booking_service.DTOs;
using hotel_booking_service.Models;
using hotel_booking_service.Services;
using Microsoft.AspNetCore.Mvc;

namespace hotel_booking_service.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BookingController(IBookingService bookingService) : ControllerBase
{
    [HttpGet("GetAllBookings")]
    public IActionResult GetAllBookings()
    {
        var bookings = bookingService.GetAllBookings();
        return Ok(bookings);
    }

    [HttpGet("GetBookingById")]
    public IActionResult GetBookingById([FromQuery] string id)
    {
        var booking = bookingService.GetBookingById(id);
        if (booking != null)
            return Ok(booking);
        else
            return NotFound("Booking not found.");
    }

    [HttpPost("AddBooking")]
    public IActionResult AddBooking([FromBody] BookingDto bookingDto)
    {
        // Преобразование DTO в объект бронирования
        var booking = new Booking
        {
            Id = Guid.NewGuid().ToString(),
            UserId = bookingDto.UserId,
            RoomId = bookingDto.RoomId,
        };
        
        bookingService.AddBooking(booking);
        
        return Ok("Booking added successfully.");
    }

    [HttpPut("UpdateBooking")]
    public IActionResult UpdateBooking([FromQuery] string id,[FromBody] BookingDto bookingDto)
    {
        var booking = new Booking
        {
            Id = id,
            UserId = bookingDto.UserId,
            RoomId = bookingDto.RoomId,
        };
        
        bookingService.UpdateBooking(booking);
        return Ok("Booking updated successfully.");
    }

    [HttpDelete("DeleteBooking")]
    public IActionResult DeleteBooking([FromQuery] string id)
    {
        bookingService.DeleteBooking(id);
        return Ok("Booking deleted successfully.");
    }
}