using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext
//builder.Services.AddDbContext<AppDbContext>(options =>
//options.UseInMemoryDatabase("HotelDb"));
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register services
builder.Services.AddScoped<HotelService>();
builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<BookingService>();

//builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new MappingProfile());
});

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Map routes
app.MapControllers();
app.Run();
