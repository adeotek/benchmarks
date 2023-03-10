using Microsoft.AspNetCore.Mvc;
using WeatherForecastProvider;

namespace ControllersAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IWeatherForecastService weatherForecastService, ILogger<WeatherForecastController> logger)
        {
            _weatherForecastService = weatherForecastService;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get([FromQuery] int? days)
        {
            if (days is not null && days < 1)
            {
                return BadRequest($"Invalid value [{days}] for parameter days!");
            }
            return Ok(await _weatherForecastService.GetForecast(days ?? 1));
        }
    }
}