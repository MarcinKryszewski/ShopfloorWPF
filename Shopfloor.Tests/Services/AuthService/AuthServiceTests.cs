using Shopfloor.Models.UserModel;
using Shopfloor.Services;

namespace Shopfloor.Tests.Services.AuthServices
{
    public class AuthServiceTests
    {
        private readonly IUserProvider _provider = Substitute.For<IUserProvider>();
        [Fact]
        public void Login_ShouldReturnUser_WhenUsernameFound()
        {
            // Arrange
            AuthService sut = new(_provider);
            string username = "test";
            User user = new() { Username = username };
            _provider.GetByUsername(username).Returns(user);
            // Act
            User? result = sut.Login(username);
            // Assert
            result.Should().NotBeNull();
            result.Should().Be(user);
        }
        [Fact]
        public void Login_ShouldReturnNull_WhenUsernameNotFound()
        {
            // Arrange
            AuthService sut = new(_provider);
            string username = "test";
            User user = new() { Username = username };
            _provider.GetByUsername(username).ReturnsNull();
            // Act
            User? result = sut.Login(username);
            // Assert
            result.Should().BeNull();
            result.Should().NotBe(user);
        }
    }
}