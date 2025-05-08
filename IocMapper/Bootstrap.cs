using IocMapper.Mediator;
using IocMapper.Reflectors;
using System;
using System.Linq;
using System.Reflection;

// Namespace does not match folder structure
#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection
#pragma warning restore IDE0130
{
    public static class Bootstrap
    {
        public static IServiceCollection AddIocMappings(this IServiceCollection services, params Type[] sources)
        {
            var types = sources.Length > 0 ? sources
                : [Assembly.GetCallingAssembly().DefinedTypes.First()];
            new IocReflector(types).AddMappings(services);
            var mediatorReflector = new MediatorReflector(types);
            mediatorReflector.AddMappings(services);
            services
                .AddSingleton<IMediator, Mediator>()
                .AddSingleton<IMediatorReflector>(mediatorReflector);
            return services;
        }
    }
}
