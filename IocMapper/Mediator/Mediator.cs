using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace IocMapper.Mediator
{
    public class Mediator(IServiceProvider services) : IMediator
    {
        private readonly IServiceProvider _services = services;

        private readonly Dictionary<string, MediatorMapping> _mappings = MediatorReflector.GetMappings();

        public async Task Send<TRequestType>(
            TRequestType request,
            CancellationToken cancellationToken = default)
            where TRequestType : IRequest
        {
            var key = typeof(TRequestType).GetMediatorMappingKey();
            var handlerType = (_mappings.TryGetValue(key, out var mapping) ? mapping.HandlerType : null)
                ?? throw new MediatorMappingException();
            var handler = _services.GetRequiredService(handlerType);
            var method = handlerType.GetMethod("Handle", BindingFlags.Public | BindingFlags.Instance,
                [ typeof(TRequestType), typeof(CancellationToken) ])
                ?? throw new MediatorMappingException();
            await (Task)method.Invoke(handler, [ request, cancellationToken ]);
        }

        public async Task<TResponseType> Send<TResponseType>(
            IRequest<TResponseType> request,
            CancellationToken cancellationToken = default)
        {
            var requestType = request.GetType();
            var key = requestType.GetMediatorMappingKey();
            var handlerType = (_mappings.TryGetValue(key, out var mapping) ? mapping.HandlerType : null)
                ?? throw new MediatorMappingException();
            var handler = _services.GetRequiredService(handlerType);
            var method = handlerType.GetMethod("Handle", BindingFlags.Public | BindingFlags.Instance,
                [requestType, typeof(CancellationToken)])
                ?? throw new MediatorMappingException();
            return await (Task<TResponseType>)method.Invoke(handler, [request, cancellationToken]);
        }
    }
}
