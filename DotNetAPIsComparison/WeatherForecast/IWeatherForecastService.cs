namespace WeatherForecastProvider;

public interface IWeatherForecastService
{
    Task<IEnumerable<WeatherForecastModel>> GetForecast(int days);
}
