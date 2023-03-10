using Microsoft.Extensions.Logging;

namespace WeatherForecastProvider;

public class WeatherForecastService : IWeatherForecastService
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    private readonly ILogger<WeatherForecastService> _logger;

    public WeatherForecastService(ILogger<WeatherForecastService> logger)
    {
        _logger = logger;
    }

    public async Task<IEnumerable<WeatherForecastModel>> GetForecast(int days)
    {
        _logger.LogInformation("Get weather forecast for the next {Days} days", days.ToString());
        await Task.Delay(20);
        return Enumerable.Range(1, days).Select(index => new WeatherForecastModel
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
            .ToArray();
    }
}
