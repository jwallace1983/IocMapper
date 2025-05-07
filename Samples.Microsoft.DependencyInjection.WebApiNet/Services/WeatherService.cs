using IocMapper;
using Samples.Microsoft.DependencyInjection.WebApiNet6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.Microsoft.DependencyInjection.WebApiNet6.Services
{
    [Ioc(Lifetimes.Scoped)]
    public class WeatherService : IWeatherService
    {
        public async Task<IEnumerable<WeatherForecast>> GetForecasts()
        {
            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
            });
            return await Task.FromResult(forecasts);
        }
    }
}
