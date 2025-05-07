using IocMapper.UnitTests.Reflectors.Mocks;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IocMapper.UnitTests.Reflectors
{
    public class IocReflectorTest
    {
        [Theory]
        [InlineData(typeof(IDefaultService), true)]
        [InlineData(typeof(ILifetimeService), true)]
        [InlineData(typeof(IMultiService), true)]
        [InlineData(typeof(Settings), true)]
        [InlineData(typeof(NoInterfaceService), false)] // Not registered
        public void MappingTest(Type type, bool expectValue)
        {
            // Arrange
            var services = new ServiceCollection()
                .AddIocMappings(type)
                .BuildServiceProvider();

            // Act
            var result = services.GetService(type);

            // Assert
            if (expectValue)
                result.ShouldNotBeNull();
            else
                result.ShouldBeNull();
        }
    }
}
