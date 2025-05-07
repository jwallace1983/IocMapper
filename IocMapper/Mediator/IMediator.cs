using System.Threading;
using System.Threading.Tasks;

namespace IocMapper.Mediator
{
    public interface IMediator
    {
        Task Send<TRequestType>(
            TRequestType request,
            CancellationToken cancellationToken = default)
            where TRequestType : IRequest;

        Task<TResponseType> Send<TResponseType>(
            IRequest<TResponseType> request,
            CancellationToken cancellationToken = default);
    }
}