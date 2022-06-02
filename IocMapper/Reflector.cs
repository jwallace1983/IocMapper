using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IocMapper
{
    public static class Reflector
    {
        public static IEnumerable<Assembly> Assemblies => _assemblies;
        private static readonly List<Assembly> _assemblies = new List<Assembly>();

        public static void AddAssembly(Type type)
        {
            if (_assemblies.Any(a => a.FullName.Equals(type.Assembly.FullName)))
                return; // Guard: already added
            _assemblies.Add(type.Assembly);
        }

        public static IEnumerable<IocMapping> GetMappings()
        {
            foreach (var assembly in _assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    var attribute = type.GetCustomAttribute<IocAttribute>(false);
                    if (attribute != null)
                    {
                        yield return attribute.GetMapping(type);
                    }
                }
            }
        }

        public static IocMapping GetMapping(this IocAttribute attribute, Type implementationType)
        {
            var mapping = new IocMapping
            {
                Implementation = implementationType,
                Service = attribute.Target,
                Lifetime = attribute.Lifetime,
            };
            var interfaces = implementationType.GetInterfaces();
            if (mapping.Service == null)
            {
                if (interfaces.Length > 1)
                    throw new IocMappingException(mapping.Implementation, "Multiple interfaces found");
                else if (interfaces.Length == 0)
                    throw new IocMappingException(mapping.Implementation, "No interfaces found");
                mapping.Service = interfaces[0];
            }
            else if (interfaces.Any(m => m == mapping.Service) == false)
            {
                throw new IocMappingException(mapping.Implementation, "Target service not implemented");
            }    
            return mapping;
        }
    }
}
