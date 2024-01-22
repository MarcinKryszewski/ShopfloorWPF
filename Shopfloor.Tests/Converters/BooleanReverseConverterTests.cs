using FluentAssertions;
using Shopfloor.Converters;
using System.Globalization;

namespace Shopfloor.Tests.Converters
{
    public class BooleanReverseConverterTests
    {
        [Fact]
        public void Convert_WhenInputIsFalse_ReturnsTrue()
        {
            // Arrange
            BooleanReverseConverter converter = new();
            bool input = false;

            // Act
            object result = converter.Convert(input, typeof(object), null, CultureInfo.InvariantCulture);

            // Assert
            result.Should().Be(true);
        }
        [Fact]
        public void Convert_WhenInputIsNotBool_ReturnsSameValue()
        {
            // Arrange
            BooleanReverseConverter converter = new();
            object input = "someValue"; // Not a bool

            // Act
            object result = converter.Convert(input, typeof(object), null, CultureInfo.InvariantCulture);

            // Assert
            result.Should().Be(input);
        }
        [Fact]
        public void Convert_WhenInputIsTrue_ReturnsFalse()
        {
            // Arrange
            BooleanReverseConverter converter = new();
            bool input = true;
            // Act
            object result = converter.Convert(input, typeof(object), null, CultureInfo.InvariantCulture);
            // Assert
            result.Should().Be(false);
        }
        [Fact]
        public void ConvertBack_AlwaysThrowsNotImplementedException()
        {
            // Arrange
            BooleanReverseConverter converter = new();

            // Act and Assert
            FluentActions.Invoking(() => converter.ConvertBack(null, null, null, null))
                .Should().Throw<NotImplementedException>();
        }
    }
}