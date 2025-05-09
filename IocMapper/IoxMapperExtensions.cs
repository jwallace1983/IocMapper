using IocMapper.Mediator;
using IocMapper.Reflectors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

// Namespace does not match folder structure
namespace IocMapper
{
    public static class IocMapperExtensions
    {
        public static IServiceCollection AddIocMappings(this IServiceCollection services, params Type[] sources)
        {
            var types = sources.Length > 0 ? sources
                : [Assembly.GetCallingAssembly().DefinedTypes.First()];
            new IocReflector(types).AddMappings(services);
            var mediatorReflector = new MediatorReflector(types);
            mediatorReflector.AddMappings(services);
            services
                .AddSingleton<IMediator, Mediator.Mediator>()
                .AddSingleton<IMediatorReflector>(mediatorReflector);
            return services;
        }
    }
}
