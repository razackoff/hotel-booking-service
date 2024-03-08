using hotel_booking_service.Models;
using hotel_booking_service.Repositories;

namespace hotel_booking_service.Services;

public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IBookingRepository _bookingRepository;

        public HotelService(IHotelRepository hotelRepository, IBookingRepository bookingRepository)
        {
            _hotelRepository = hotelRepository;
            _bookingRepository = bookingRepository;
        }

        public List<Hotel> SearchHotels(SearchCriteria criteria)
        {
            // Реализация поиска отелей по критериям
            return _hotelRepository.SearchHotels(criteria);
        }

        public bool BookRoom(RoomBookingInfo bookingInfo)
        {
            try
            {
                // Проверка доступности номера
                var room = _hotelRepository.GetRoomById(bookingInfo.RoomId);
                if (room == null || !room.IsAvailable)
                    return false;

                // Создание бронирования
                var booking = new Booking
                {
                    UserId = bookingInfo.UserId,
                    RoomId = bookingInfo.RoomId,
                    CheckInDate = bookingInfo.CheckInDate,
                    CheckOutDate = bookingInfo.CheckOutDate,
                    // Другие свойства бронирования
                };

                // Добавление бронирования в базу данных
                _bookingRepository.Add(booking);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CancelBooking(string bookingId)
        {
            try
            {
                var booking = _bookingRepository.GetById(bookingId);
                if (booking != null)
                {
                    _bookingRepository.Delete(booking);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public List<Booking> GetBookings(string userId)
        {
            return _bookingRepository.GetBookings(userId);
        }
    }