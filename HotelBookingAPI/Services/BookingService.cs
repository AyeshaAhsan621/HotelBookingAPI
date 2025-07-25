using Microsoft.EntityFrameworkCore;

public class BookingService
{
    private readonly AppDbContext _context;

    public BookingService(AppDbContext context)
    {
        _context = context;
    }

    public bool IsRoomAvailable(int roomId, DateTime start, DateTime end)
    {
        return !_context.Bookings.Any(b =>
            b.RoomId == roomId &&
            b.StartDate < end &&
            b.EndDate > start);
    }

    public ServiceResult<Booking> CreateBooking(int roomId, DateTime start, DateTime end, int guests)
    {
        if (start >= end)
            return ServiceResult<Booking>.Fail("Start date must be before end date.");

        var room = _context.Rooms.Find(roomId);
        if (room == null)
            return ServiceResult<Booking>.Fail("Room not found.");

        if (guests > room.Capacity)
            return ServiceResult<Booking>.Fail("Guest count exceeds room capacity.");

        if (!IsRoomAvailable(roomId, start, end))
            return ServiceResult<Booking>.Fail("Room is not available for the selected dates.");

        var booking = new Booking
        {
            RoomId = roomId,
            StartDate = start,
            EndDate = end,
            GuestCount = guests
        };

        _context.Bookings.Add(booking);
        _context.SaveChanges();
        return ServiceResult<Booking>.Ok(booking);
    }

    public Booking? GetBookingByRef(string bookingRef)
    {
        return _context.Bookings
            .Include(b => b.Room)
            .ThenInclude(r => r.Hotel)
            .FirstOrDefault(b => b.BookingRef == bookingRef);
    }
}
