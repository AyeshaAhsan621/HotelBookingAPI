public class BookingDto
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public string CustomerName { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
}
