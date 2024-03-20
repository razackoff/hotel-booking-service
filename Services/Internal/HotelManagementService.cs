using hotel_booking_service.Models;
using hotel_booking_service.Repositories;
using Serilog;

namespace hotel_booking_service.Services.Internal;

public class HotelManagementService : IHotelManagementService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly Serilog.ILogger _logger;

        public HotelManagementService(IHotelRepository hotelRepository, IRoomRepository roomRepository)
        {
            _hotelRepository = hotelRepository;
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
        
        public List<Room> GetRoomsByHotelId(string hotelId)
        {
            // Получаем список комнат по идентификатору отеля из репозитория комнат
            return _roomRepository.GetRoomsByHotelId(hotelId);
        }
        
        public bool AddHotel(Hotel hotel)
        {
            try
            {
                _hotelRepository.Add(hotel);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString(), "Error occurred while adding hotel");
                return false;
            }
        }
        
        public bool AddRoom(Room room)
        {
            try
            {
                _roomRepository.Add(room);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString(), "Error occurred while adding hotel");
                return false;
            }
        }
        
        public bool UpdateHotel(string hotelId, Hotel hotel)
        {
            var existingHotel = _hotelRepository.GetById(hotelId);
            if (existingHotel == null)
                return false;

            try
            {
                existingHotel.Name = hotel.Name;
                existingHotel.City = hotel.City;
                existingHotel.Address = hotel.Address;
                existingHotel.Description = hotel.Description;
                // Другие свойства для обновления

                _hotelRepository.Update(existingHotel);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateRoomAvailability(string roomId, bool available)
        {
            try
            {
                // Находим комнату в репозитории комнат
                var roomToUpdate = _roomRepository.GetById(roomId);
                if (roomToUpdate == null)
                    return false; // Комната не найдена

                // Обновляем состояние доступности комнаты
                roomToUpdate.IsAvailable = available;

                // Вызываем метод Update из репозитория комнат для сохранения изменений
                _roomRepository.Update(roomToUpdate);

                return true; // Операция завершена успешно
            }
            catch
            {
                return false; // Возникла ошибка при обновлении доступности комнаты
            }
        }

        public bool UpdateRoomPrice(string roomId, decimal price)
        {
            try
            {
                // Находим комнату в репозитории комнат
                var roomToUpdate = _roomRepository.GetById(roomId);
                if (roomToUpdate == null)
                    return false; // Комната не найдена

                // Обновляем цену комнаты
                roomToUpdate.Price = price;

                // Вызываем метод Update из репозитория комнат для сохранения изменений
                _roomRepository.Update(roomToUpdate);

                return true; // Операция завершена успешно
            }
            catch
            {
                return false; // Возникла ошибка при обновлении цены комнаты
            }
        }
        
        public bool RemoveHotel(string hotelId)
        {
            var existingHotel = _hotelRepository.GetById(hotelId);
            if (existingHotel == null)
                return false;

            try
            {
                _hotelRepository.Delete(existingHotel);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }