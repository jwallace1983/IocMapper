using IocMapper;
using IocMapper.Mediator;
using Samples.Microsoft.DependencyInjection.WebApiNet.Models;
using Samples.Microsoft.DependencyInjection.WebApiNet.Services.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Samples.Microsoft.DependencyInjection.WebApiNet.Services
{
    [Ioc(Lifetimes.Scoped)]
    public class WeatherService (IMediator mediator) : IWeatherService
    {
        private readonly IMediator _mediator = mediator;

        public async Task<IEnumerable<WeatherForecast>> GetForecasts()
        {
            var forecasts = await _mediator.Send(new GetForecast());
            return forecasts;
        }
    }
}
