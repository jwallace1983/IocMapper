using System.Threading;
using System.Threading.Tasks;

namespace IocMapper.Mediator
{
    public interface IRequestHandler<TRequestType, TResponseType>
        where TRequestType : IRequest<TResponseType>
    {
        Task<TResponseType> Handle(TRequestType request, CancellationToken cancellationToken = default);
    }
}
