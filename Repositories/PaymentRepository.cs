using hotel_booking_service.Data;
using hotel_booking_service.Models;
using Microsoft.EntityFrameworkCore;

namespace hotel_booking_service.Repositories;

/*public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _context;

    public PaymentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Payment> GetAll()
    {
        return _context.Payments.ToList();
    }

    public Payment GetById(string id)
    {
        return _context.Payments.Find(id);
    }

    public void Add(Payment payment)
    {
        _context.Payments.Add(payment);
        _context.SaveChanges();
    }

    public void Update(Payment payment)
    {
        _context.Entry(payment).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(Payment payment)
    {
        _context.Payments.Remove(payment);
        _context.SaveChanges();
    }
}*/