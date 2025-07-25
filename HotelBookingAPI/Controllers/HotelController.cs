using AutoMapper;

using Microsoft.AspNetCore.Mvc;


    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly HotelService _hotelService;
        private readonly IMapper _mapper;

        public HotelController(HotelService hotelService, IMapper mapper)
        {
            _hotelService = hotelService;
            _mapper = mapper;
        }

        [HttpGet("{name}")]
        public IActionResult GetHotelByName(string name)
        {
            var hotel = _hotelService.GetHotelByName(name);
            if (hotel == null)
                return NotFound(new { error = $"Hotel '{name}' not found." });

            var dto = _mapper.Map<HotelDto>(hotel);
            return Ok(dto);
        }

        [HttpGet]
        public IActionResult GetAllHotels()
        {
            var hotels = _hotelService.GetAllHotels();
            var dtoList = _mapper.Map<List<HotelDto>>(hotels);
            return Ok(dtoList);
        }

        [HttpPost]
        public IActionResult AddHotel([FromBody] HotelDto hotelDto)
        {
            var hotel = _mapper.Map<Hotel>(hotelDto);
            _hotelService.AddHotel(hotel);
            var resultDto = _mapper.Map<HotelDto>(hotel);
            return CreatedAtAction(nameof(GetHotelByName), new { name = hotel.Name }, resultDto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHotel(int id)
        {
            var existing = _hotelService.GetAllHotels().FirstOrDefault(h => h.Id == id);
            if (existing == null)
                return NotFound(new { error = $"Hotel with ID {id} not found." });

            _hotelService.DeleteHotel(id);
            return NoContent();
        }
    }

