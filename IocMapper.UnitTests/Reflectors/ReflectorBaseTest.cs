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
            target.AddAssembly(typeof(Lifetimes).Assembly); // From IocMapper
            target.AddAssembly(typeof(IDefaultService).Assembly); // From IosMapper.UnitTests

            // Assert
            target.Assemblies.Count().ShouldBe(2);
        }

    }
}
