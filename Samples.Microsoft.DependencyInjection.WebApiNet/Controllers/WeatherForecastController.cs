using Microsoft.AspNetCore.Mvc;
using Samples.Microsoft.DependencyInjection.WebApiNet6.Services;
using System.Threading.Tasks;

namespace Samples.Microsoft.DependencyInjection.WebApiNet6.Controllers
{
    [ApiController, Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _weatherService.GetForecasts();
            return this.Ok(results);
        }
    }
}