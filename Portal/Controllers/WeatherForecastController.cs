using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Services;

namespace Portal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherService _weatherService;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            WeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }
        [Authorize]
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Authorize]
        [HttpGet("Current")]
        public async Task<IActionResult> GetCurrentWeather(
            [FromQuery] double lat,
            [FromQuery] double lon)
        {
            try
            {
                var weather = await _weatherService.GetCurrentWeatherAsync(lat, lon);
                return Ok(weather);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the weather data.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

    }
}
