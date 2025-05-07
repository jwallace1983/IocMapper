using IocMapper.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace IocMapper.UnitTests.Mediator.Mocks
{
    public class SimpleRequest : IRequest
    {
        public int Counter { get; set; } = 0;

        public class Handler : IRequestHandler<SimpleRequest>
        {
            public Task Handle(SimpleRequest request, CancellationToken cancellationToken)
            {
                request.Counter++;
                return Task.CompletedTask;
            }
        }
    }
}
