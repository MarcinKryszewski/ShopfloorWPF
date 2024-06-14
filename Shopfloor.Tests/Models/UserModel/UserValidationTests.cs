using Shopfloor.Models.UserModel;

namespace Shopfloor.Tests.Models.UserModel
{
    public class UserValidationTests
    {
        [Theory]
        [InlineData("test ")]
        [InlineData("test1")]
        [InlineData("test#")]
        public void ValidateName_ShouldAddError_WhenNameContainsSpecialCharacter(string testedValue)
        {
            // Arrange            
            string propertyName = nameof(User.Name);
            User user = new() { Username = "test" };
            UserValidation validation = new(user);
            // Act
            user.ClearErrors(propertyName);
            user.Name = testedValue;
            validation.ValidateName();
            bool result = user.GetErrors(propertyName).Cast<object>().Any();
            // Assert
            result.Should().BeTrue();
        }
        [Theory]
        [InlineData("")]
        [InlineData("t")]
        [InlineData("te")]
        public void ValidateName_ShouldAddError_WhenNameIsTooSort(string testedValue)
        {
            // Arrange
            string propertyName = nameof(User.Name);
            User user = new() { Username = "test" };
            UserValidation validation = new(user);
            // Act
            user.ClearErrors(propertyName);
            user.Name = testedValue;
            validation.ValidateName();
            bool result = user.GetErrors(propertyName).Cast<object>().Any();
            // Assert
            result.Should().BeTrue();
        }
        [Theory]
        [InlineData("test")]
        [InlineData("Test")]
        [InlineData("tEst")]
        [InlineData("teSt")]
        public void ValidateName_ShouldNotAddError_WhenNameIsCorrect(string testedValue)
        {
            // Arrange
            string propertyName = nameof(User.Name);
            User user = new() { Username = "test" };
            UserValidation validation = new(user);
            // Act
            user.ClearErrors(propertyName);
            user.Name = testedValue;
            validation.ValidateName();
            bool result = user.GetErrors(propertyName).Cast<object>().Any();
            // Assert
            result.Should().BeFalse();
        }
    }
}
