using IocMapper.Mediator;
using IocMapper.UnitTests.Mediator.Mocks;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace IocMapper.UnitTests.Mediator
{
    public class MediatorTest
    {
        [Fact]
        public async Task SendWithNoResponseTest()
        {
            // Arrange
            var services = new ServiceCollection()
                .AddMediator()
                .BuildServiceProvider();
            var request = new SimpleRequest();
            var mediator = services.GetRequiredService<IMediator>();

            // Act
            await mediator.Send(request);

            // Assert
            request.Counter.ShouldBe(1);
        }

        [Fact]
        public async Task SendWithResponseTest()
        {
            // Arrange
            var services = new ServiceCollection()
                .AddMediator()
                .BuildServiceProvider();
            var request = new AddRequest(1, 2);
            var mediator = services.GetRequiredService<IMediator>();

            // Act
            var result = await mediator.Send(request);

            // Assert
            result.ShouldBe(3);
        }
    }
}
