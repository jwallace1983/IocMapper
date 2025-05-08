using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace IocMapper.Reflectors
{
    public class IocReflector(params Type[] types) : ReflectorBase(types)
    {
        public override void AddMappingFromType(IServiceCollection services, Type type)
        {
            var mapping = GetMapping(type);
            if (mapping == null)
                return; // No mapping found
            else if (mapping.Lifetime == Lifetimes.Transient)
                services.AddTransient(mapping.Service, mapping.Implementation);
            else if (mapping.Lifetime == Lifetimes.Scoped)
                services.AddScoped(mapping.Service, mapping.Implementation);
            else if (mapping.Lifetime == Lifetimes.Singleton)
                services.AddSingleton(mapping.Service, mapping.Implementation);
        }

        public static IocMapping GetMapping(Type type)
        {
            var attribute = type.GetCustomAttribute<IocAttribute>(false);
            if (attribute == null)
                return default;
            var mapping = new IocMapping(attribute.Target, type, attribute.Lifetime);
            var interfaces = type.GetInterfaces();
            if (mapping.Service == null)
            {
                if (interfaces.Length > 1)
                    throw new MappingException(mapping.Implementation, "Multiple interfaces found");
                else if (interfaces.Length == 0)
                    throw new MappingException(mapping.Implementation, "No interfaces found");
                mapping.Service = interfaces[0];
            }
            else if (mapping.Service != type
                && interfaces.Any(m => m == mapping.Service) == false)
            {
                throw new MappingException(mapping.Implementation, "Target service not implemented");
            }
            return mapping;

        }
    }
}
