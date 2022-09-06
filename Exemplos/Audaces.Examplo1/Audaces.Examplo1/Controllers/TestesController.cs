using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Audaces.Examplo1.Controllers;

[ApiController]
[Route("[controller]")]
public class TestesController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<TestesController> _logger;
    private readonly IStringLocalizer<SharedResourcx> _stringLocalizer;

    public TestesController(ILogger<TestesController> logger, IStringLocalizer<SharedResourcx> stringLocalizer)
    {
        _logger = logger;
        _stringLocalizer = stringLocalizer;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray();
    }

    [HttpGet("accept-language")]
    public string GetLocalized()
    {
        return $"{_stringLocalizer.GetString("HelloWord")} em {DateTime.Now}";
    }
}