//using HotelBookingAPI.Data;
//using HotelBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

//namespace HotelBookingAPI.Services
//{
    public class HotelService
    {
        private readonly AppDbContext _context;

        public HotelService(AppDbContext context)
        {
            _context = context;
        }

        public Hotel? GetHotelByName(string name)
        {
            return _context.Hotels
                .Include(h => h.Rooms)
                 //.FirstOrDefault(h => h.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                 .FirstOrDefault(h => h.Name.ToLower() == name.ToLower());

    
    }

        public IEnumerable<Hotel> GetAllHotels()
        {
            return _context.Hotels.Include(h => h.Rooms).ToList();
        }

        public void AddHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            _context.SaveChanges();
        }

        public void DeleteHotel(int id)
        {
            var hotel = _context.Hotels.Find(id);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                _context.SaveChanges();
            }
        }
    }
//}
