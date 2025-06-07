using IocMapper.Reflectors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace IocMapper.Mediator
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _services;

        private readonly IMediatorReflector _reflector;

        public Mediator(IServiceProvider services, IMediatorReflector reflector)
        {
            _services = services;
            _reflector = reflector;
        }

        public async Task Send<TRequestType>(
            TRequestType request,
            CancellationToken cancellationToken = default)
            where TRequestType : IRequest
        {
            var key = _reflector.GetMediatorMappingKey(typeof(TRequestType));
            var handlerType = (_reflector.Mappings.TryGetValue(key, out var mapping) ? mapping.HandlerType : null)
                ?? throw new MappingException(typeof(TRequestType), "No handler found");
            var handler = _services.GetRequiredService(handlerType);
            var types = new Type[] { typeof(TRequestType), typeof(CancellationToken) };
            var method = handlerType.GetMethod("Handle", types)
                ?? throw new MappingException(typeof(TRequestType), "No handle method found");
            await (Task)method.Invoke(handler, new object[] { request, cancellationToken });
        }

        public async Task<TResponseType> Send<TResponseType>(
            IRequest<TResponseType> request,
            CancellationToken cancellationToken = default)
        {
            var requestType = request.GetType();
            var key = _reflector.GetMediatorMappingKey(requestType);
            var handlerType = (_reflector.Mappings.TryGetValue(key, out var mapping) ? mapping.HandlerType : null)
                ?? throw new MappingException(typeof(IRequest<TResponseType>), "No handler found");
            var handler = _services.GetRequiredService(handlerType);
            var method = handlerType.GetMethod("Handle", new Type[] { requestType, typeof(CancellationToken) })
                ?? throw new MappingException(typeof(IRequest<TResponseType>), "No handle method found");
            return await (Task<TResponseType>)method.Invoke(handler, new object[] { request, cancellationToken });
        }
    }
}
