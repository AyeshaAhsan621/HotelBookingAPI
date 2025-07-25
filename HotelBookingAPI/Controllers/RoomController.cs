using AutoMapper;

using Microsoft.AspNetCore.Mvc;


    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly RoomService _roomService;
        private readonly IMapper _mapper;

        public RoomController(RoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }

        [HttpGet("available")]
        public IActionResult GetAvailableRooms(DateTime start, DateTime end, int guests)
        {
            if (start >= end)
                return BadRequest(new { error = "Start date must be before end date." });

            if (guests <= 0)
                return BadRequest(new { error = "Guest count must be greater than zero." });

            var rooms = _roomService.GetAvailableRooms(start, end, guests);
            var dtoList = _mapper.Map<List<RoomDto>>(rooms);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public IActionResult GetRoomById(int id)
        {
            var room = _roomService.GetRoomById(id);
            if (room == null)
                return NotFound(new { error = $"Room with ID {id} not found." });

            var dto = _mapper.Map<RoomDto>(room);
            return Ok(dto);
        }

        [HttpGet("hotel/{hotelId}")]
        public IActionResult GetRoomsByHotel(int hotelId)
        {
            var rooms = _roomService.GetRoomsByHotel(hotelId);
            var dtoList = _mapper.Map<List<RoomDto>>(rooms);
            return Ok(dtoList);
        }

        [HttpPost]
        public IActionResult AddRoom([FromBody] RoomDto roomDto)
        {
            var room = _mapper.Map<Room>(roomDto);
            _roomService.AddRoom(room);
            var resultDto = _mapper.Map<RoomDto>(room);
            return CreatedAtAction(nameof(GetRoomById), new { id = room.Id }, resultDto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var existing = _roomService.GetRoomById(id);
            if (existing == null)
                return NotFound(new { error = $"Room with ID {id} not found." });

            _roomService.DeleteRoom(id);
            return NoContent();
        }
    }

