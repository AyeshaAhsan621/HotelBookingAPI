using AutoMapper;
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Hotel ↔ HotelDto
            CreateMap<Hotel, HotelDto>().ReverseMap();

            // Room ↔ RoomDto
            CreateMap<Room, RoomDto>().ReverseMap();

            // Booking ↔ BookingDto
            CreateMap<Booking, BookingDto>().ReverseMap();

            // BookingRequestDto → Booking (only one-way)
            CreateMap<BookingRequestDto, Booking>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
               // .ForMember(dest => dest.Reference, opt => opt.Ignore())
                .ForMember(dest => dest.Room, opt => opt.Ignore());
        }
    }

