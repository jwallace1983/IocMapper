using IocMapper;
using IocMapper.Mediator;
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
            // Add calling assembly
            Reflector.AddAssembly(Assembly.GetCallingAssembly().DefinedTypes.FirstOrDefault());

            // Add provided assembly
            foreach (var source in sources)
                Reflector.AddAssembly(source);

            // Add the mappings
            var mappings = Reflector.GetMappings();
            foreach (var m in mappings)
            {
                if (m.Lifetime == Lifetimes.Transient)
                    services.AddTransient(m.Service, m.Implementation);
                else if (m.Lifetime == Lifetimes.Scoped)
                    services.AddScoped(m.Service, m.Implementation);
                else if (m.Lifetime == Lifetimes.Singleton)
                    services.AddSingleton(m.Service, m.Implementation);
            }

            // Return fluent reference
            return services;
        }

        public static IServiceCollection AddMediator(this IServiceCollection services, params Type[] sources)
        {
            services.AddSingleton<IMediator, Mediator>();
            MediatorReflector.AddAssembly(Assembly.GetCallingAssembly().DefinedTypes.FirstOrDefault());
            foreach (var source in sources)
                MediatorReflector.AddAssembly(source);
            var mappings = MediatorReflector.GetMappings();
            foreach (var m in mappings)
                services.AddTransient(m.Value.HandlerType); // Add handler
            return services;
        }
    }
}
