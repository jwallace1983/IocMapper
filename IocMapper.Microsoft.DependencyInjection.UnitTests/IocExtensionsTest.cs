using IocMapper.Microsoft.DependencyInjection.UnitTests.Samples;
using Microsoft.Extensions.DependencyInjection;

namespace IocMapper.Microsoft.DependencyInjection.UnitTests
{
    public class IocExtensionsTest
    {
        [Fact]
        public void AddIocMappingsTest()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddIocMappings();

            // Assert
            services.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void SelfReferenceTest()
        {
            // Arrange
            var target = new ServiceCollection()
                .AddIocMappings().BuildServiceProvider();

            // Act
            var settings = target.GetService<Settings>();

            // Assert
            settings.Should().NotBeNull();
        }

        [Fact]
        public void LifetimeTest_()
        {
            // Arrange
            var target = new ServiceCollection()
                .AddIocMappings().BuildServiceProvider();
            var settings1 = target.GetService<Settings>();
            var localService1 = target.GetService<ILocalService>();

            // Act
            settings1.Counter++;
            localService1.Counter++;
            var settings2 = target.GetService<Settings>();
            var localService2 = target.GetService<ILocalService>();

            // Assert
            settings1.Counter.Should().Be(1);
            settings2.Counter.Should().Be(1);
            localService1.Counter.Should().Be(1);
            localService2.Counter.Should().Be(0);
        }

    }
}
