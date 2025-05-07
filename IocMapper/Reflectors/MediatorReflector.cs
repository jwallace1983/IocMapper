using IocMapper.Mediator;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace IocMapper.Reflectors
{
    public class MediatorReflector : ReflectorBase, IMediatorReflector
    {
        private static readonly string HANDLER_NAMESPACE = typeof(IRequestHandler<>).Namespace;

        private static readonly Type HANDLER_NO_RESPONSE = typeof(IRequestHandler<>);

        private static readonly Type HANDLER_WITH_RESPONSE = typeof(IRequestHandler<,>);

        public Dictionary<string, MediatorMapping> Mappings { get; } = [];

        public MediatorReflector(params Type[] types) : base(types) { }

        public MediatorReflector(params Assembly[] assemblies) : base(assemblies) { }

        public override void AddMappingFromType(IServiceCollection services, Type type)
        {
            var mapping = GetMediatorMapping(type);
            if (mapping != null)
            {
                var mappingKey = this.GetMediatorMappingKey(mapping.RequestType);
                Mappings[mappingKey] = mapping;
                services.AddTransient(mapping.HandlerType);
            }
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

        public string GetMediatorMappingKey(Type type)
            => $"{type.Namespace}.{type.Name}";
    }
}
