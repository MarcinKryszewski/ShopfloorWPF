using FluentAssertions;
using Shopfloor.Utilities;

namespace Shopfloor.Tests.Utilities
{
    public class RemovePolishCharactersTests
    {
        [Theory]
        [InlineData("ąćęłńóśźż", "acelnoszz")]
        [InlineData("ĄĆĘŁŃÓŚŹŻ", "ACELNOSZZ")]
        [InlineData("ZażółćGĘśląJaźń", "ZazolcGEslaJazn")]

        public void Remove_ReplacesPolishCharactersWithEnglishCharacters(string input, string expected)
        {
            // Arrange
            // Act
            string result = RemovePolishCharacters.Remove(input);
            // Assert
            result.Should().Be(expected);
        }
        [Fact]
        public void Remove_WithEmptyString_ReturnsEmptyString()
        {
            // Arrange
            string input = string.Empty;
            // Act
            string result = RemovePolishCharacters.Remove(input);
            // Assert
            result.Should().BeEmpty();
        }
        [Fact]
        public void Remove_WithNoPolishCharacters_ReturnsSameString()
        {
            // Arrange
            string input = "Hello World!";
            // Act
            string result = RemovePolishCharacters.Remove(input);
            // Assert
            result.Should().Be(input);
        }
    }
}