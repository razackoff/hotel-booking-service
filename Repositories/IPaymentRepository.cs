using hotel_booking_service.Models;

namespace hotel_booking_service.Repositories;

public interface IPaymentRepository
{
    IEnumerable<Payment> GetAll();
    Payment GetById(string id);
    void Add(Payment payment);
    void Update(Payment payment);
    void Delete(Payment payment);
}