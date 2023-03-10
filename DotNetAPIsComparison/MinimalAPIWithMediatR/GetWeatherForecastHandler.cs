using MediatR;
using WeatherForecastProvider;

namespace MinimalAPIWithMediatR;

public class GetWeatherForecastHandler : IRequestHandler<GetWeatherForecastRequest, IResult>
{
    private readonly IWeatherForecastService _weatherForecastService;

    public GetWeatherForecastHandler(IWeatherForecastService weatherForecastService)
    {
        _weatherForecastService = weatherForecastService;
    }

    public async Task<IResult> Handle(GetWeatherForecastRequest request, CancellationToken cancellationToken)
    {
        if (request.Days is not null &&  request.Days < 1)
        {
            return Results.BadRequest($"Invalid value [{request.Days}] for parameter days!");
        }
        return Results.Ok(await _weatherForecastService.GetForecast(request.Days ?? 1));
    }
}
