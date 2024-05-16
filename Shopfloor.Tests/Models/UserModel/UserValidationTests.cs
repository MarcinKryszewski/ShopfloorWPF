using System.ComponentModel;
using Shopfloor.Interfaces;
using Shopfloor.Models.UserModel;

namespace Shopfloor.Tests.Models.UserModel
{
    public class UserValidationTests
    {
        IInputForm<User> _inputForm = Substitute.For<IInputForm<User>>();
        [Fact]
        public void ValidateName_UsernameIsNull_AddErrorToList()
        {
            // Arrange
            string? name = null;
            UserValidation sut = new(_inputForm);
            // Act
            sut.ValidateName(name, "name");
            bool result = _inputForm.HasErrors;
            // Assert
            result.Should().BeTrue();
        }
        [Fact]
        public void ValidateName_CorrectUsername_ReturnsEmptyErrorList()
        {
            // Arrange
            UserValidation sut = new(_inputForm);
            // Act
            // Assert
        }
        [Fact]
        public void ValidateName_UsernameLengthIsZero_AddErrorToList()
        {
            // Arrange
            UserValidation sut = new(_inputForm);
            // Act
            // Assert
        }
    }
}
