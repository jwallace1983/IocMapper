using IocMapper.Mediator;
using IocMapper.Reflectors;
using System;

// Namespace does not match folder structure
#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection
#pragma warning restore IDE0130
{
    public static class Bootstrap
    {
        public static IServiceCollection AddIocMappings(this IServiceCollection services, params Type[] sources)
        {
            new IocReflector(sources).AddMappings(services);
            return services;
        }

        public static IServiceCollection AddMediator(this IServiceCollection services, params Type[] sources)
        {
            var reflector = new MediatorReflector(sources);
            reflector.AddMappings(services);
            services.AddSingleton<IMediator, Mediator>();
            services.AddSingleton<IMediatorReflector>(reflector);
            return services;
        }
    }
}
