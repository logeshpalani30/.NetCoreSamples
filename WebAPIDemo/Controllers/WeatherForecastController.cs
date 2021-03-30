using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using WebApi.Model;

namespace WebAPIDemo.Controllers
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
        private readonly ICheckPrimeOrNot _checkPrimeOrNot;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICheckPrimeOrNot checkPrimeOrNot)
        {
            _logger = logger;
            _checkPrimeOrNot = checkPrimeOrNot;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPost("checkPrime")]
        public IActionResult GetPrimeOrNot([FromBody] PrimeOrNotRequest request)
        {
            if (!string.IsNullOrEmpty(request.number.Trim()))
            {
                var primeResult = _checkPrimeOrNot.CheckPrimeOrNot(request.number);
                var result = new PrimeOrNotResponse()
                {
                    number = request.number,
                    PrimeOrNot = primeResult
                };

                return new OkObjectResult(result);
            }
            else
                return BadRequest("Entered Wrong Text");

        }
    }
}
