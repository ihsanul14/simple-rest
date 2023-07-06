using Microsoft.AspNetCore.Mvc;
using simple_rest.usecase;

namespace simple_rest.application.controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IUsecase useCase;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IUsecase usecase)
    {
        _logger = logger;
        useCase = usecase;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        // return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        // {
        //     Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)), 
        //     TemperatureC = Random.Shared.Next(-20, 55),
        //     Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        // })
        // .ToArray();

        IEnumerable<WeatherForecast> res = useCase.GetAllData();
        return (IEnumerable<WeatherForecast>)Ok(res);
    }
}
