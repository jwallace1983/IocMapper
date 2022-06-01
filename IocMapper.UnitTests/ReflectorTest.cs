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
            results.Should().NotBeNull();
        }

        [Fact]
        public void AddAssemblyTest()
        {
            // Arrange
            // Act
            Reflector.AddAssembly(typeof(ReflectorTest));
            Reflector.AddAssembly(typeof(Mocks.LifetimeService));
            Reflector.AddAssembly(typeof(Mocks.IDefaultService));

            // Assert
            Reflector.Assemblies.Should().HaveCount(1);
        }
    }
}
