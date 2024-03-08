using hotel_booking_service.Data;
using hotel_booking_service.Models;
using Microsoft.EntityFrameworkCore;

namespace hotel_booking_service.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly ApplicationDbContext _context;

    public HotelRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Hotel> GetAll()
    {
        return _context.Hotels.ToList();
    }

    public Hotel GetById(string id)
    {
        return _context.Hotels.Find(id);
    }

    public List<Hotel> GetHotelsByName(string name)
    {
        // Используем LINQ для фильтрации отелей по имени
        return _context.Hotels
            .Where(h => h.Name == name)
            .ToList();
    }
    
    public List<Hotel> GetHotelsContainingName(string name)
    {
        return _context.Hotels.Where(h => h.Name.Contains(name)).ToList();
    }
    
    public void Add(Hotel hotel)
    {
        _context.Hotels.Add(hotel);
        _context.SaveChanges();
    }

    public void Update(Hotel hotel)
    {
        _context.Entry(hotel).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(Hotel hotel)
    {
        _context.Hotels.Remove(hotel);
        _context.SaveChanges();
    }

    public List<Hotel> SearchHotels(SearchCriteria criteria)
    {
        // Ваша реализация поиска отелей по критериям
        // Пример: возвращаем список отелей, где имя отеля содержит указанный текст
        return _context.Hotels.Where(h => h.City.Contains(criteria.City)).ToList();
    }

    public Room GetRoomById(string roomId)
    {
        return _context.Rooms.Find(roomId);
    }

    public List<Booking> GetBookings(string userId)
    {
        return _context.Bookings.Where(b => b.UserId == userId).ToList();
    }
}
