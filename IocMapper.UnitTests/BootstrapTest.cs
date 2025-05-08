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
            var mediator = services.GetService<IMediator>();

            // Assert
            defaultService.ShouldNotBeNull();
            mediator.ShouldNotBeNull();
        }
    }
}
