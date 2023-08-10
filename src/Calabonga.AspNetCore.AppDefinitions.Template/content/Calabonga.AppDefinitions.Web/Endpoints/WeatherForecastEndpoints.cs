using Calabonga.AppDefinitions.Web.Entities;
using Calabonga.AspNetCore.AppDefinitions;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.AppDefinitions.Web.Endpoints;

/// <summary>
/// Endpoint definition for <see cref="WeatherForecast"/> entity.
/// </summary>
public class WeatherForecastEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app)
    {
        var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

        app.MapGet("/weatherforecast", ([FromServices] ILogger<WeatherForecastEndpoints> logger) =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                        new WeatherForecast
                        (
                            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                            Random.Shared.Next(-20, 55),
                            summaries[Random.Shared.Next(summaries.Length)]
                        ))
                    .ToArray();
                logger.LogInformation("WeatherForecast request execute at [{Time}].", DateTime.UtcNow);
                return forecast;
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();
    }
}