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
    public ActionResult<IEnumerable<WeatherForecast>> Get()
    {
        IEnumerable<WeatherForecast> res = useCase.GetAllData();
        Console.WriteLine("res: {0}",res);
        return (OkObjectResult)Ok(res);
    }
}
