using IocMapper.Reflectors;
using IocMapper.UnitTests.Reflectors.Mocks;
using System;
using System.Linq;

namespace IocMapper.UnitTests.Reflectors
{
    public class ReflectorBaseTest
    {
        [Fact]
        public void AssembliesTest()
        {
            // Arrange
            var target = new IocReflector(typeof(ReflectorBaseTest));

            // Act
            var results = target.Assemblies;

            // Assert
            results.ShouldNotBeNull();
        }

        [Fact]
        public void AddAssemblyTest_WithDuplicates()
        {
            // Arrange
            var target = new IocReflector(typeof(ReflectorBaseTest));

            // Act
            target.AddAssembly(typeof(LifetimeService).Assembly); // From IocMapper
            target.AddAssembly(typeof(IDefaultService).Assembly); // From IosMapper.UnitTests

            // Assert
            target.Assemblies.Count().ShouldBe(2);
        }

        //[Fact]
        //public void GetMappingsTest()
        //{
        //    // Arrange
        //    var target = new IocReflector(typeof(ReflectorBaseTest));

        //    // Act
        //    var results = target.GetMappings();

        //    // Assert
        //    results.ShouldContain(m => m.Service == typeof(IDefaultService)
        //        && m.Implementation == typeof(DefaultService)
        //        && m.Lifetime == Lifetimes.Transient);
        //    results.ShouldContain(m => m.Service == typeof(ILifetimeService)
        //        && m.Implementation == typeof(LifetimeService)
        //        && m.Lifetime == Lifetimes.Singleton);
        //    results.ShouldContain(m => m.Service == typeof(IMultiService)
        //        && m.Implementation == typeof(MultiService)
        //        && m.Lifetime == Lifetimes.Transient);
        //    results.ShouldContain(m => m.Service == typeof(Settings)
        //        && m.Implementation == typeof(Settings)
        //        && m.Lifetime == Lifetimes.Singleton);
        //    results.Count().ShouldBe(4);
        //}

        //[Theory]
        //[InlineData(typeof(DefaultService), null, typeof(IDefaultService), null)] // Single interface
        //[InlineData(typeof(MultiService), typeof(IMultiService), typeof(IMultiService), null)] // Multi service
        //[InlineData(typeof(Settings), typeof(Settings), typeof(Settings), null)] // Self mapping
        //[InlineData(typeof(MultiService), null, null, "Multiple interfaces found")] // Error
        //[InlineData(typeof(NoInterfaceService), null, null, "No interfaces found")] // Error
        //[InlineData(typeof(MultiService), typeof(IDefaultService), null, "Target service not implemented")] // Error
        //public void GetMappingTest(Type implementationType, Type target, Type expectedService, string expectedError)
        //{
        //    // Arrange
        //    var attribute = new IocAttribute(Lifetimes.Singleton) { Target = target };

        //    // Act
        //    try
        //    {
        //        var result = attribute.GetMapping(implementationType);

        //        // Assert
        //        expectedError.ShouldBeNullOrEmpty();
        //        result.Implementation.ShouldBe(implementationType);
        //        result.Lifetime.ShouldBe(Lifetimes.Singleton);
        //        result.Service.ShouldBe(expectedService);
        //    }
        //    catch (MappingException ex)
        //    {
        //        // Assert
        //        ex.Message.ShouldBe(expectedError);
        //        ex.Type.ShouldBe(implementationType);
        //    }
        //}
    }
}
