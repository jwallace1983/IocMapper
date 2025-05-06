using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IocMapper.Mediator
{
    public static class MediatorReflector
    {
        private static readonly string HANDLER_NAMESPACE = typeof(IRequestHandler<>).Namespace;
        private static readonly Type HANDLER_NO_RESPONSE = typeof(IRequestHandler<>);
        private static readonly Type HANDLER_WITH_RESPONSE = typeof(IRequestHandler<,>);

        public static IEnumerable<Assembly> Assemblies => _assemblies;
        private static readonly List<Assembly> _assemblies = [];

        public static void AddAssembly(Type type) => AddAssembly(type.Assembly);

        public static void AddAssembly(Assembly assembly)
        {
            if (_assemblies.Any(a => a.FullName.Equals(assembly.FullName)))
                return; // Guard: already added
            _assemblies.Add(assembly);
        }

        public static Dictionary<string, MediatorMapping> GetMappings()
        {
            var mappings = new Dictionary<string, MediatorMapping>();
            foreach (var assembly in _assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    var handlerType = GetMediatorMapping(type);
                    if (handlerType == null)
                        continue; // Not found
                    mappings[handlerType.RequestType.GetMediatorMappingKey()] = handlerType;
                }
            }
            return mappings;
        }

        public static MediatorMapping GetMediatorMapping(Type type)
        {
            if (!type.IsClass || type.IsAbstract)
                return null; // Must be a class
            foreach (var implementedInterface in type.GetInterfaces())
            {
                if (HANDLER_NAMESPACE.Equals(implementedInterface.Namespace) == false)
                    continue; // Not in right namespace
                else if (HANDLER_NO_RESPONSE.Name.Equals(implementedInterface.Name))
                    return new MediatorMapping(type, // Found, IRequest
                        implementedInterface.GenericTypeArguments[0]);
                else if (HANDLER_WITH_RESPONSE.Name.Equals(implementedInterface.Name))
                    return new MediatorMapping(type, // Found, IRequest<TResponseType>
                        implementedInterface.GenericTypeArguments[0],
                        implementedInterface.GenericTypeArguments[1]);
            }
            return null; // Otherwise, not found
        }

        public static string GetMediatorMappingKey(this Type type)
            => $"{type.Namespace}.{type.Name}";
    }
}
