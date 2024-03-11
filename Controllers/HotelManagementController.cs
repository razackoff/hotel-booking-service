using hotel_booking_service.DTOs;
using hotel_booking_service.Models;
using hotel_booking_service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hotel_booking_service.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class HotelManagementController : ControllerBase
{
    private readonly IHotelManagementService _hotelManagementService;

    public HotelManagementController(IHotelManagementService hotelManagementService)
    {
        _hotelManagementService = hotelManagementService;
    }
    //[Authorize(Roles = "Admin, Manager")]
    [HttpGet("GetAllHotels")]
    public IActionResult GetAllHotels()
    {
        var hotels = _hotelManagementService.GetAllHotels();
        if (hotels != null && hotels.Count > 0)
            return Ok(hotels);
        else
            return NotFound("No hotels found.");
    }
    
    //[Authorize(Roles = "Admin, Manager")]
    [HttpGet("GetHotelById")]
    public IActionResult GetHotel([FromQuery] string hotelId)
    {
        // Получаем информацию об отеле по его идентификатору с помощью сервиса
        var hotel = _hotelManagementService.GetHotelById(hotelId);

        // Проверяем, был ли найден отель
        if (hotel == null)
        {
            return NotFound("Hotel not found.");
        }

        // Возвращаем информацию об отеле в виде ответа
        return Ok(hotel);
    }
    
    //[Authorize(Roles = "Admin, Manager")]
    [HttpGet("GetRoomsByHotelId")]
    public IActionResult GetRoomsByHotelId(string hotelId)
    {
        // Получаем список комнат отеля по его идентификатору с помощью сервиса
        var rooms = _hotelManagementService.GetRoomsByHotelId(hotelId);

        // Проверяем, были ли найдены комнаты
        if (rooms == null || !rooms.Any())
        {
            return NotFound("Rooms not found for the specified hotel.");
        }

        // Возвращаем список комнат в виде ответа
        return Ok(rooms);
    }
    
    //[Authorize(Roles = "Admin, Manager")]
    [HttpGet("GetHotelsByName")]
    public IActionResult GetHotelsByName(string name)
    {
        var hotels = _hotelManagementService.GetHotelsByName(name);
        if (hotels != null && hotels.Count > 0)
            return Ok(hotels);
        else
            return NotFound("Hotels not found by name.");
    }
    
    //[Authorize(Roles = "Admin, Manager")]
    [HttpGet("GetHotelsContainingName")]
    public IActionResult GetHotelsContainingName(string name)
    {
        var hotels = _hotelManagementService.GetHotelsContainingName(name);
        if (hotels.Any())
        {
            return Ok(hotels);
        }
        else
        {
            return NotFound("No hotels found with the specified name.");
        }
    }
    
    //[Authorize(Roles = "Admin, Manager")]
    [HttpPost("AddHotel")]
    public IActionResult AddHotel(HotelDto hotelDto)
    {
        // Преобразование DTO в модель Hotel перед передачей сервису
        var hotel = new Hotel
        {
            Name = hotelDto.Name,
            City = hotelDto.City,
            Address = hotelDto.Address,
            Description = hotelDto.Description
        };

        hotel.Id = Guid.NewGuid().ToString();

        var success = _hotelManagementService.AddHotel(hotel);
        if (success)
            return Ok("Hotel added successfully.");
        else
            return BadRequest("Failed to add hotel.");
    }

    //[Authorize(Roles = "Admin, Manager")]
    [HttpPost("AddRoom")]
    public IActionResult AddRoom([FromQuery] string hotelId, [FromBody] RoomDto roomDto)
    {
        // Преобразуйте DTO комнаты в модель Room перед передачей сервису
        var room = new Room { Type = roomDto.Type, Price = roomDto.Price, Capacity = roomDto.Capacity };

        room.Id = Guid.NewGuid().ToString();
        room.HotelId = hotelId;

        var success = _hotelManagementService.AddRoom(room);
        if (success)
            return Ok("Room added to hotel successfully.");
        else
            return BadRequest("Failed to add room to hotel.");
    }

    //[Authorize(Roles = "Admin, Manager")]
    [HttpPut("UpdateHotel")]
    public IActionResult UpdateHotel([FromQuery] string hotelId, [FromBody] HotelUpdateDto hotelDto)
    {
        Hotel hotel = new Hotel
        {
            Id = hotelId,
            Name = hotelDto.Name,
            Address = hotelDto.Address,
            City = hotelDto.City,
            Description = hotelDto.Description
        };
        var success = _hotelManagementService.UpdateHotel(hotelId, hotel);
        if (success)
            return Ok("Hotel updated successfully.");
        else
            return BadRequest("Failed to update hotel.");
    }

    //[Authorize(Roles = "Admin, Manager")]
    [HttpDelete("RemoveHotel")]
    public IActionResult RemoveHotel([FromQuery] string hotelId)
    {
        var success = _hotelManagementService.RemoveHotel(hotelId);
        if (success)
            return Ok("Hotel removed successfully.");
        else
            return NotFound("Hotel not found or removal failed.");
    }

    //[Authorize(Roles = "Admin, Manager")]
    [HttpPut("UpdateRoomAvailability")]
    public IActionResult UpdateRoomAvailability([FromQuery] string roomId, [FromQuery] bool available)
    {
        var success = _hotelManagementService.UpdateRoomAvailability(roomId, available);
        if (success)
            return Ok("Room availability updated successfully.");
        else
            return BadRequest("Failed to update room availability.");
    }

    //[Authorize(Roles = "Admin, Manager")]
    [HttpPut("UpdateRoomPrice")]
    public IActionResult UpdateRoomPrice([FromQuery] string roomId, [FromQuery] decimal price)
    {
        var success = _hotelManagementService.UpdateRoomPrice(roomId, price);
        if (success)
            return Ok("Room price updated successfully.");
        else
            return BadRequest("Failed to update room price.");
    }
}