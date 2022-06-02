using IocMapper;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IocExtensions
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
    }
}
