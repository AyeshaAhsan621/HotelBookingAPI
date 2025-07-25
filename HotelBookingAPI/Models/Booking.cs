public class Booking
{
    public int Id { get; set; }
    public string BookingRef { get; set; } = Guid.NewGuid().ToString();
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int GuestCount { get; set; }

    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;
}
