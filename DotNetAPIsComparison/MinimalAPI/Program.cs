using Serilog;
using WeatherForecastProvider;

var builder = WebApplication.CreateBuilder(args);

// Add Serilog support
builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

// Add services to the container.
builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use Serilog for web server internal logging
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", async (IWeatherForecastService weatherForecastService, [AsParameters] int days) =>
{
    if (days < 0)
    {
        return Results.BadRequest($"Invalid value [{days}] for parameter days!");
    }

    return Results.Ok(await weatherForecastService.GetForecast(days == 0 ? 1 : days));
})
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();
