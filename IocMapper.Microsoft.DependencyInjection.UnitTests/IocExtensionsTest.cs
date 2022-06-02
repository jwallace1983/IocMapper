using IocMapper.Microsoft.DependencyInjection.UnitTests.ExternalLibrary;
using IocMapper.Microsoft.DependencyInjection.UnitTests.Samples;
using Microsoft.Extensions.DependencyInjection;
using System;

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
            services.AddIocMappings(typeof(IExternalService));

            // Assert
            services.Should().NotBeNullOrEmpty();
        }

        [Theory]
        [InlineData(typeof(ILocalService))] // Calling assembly
        [InlineData(typeof(IExternalService))] // External library included
        [InlineData(typeof(Settings))] // Self-reference
        public void GetServiceTest(Type type)
        {
            // Arrange
            var target = new ServiceCollection()
                .AddIocMappings(typeof(IExternalService))
                .BuildServiceProvider();

            // Act
            var service = target.GetService(type);

            // Assert
            service.Should().NotBeNull();
        }

        [Theory]
        [InlineData(typeof(ILifetimeSingletonService), 1)]
        [InlineData(typeof(ILifetimeTransientService), 0)]
        [InlineData(typeof(ILifetimeScopedService), 1)]
        public void LifetimesTest(Type type, int expected)
        {
            // Arrange
            var target = new ServiceCollection()
                .AddIocMappings().BuildServiceProvider();

            // Act
            var service = target.GetService(type) as LifetimeServiceBase;
            service.Counter++;
            var service2 = target.GetService(type) as LifetimeServiceBase;

            // Assert
            service.Counter.Should().Be(1);
            service2.Counter.Should().Be(expected);
        }

    }
}
