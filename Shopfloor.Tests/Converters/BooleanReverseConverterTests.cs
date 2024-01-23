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
            object result = converter.Convert(input, typeof(object), new object(), CultureInfo.InvariantCulture);
            // Assert
            result.Should().Be(true);
        }
        [Fact]
        public void Convert_WhenInputIsNotBool_ReturnsSameValue()
        {
            // Arrange
            BooleanReverseConverter converter = new();
            object input = "someValue";
            // Act
            object result = converter.Convert(input, typeof(object), new object(), CultureInfo.InvariantCulture);
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
            object result = converter.Convert(input, typeof(object), new object(), CultureInfo.InvariantCulture);
            // Assert
            result.Should().Be(false);
        }
        [Fact]
        public void ConvertBack_AlwaysThrowsNotImplementedException()
        {
            // Arrange
            BooleanReverseConverter converter = new();
            // Act
            Action result = () => converter.ConvertBack(new object(), typeof(object), new object(), CultureInfo.InvariantCulture);
            // Assert
            result.Should().Throw<NotImplementedException>();
        }
    }
}