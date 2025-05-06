using IocMapper.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace IocMapper.UnitTests.Mediator.Mocks
{
    public class AddRequest(int valueA, int valueB) : IRequest<int>
    {
        public int ValueA => valueA;

        public int ValueB => valueB;

        public class Handler : IRequestHandler<AddRequest, int>
        {
            public Task<int> Handle(AddRequest request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult(request.ValueA + request.ValueB);
            }
        }
    }
}
