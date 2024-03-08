using hotel_booking_service.Data;
using hotel_booking_service.Models;
using Microsoft.EntityFrameworkCore;

namespace hotel_booking_service.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly ApplicationDbContext _context;

    public BookingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Booking> GetAll()
    {
        return _context.Bookings.ToList();
    }

    public Booking GetById(string id)
    {
        return _context.Bookings.Find(id);
    }

    public void Add(Booking booking)
    {
        _context.Bookings.Add(booking);
        _context.SaveChanges();
    }

    public void Update(Booking booking)
    {
        _context.Entry(booking).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(Booking booking)
    {
        _context.Bookings.Remove(booking);
        _context.SaveChanges();
    }
    
    public List<Booking> GetBookings(string userId)
    {
        return _context.Bookings.Where(b => b.UserId == userId).ToList();
    }
}
