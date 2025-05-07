using System.Threading;
using System.Threading.Tasks;

namespace IocMapper.Mediator
{
    public interface IRequestHandler<TRequestType>
        where TRequestType : IRequest
    {
        Task Handle(TRequestType request, CancellationToken cancellationToken);
    }
}
