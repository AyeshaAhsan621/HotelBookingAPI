//using HotelBookingAPI.Models;

public class Room
{
    public int Id { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public RoomType RoomType { get; set; }
    public int Capacity { get; set; }

    public int HotelId { get; set; }
    public Hotel Hotel { get; set; } = null!;

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
