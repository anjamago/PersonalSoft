using Microsoft.AspNetCore.Mvc;
using Entities.Interface.Repositories;
using Entities.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly  IDbMongo<Book> _mongo;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IDbMongo<Book>  mongo)
    {
        _logger = logger;
        _mongo = mongo;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpGet("/mongoConsulta")]
    public async Task<dynamic> GetBook()
    {
        var book = typeof(Book).Name;
        var list = await _mongo.GetAsync();
        return list;
    }
}