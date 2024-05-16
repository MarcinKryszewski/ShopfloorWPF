using Shopfloor.Models.UserModel;

namespace Shopfloor.Tests.Models.UserModel
{
    public sealed class UserTests
    {
        [Fact]
        public void Id_WhenIdNotProvided_ShouldReturnNull()
        {
            // Arrange
            var user = new User() { Username = "testUsername" };
            // Act
            var result = user.Id;
            // Assert
            result.Should().BeNull();
        }
        [Fact]
        public void Id_WhenIdProvided_ShouldReturnValue()
        {
            // Arrange
            var user = new User() { Id = 1, Username = "test" };
            // Act
            var id = user.Id;
            // Assert
            id.Should().Be(1);
        }
        [Fact]
        public void Username_WhenProvided_ShouldReturnValue()
        {
            // Arrange
            var expectedUsername = "testUsername";
            var user = new User() { Username = "testUsername" };

            // Act
            var username = user.Username;

            // Assert
            username.Should().Be(expectedUsername);
        }

        [Fact]
        public void Username_Should_Return_Correct_Value_After_Constructor_With_Id()
        {
            // Arrange
            var expectedUsername = "testUsername";
            var user = new User()
            {
                Username = expectedUsername,
                Surname = "TestSurname"
            };

            // Act
            var username = user.Username;

            // Assert
            username.Should().Be(expectedUsername);
        }
    }
}