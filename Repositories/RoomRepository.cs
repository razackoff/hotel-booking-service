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
    
    public List<Room> GetAvailableRoomsByHotelId(string hotelId)
    {
        // Предположим, что у вас есть таблица комнат (Rooms) с колонкой, отвечающей за доступность комнаты (IsAvailable)
        // и связанная с таблицей отелей (Hotels) через внешний ключ HotelId
        return _context.Rooms.Where(r => r.HotelId == hotelId && r.IsAvailable).ToList();
    }
    
    public List<Room> FindAvailableRooms(SearchCriteria criteria)
    {
        var availableRooms = _context.Rooms
            .Where(room => room.IsAvailable && room.Type != null && room.Price > 0) // фильтрация доступных номеров
            .Join(_context.Hotels, room => room.HotelId, hotel => hotel.Id, (room, hotel) => new { Room = room, Hotel = hotel }) // присоединение к отелям
            .Where(roomHotel => roomHotel.Hotel.City == criteria.City) // фильтрация по городу
            .Select(roomHotel => roomHotel.Room) // выборка только номеров
            .ToList();

        // Фильтрация по датам бронирования
        var bookedRoomIds = _context.Bookings
            .Where(booking =>
                (criteria.CheckInDate >= booking.CheckInDate && criteria.CheckInDate < booking.CheckOutDate) ||
                (criteria.CheckOutDate > booking.CheckInDate && criteria.CheckOutDate <= booking.CheckOutDate) ||
                (criteria.CheckInDate <= booking.CheckInDate && criteria.CheckOutDate >= booking.CheckOutDate))
            .Select(booking => booking.RoomId)
            .ToList();

        availableRooms = availableRooms
            .Where(room => !bookedRoomIds.Contains(room.Id)) // исключаем уже забронированные номера
            .ToList();

        // Фильтрация по вместимости
        availableRooms = availableRooms
            .Where(room => room.Capacity >= criteria.NumberOfGuests)
            .ToList();

        return availableRooms;
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