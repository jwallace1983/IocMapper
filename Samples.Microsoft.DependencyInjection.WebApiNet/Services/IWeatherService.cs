using Samples.Microsoft.DependencyInjection.WebApiNet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Samples.Microsoft.DependencyInjection.WebApiNet.Services
{
    public interface IWeatherService
    {
        Task<IEnumerable<WeatherForecast>> GetForecasts();
    }
}