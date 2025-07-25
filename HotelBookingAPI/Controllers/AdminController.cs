using Microsoft.AspNetCore.Mvc;
using System;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("seed")]
    public IActionResult Seed()
    {
        Reset();
        var hotel = new Hotel { Name = "Test Hotel", Address = "123 Main St" };
        _context.Hotels.Add(hotel);
        _context.SaveChanges();

        var rooms = new List<Room>
        {
            new() { RoomNumber = "101", RoomType = RoomType.Single, Capacity = 1, HotelId = hotel.Id },
            new() { RoomNumber = "102", RoomType = RoomType.Single, Capacity = 1, HotelId = hotel.Id },
            new() { RoomNumber = "201", RoomType = RoomType.Double, Capacity = 2, HotelId = hotel.Id },
            new() { RoomNumber = "202", RoomType = RoomType.Double, Capacity = 2, HotelId = hotel.Id },
            new() { RoomNumber = "301", RoomType = RoomType.Deluxe, Capacity = 4, HotelId = hotel.Id },
            new() { RoomNumber = "302", RoomType = RoomType.Deluxe, Capacity = 4, HotelId = hotel.Id }
        };

        _context.Rooms.AddRange(rooms);
        _context.SaveChanges();

        return Ok("Seeded successfully.");
    }

    [HttpPost("reset")]
    public IActionResult Reset()
    {
        _context.Bookings.RemoveRange(_context.Bookings);
        _context.Rooms.RemoveRange(_context.Rooms);
        _context.Hotels.RemoveRange(_context.Hotels);
        _context.SaveChanges();

        return Ok("Reset successfully.");
    }
}
