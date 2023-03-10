using MediatR;
using MinimalAPIWithMediatR;
using Serilog;
using WeatherForecastProvider;

var builder = WebApplication.CreateBuilder(args);

// Add Serilog support
builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

// Add services to the container.
builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();

builder.Services.AddMediatR(x =>
{
    x.Lifetime = ServiceLifetime.Scoped;
    x.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

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

app.MapGet("/weatherforecast", async (IMediator mediator, [AsParameters] GetWeatherForecastRequest request)
    => await mediator.Send(request))
    .WithName(typeof(GetWeatherForecastRequest).Name)
    .WithOpenApi();

app.Run();
