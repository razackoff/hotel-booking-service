using hotel_booking_service.Models;

namespace hotel_booking_service.Services;

public interface IPaymentService
{
    bool ProcessPayment(Payment payment);
}