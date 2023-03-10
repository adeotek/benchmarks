using MediatR;

namespace MinimalAPIWithMediatR;

public class GetWeatherForecastRequest : IRequest<IResult>
{
    public int? Days { get; set; }
}
