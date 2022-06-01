using Samples.Microsoft.DependencyInjection.WebApiNet6.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Samples.Microsoft.DependencyInjection.WebApiNet6.Services
{
    public interface IWeatherService
    {
        Task<IEnumerable<WeatherForecast>> GetForecasts();
    }
}