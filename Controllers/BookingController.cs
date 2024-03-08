using hotel_booking_service.Models;
using hotel_booking_service.Services;
using Microsoft.AspNetCore.Mvc;

namespace hotel_booking_service.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    public IActionResult GetAllBookings()
    {
        var bookings = _bookingService.GetAllBookings();
        return Ok(bookings);
    }

    [HttpGet("{id}")]
    public IActionResult GetBookingById(string id)
    {
        var booking = _bookingService.GetBookingById(id);
        if (booking != null)
            return Ok(booking);
        else
            return NotFound("Booking not found.");
    }

    [HttpPost]
    public IActionResult AddBooking(Booking booking)
    {
        _bookingService.AddBooking(booking);
        return Ok("Booking added successfully.");
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBooking(int id, Booking booking)
    {
        _bookingService.UpdateBooking(booking);
        return Ok("Booking updated successfully.");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBooking(string id)
    {
        _bookingService.DeleteBooking(id);
        return Ok("Booking deleted successfully.");
    }
}