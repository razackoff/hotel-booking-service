using hotel_booking_service.Models;
using Microsoft.EntityFrameworkCore;

namespace hotel_booking_service.Data;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    DbSet<Hotel> hotels,
    DbSet<Room> rooms,
    DbSet<Booking> bookings)
    : DbContext(options)
{
    public DbSet<Hotel> Hotels { get; set; } = hotels;
    public DbSet<Room> Rooms { get; set; } = rooms;
    public DbSet<Booking> Bookings { get; set; } = bookings;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}