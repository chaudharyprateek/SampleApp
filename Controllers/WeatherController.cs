using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SampleApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var rng = new Random();
            var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

            var forecasts = Enumerable.Range(1, 5).Select(index => new
            {
                Date = DateTime.Now.AddDays(index).ToString("yyyy-MM-dd"),
                TemperatureC = rng.Next(-20, 55),
                Summary = summaries[rng.Next(summaries.Length)]
            });

            return Ok(forecasts);
        }
    }
}
