using IocMapper.Mediator;
using IocMapper.UnitTests.Reflectors.Mocks;
using Microsoft.Extensions.DependencyInjection;

namespace IocMapper.UnitTests
{
    public class BootstrapTest
    {
        [Fact]
        public void AddIocMappingsTest()
        {
            // Arrange
            var services = new ServiceCollection()
                .AddIocMappings(typeof(BootstrapTest))
                .BuildServiceProvider();

            // Act
            var defaultService = services.GetService<IDefaultService>();

            // Assert
            defaultService.ShouldNotBeNull();
        }

        [Fact]
        public void AddMediatorTest()
        {
            // Arrange
            var services = new ServiceCollection()
                .AddMediator(typeof(BootstrapTest))
                .BuildServiceProvider();

            // Act
            var mediator = services.GetService<IMediator>();

            // Assert
            mediator.ShouldNotBeNull();
        }
    }
}
