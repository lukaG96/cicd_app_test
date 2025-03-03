using Microsoft.AspNetCore.Mvc;

namespace cicd_app_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        // New GET method for testing purposes
        [HttpGet("citiesweather", Name = "GetCitiesWeather")]
        public IEnumerable<CityWeatherForecast> GetCitiesWeather()
        {
            var cities = new[]
            {
                new { City = "New York", TemperatureC = Random.Shared.Next(-20, 55), Summary = Summaries[Random.Shared.Next(Summaries.Length)] },
                new { City = "Los Angeles", TemperatureC = Random.Shared.Next(-20, 55), Summary = Summaries[Random.Shared.Next(Summaries.Length)] },
                new { City = "Chicago", TemperatureC = Random.Shared.Next(-20, 55), Summary = Summaries[Random.Shared.Next(Summaries.Length)] },
                new { City = "Miami", TemperatureC = Random.Shared.Next(-20, 55), Summary = Summaries[Random.Shared.Next(Summaries.Length)] }
            };

            return cities.Select(city => new CityWeatherForecast
            {
                City = city.City,
                TemperatureC = city.TemperatureC,
                Summary = city.Summary
            }).ToArray();
        }
    }

    // New class to represent city weather forecasts
    public class CityWeatherForecast
    {
        public string City { get; set; }
        public int TemperatureC { get; set; }
        public string Summary { get; set; }
    }
}
