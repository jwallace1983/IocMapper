namespace IocMapper.UnitTests
{
    public class MappingExceptionTest
    {
        [Fact]
        public void Constructor_ShouldInitializeTypeProperty()
        {
            // Arrange
            var expectedType = typeof(string);

            // Act
            var exception = new MappingException(expectedType);

            // Assert
            exception.Type.ShouldBe(expectedType);
        }

        [Fact]
        public void Constructor_ShouldSetDefaultErrorMessage_WhenErrorIsNull()
        {
            // Arrange
            var expectedType = typeof(int);

            // Act
            var exception = new MappingException(expectedType);

            // Assert
            exception.Message.ShouldBe("mapping-error");
        }

        [Fact]
        public void Constructor_ShouldSetCustomErrorMessage_WhenErrorIsProvided()
        {
            // Arrange
            var expectedType = typeof(double);
            var customMessage = "Custom error message";

            // Act
            var exception = new MappingException(expectedType, customMessage);

            // Assert
            exception.Message.ShouldBe(customMessage);
        }
    }
}
