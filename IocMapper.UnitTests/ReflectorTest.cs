using IocMapper.UnitTests.Mocks;
using System;
using System.Linq;

namespace IocMapper.UnitTests
{
    public class ReflectorTest
    {
        [Fact]
        public void AssembliesTest()
        {
            // Arrange
            // Act
            var results = Reflector.Assemblies;

            // Assert
            results.ShouldNotBeNull();
        }

        [Fact]
        public void AddAssemblyTest()
        {
            // Arrange
            // Act
            Reflector.AddAssembly(typeof(ReflectorTest));
            Reflector.AddAssembly(typeof(LifetimeService).Assembly);
            Reflector.AddAssembly(typeof(IDefaultService));

            // Assert
            Reflector.Assemblies.Count().ShouldBe(1);
        }

        [Fact]
        public void GetMappingsTest()
        {
            // Arrange
            Reflector.AddAssembly(typeof(IDefaultService));

            // Act
            var results = Reflector.GetMappings();

            // Assert
            results.ShouldContain(m => m.Service == typeof(IDefaultService)
                && m.Implementation == typeof(DefaultService)
                && m.Lifetime == Lifetimes.Transient);
            results.ShouldContain(m => m.Service == typeof(ILifetimeService)
                && m.Implementation == typeof(LifetimeService)
                && m.Lifetime == Lifetimes.Singleton);
            results.ShouldContain(m => m.Service == typeof(IMultiService)
                && m.Implementation == typeof(MultiService)
                && m.Lifetime == Lifetimes.Transient);
            results.ShouldContain(m => m.Service == typeof(Settings)
                && m.Implementation == typeof(Settings)
                && m.Lifetime == Lifetimes.Singleton);
            results.Count().ShouldBe(4);
        }

        [Theory]
        [InlineData(typeof(DefaultService), null, typeof(IDefaultService), null)] // Single interface
        [InlineData(typeof(MultiService), typeof(IMultiService), typeof(IMultiService), null)] // Multi service
        [InlineData(typeof(Settings), typeof(Settings), typeof(Settings), null)] // Self mapping
        [InlineData(typeof(MultiService), null, null, "Multiple interfaces found")] // Error
        [InlineData(typeof(NoInterfaceService), null, null, "No interfaces found")] // Error
        [InlineData(typeof(MultiService), typeof(IDefaultService), null, "Target service not implemented")] // Error
        public void GetMappingTest(Type implementationType, Type target, Type expectedService, string expectedError)
        {
            // Arrange
            var attribute = new IocAttribute(Lifetimes.Singleton) { Target = target };

            // Act
            try
            {
                var result = attribute.GetMapping(implementationType);

                // Assert
                expectedError.ShouldBeNullOrEmpty();
                result.Implementation.ShouldBe(implementationType);
                result.Lifetime.ShouldBe(Lifetimes.Singleton);
                result.Service.ShouldBe(expectedService);
            }
            catch (IocMappingException ex)
            {
                // Assert
                ex.Message.ShouldBe(expectedError);
                ex.Implementation.ShouldBe(implementationType);
            }
        }
    }
}
