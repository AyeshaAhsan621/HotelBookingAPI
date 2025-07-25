using AutoMapper;

using Microsoft.AspNetCore.Mvc;


    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bookingService;
        private readonly IMapper _mapper;

        public BookingController(BookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult BookRoom([FromBody] BookingRequestDto request)
        {
            var result = _bookingService.CreateBooking(request.RoomId, request.StartDate, request.EndDate, request.GuestCount);

            if (!result.Success)
                return BadRequest(new { error = result.ErrorMessage });

            var dto = _mapper.Map<BookingDto>(result.Data);
            return Ok(dto);
        }

        [HttpGet("{ref}")]
        public IActionResult GetBooking(string @ref)
        {
            var booking = _bookingService.GetBookingByRef(@ref);
            if (booking == null)
                return NotFound(new { error = "Booking not found." });

            var dto = _mapper.Map<BookingDto>(booking);
            return Ok(dto);
        }
    }
