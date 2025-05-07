using Microsoft.AspNetCore.Mvc;
using Samples.Microsoft.DependencyInjection.WebApiNet.Services;
using System.Threading.Tasks;

namespace Samples.Microsoft.DependencyInjection.WebApiNet.Controllers
{
    [ApiController, Route("[controller]")]
    public class WeatherForecastController(IWeatherService weatherService) : ControllerBase
    {
        private readonly IWeatherService _weatherService = weatherService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _weatherService.GetForecasts();
            return this.Ok(results);
        }
    }
}