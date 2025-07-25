//using HotelBookingAPI.Data;
//using HotelBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

////namespace HotelBookingAPI.Services
//{
    public class RoomService
    {
        private readonly AppDbContext _context;

        public RoomService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Room> GetAvailableRooms(DateTime start, DateTime end, int guests)
        {
            return _context.Rooms
                .Include(r => r.Bookings)
                .Where(r => r.Capacity >= guests &&
                            !r.Bookings.Any(b => b.StartDate < end && b.EndDate > start))
                .ToList();
        }

        public Room? GetRoomById(int id)
        {
            return _context.Rooms.Include(r => r.Bookings).FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Room> GetRoomsByHotel(int hotelId)
        {
            return _context.Rooms.Where(r => r.HotelId == hotelId).ToList();
        }

        public void AddRoom(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        public void DeleteRoom(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                _context.SaveChanges();
            }
        }
    }
//}
