using hotel_booking_service.Data;
using hotel_booking_service.Models;
using Microsoft.EntityFrameworkCore;

namespace hotel_booking_service.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _context;

    public RoomRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Room> GetAll()
    {
        return _context.Rooms.ToList();
    }

    public List<Room> GetRoomsByHotelId(string hotelId)
    {
        // Используем LINQ для получения комнат, связанных с указанным отелем
        return _context.Rooms
            .Where(r => r.HotelId == hotelId)
            .ToList();
    }
    
    public Room GetById(string id)
    {
        return _context.Rooms.Find(id);
    }

    public void Add(Room room)
    {
        _context.Rooms.Add(room);
        _context.SaveChanges();
    }

    public void Update(Room room)
    {
        _context.Entry(room).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(Room room)
    {
        _context.Rooms.Remove(room);
        _context.SaveChanges();
    }
}