using IocMapper.Mediator;
using Samples.Microsoft.DependencyInjection.WebApiNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Samples.Microsoft.DependencyInjection.WebApiNet.Services.Requests
{
    public class GetForecast : IRequest<IEnumerable<WeatherForecast>>
    {
        public class Handler : IRequestHandler<GetForecast, IEnumerable<WeatherForecast>>
        {
            public Task<IEnumerable<WeatherForecast>> Handle(GetForecast request, CancellationToken cancellationToken)
            {
                return Task.FromResult(
                    Enumerable.Range(1, 5).Select(index => new WeatherForecast
                    {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = Random.Shared.Next(-20, 55),
                    }));
            }
        }
    }
}
