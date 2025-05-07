using IocMapper.Reflectors;
using IocMapper.UnitTests.Mediator.Mocks;

namespace IocMapper.UnitTests.Reflectors
{
    public class MediatorReflectorTest
    {
        [Fact]
        public void GetMediatorMappingKeyTest()
        {
            // Arrange
            var target = new MediatorReflector(GetType());

            // Act
            string key = target.GetMediatorMappingKey(typeof(SimpleRequest));

            // Assert
            key.ShouldBe("IocMapper.UnitTests.Mediator.Mocks.SimpleRequest");
        }

        [Fact]
        public void GetMediatorMappingTest()
        {
            // Arrange / Act
            var mapping = MediatorReflector.GetMediatorMapping(typeof(AddRequest.Handler));

            // Assert
            mapping.ShouldNotBeNull();
            mapping.HandlerType.Name.ShouldBe(typeof(AddRequest.Handler).Name);
            mapping.RequestType.Name.ShouldBe(typeof(AddRequest).Name);
            mapping.ResponseType.Name.ShouldBe(typeof(int).Name);
        }

        [Fact]
        public void GetMediatorMapping_Invalid()
        {
            // Arrange / Act
            var mapping = MediatorReflector.GetMediatorMapping(typeof(string));

            // Assert
            mapping.ShouldBeNull();
        }

    }
}
