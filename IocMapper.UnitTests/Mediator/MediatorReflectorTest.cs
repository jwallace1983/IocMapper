using IocMapper.Mediator;
using IocMapper.UnitTests.Mediator.Mocks;

namespace IocMapper.UnitTests.Mediator
{
    public class MediatorReflectorTest
    {
        [Fact]
        public void GetMappingsTest()
        {
            // Arrange
            MediatorReflector.AddAssembly(this.GetType());

            // Act
            var mappings = MediatorReflector.GetMappings();

            // Assert
            mappings.Count.ShouldNotBe(0);
            mappings.ShouldContainKey(typeof(AddRequest).GetMediatorMappingKey());
            mappings.ShouldContainKey(typeof(SimpleRequest).GetMediatorMappingKey());
        }

    }
}
