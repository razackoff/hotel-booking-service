using hotel_booking_service.Models;
using hotel_booking_service.Repositories;
using Serilog;

namespace hotel_booking_service.Services.Internal;

public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly Serilog.ILogger _logger;

        public HotelService(IHotelRepository hotelRepository, IBookingRepository bookingRepository,
            IRoomRepository roomRepository)
        {
            _hotelRepository = hotelRepository;
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _logger = Log.Logger;
        }
            
        public List<Hotel> GetAllHotels()
        {
            return _hotelRepository.GetAll().ToList();
        }
        
        public Hotel GetHotelById(string hotelId)
        {
            // Используем репозиторий для получения информации об отеле по его идентификатору
            return _hotelRepository.GetById(hotelId);
        }
        
        public List<Hotel> GetHotelsByName(string name)
        {
            return _hotelRepository.GetHotelsByName(name);
        }
        
        public List<Hotel> GetHotelsContainingName(string name)
        {
            return _hotelRepository.GetHotelsContainingName(name);
        }
        
        public List<Room> FindAvailableRooms(SearchCriteria criteria)
        {
            // Реализация поиска отелей по критериям
            return _roomRepository.FindAvailableRooms(criteria);
        }
        
        public List<Room> GetAvailableRooms(string hotelId)
        {
            // Предположим, что у вас есть метод в репозитории, который возвращает список доступных комнат для отеля по его идентификатору
            return _roomRepository.GetAvailableRoomsByHotelId(hotelId);
        }

        public bool BookRoom(RoomBookingInfo bookingInfo)
        {
            try
            {
                // Проверка доступности номера
                var room = _roomRepository.GetById(bookingInfo.RoomId);
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

                booking.Id = Guid.NewGuid().ToString();
                
                // Добавление бронирования в базу данных
                _bookingRepository.Add(booking);
                return true;
            }
            catch(Exception ex)
            {
                _logger.Error(ex.ToString(), "Error occurred while adding hotel");
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