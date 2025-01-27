using System.Globalization;
using Shopfloor.Converters;

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
            result.ShouldBe(true);
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
            result.ShouldBe(input);
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
            result.ShouldBe(false);
        }
        [Fact]
        public void ConvertBack_AlwaysThrowsNotImplementedException()
        {
            // Arrange
            BooleanReverseConverter converter = new();
            // Act
            Action result = () => converter.ConvertBack(new object(), typeof(object), new object(), CultureInfo.InvariantCulture);
            // Assert
            result.ShouldThrow<NotImplementedException>();
        }
    }
}