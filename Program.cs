using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);



// Register services
builder.Services.AddControllers().AddApplicationPart(typeof(WeatherController).Assembly);
builder.Services.AddSingleton<IWeatherService, WeatherService>();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/", () => "Hello from .NET Practice API!");
app.MapControllers();


//use Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();

// ----------------------------
// Weather Controller
// ----------------------------

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet]
    public IActionResult GetWeather()
    {
        return Ok(_weatherService.GetForecast());
    }
}

// ----------------------------
// Weather Service
// ----------------------------

public interface IWeatherService
{
    IEnumerable<string> GetForecast();
}

public class WeatherService : IWeatherService
{
    public IEnumerable<string> GetForecast()
    {
        return new List<string> { "Sunny", "Cloudy", "Rainy" };
    }
}
