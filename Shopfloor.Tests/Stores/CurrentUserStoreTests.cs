using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shopfloor.Interfaces;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.RoleUserModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Services;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Stores;

namespace Shopfloor.Tests
{
    public class CurrentUserStoreTests
    {
        private readonly CurrentUserStore _sut;
        private readonly IProvider<Role> _roleProvider = Substitute.For<IProvider<Role>>();
        private readonly IRoleUserProvider _roleUserProvider = Substitute.For<IRoleUserProvider>();
        private readonly INotifier _notifier = Substitute.For<INotifier>();
        private readonly IAuthService _auth = Substitute.For<IAuthService>();
        public CurrentUserStoreTests()
        {
            _sut = new(_roleProvider, _roleUserProvider, _notifier, _auth);
        }
        [Fact]
        public void Login_WhenUsernameExists_ShouldSetUser()
        {
            // Arrange
            string username = "test";
            User user = new(1, username, "name", "surname");

            _auth.Login(username).Returns(user);
            // Act
            _sut.Login(username);
            // Assert
            _sut.User.Should().Be(user);
            _sut.IsUserLoggedIn.Should().BeTrue();
        }
        [Fact]
        public void Login_WhenUsernameNotExists_ShouldReturnNull()
        {
            // Arrange
            string username = "test";

            _auth.Login(username).ReturnsNull();
            // Act
            _sut.Login(username);
            // Assert
            _sut.User.Should().Be(null);
            _sut.IsUserLoggedIn.Should().BeFalse();
        }
        [Fact]
        public void Logout_WhenExecuted_ShouldSetsIsUserLoggedInToFalse()
        {
            // Arrange
            string username = "test";
            User user = new(1, username, "name", "surname");
            _auth.Login(username).Returns(user);
            _sut.Login(username);

            // Act
            _sut.Logout();
            bool result = _sut.IsUserLoggedIn;

            // Assert
            result.Should().BeFalse();
        }
        [Fact]
        public void Logout_WhenExecuted_SetsUsernameToDefault()
        {
            // Arrange
            string username = "test";
            User user = new(1, username, "name", "surname");
            _auth.Login(username).Returns(user);
            _sut.Login(username);

            // Act
            _sut.Logout();
            User? result = _sut.User;

            // Assert
            result.Should().NotBe(user);
        }
        [Fact]
        public void HasRole_IfUserIsNullWithRoleObject_ReturnsFalse()
        {
            // Arrange
            YourClass yourClass = new YourClass(); // Assuming YourClass is the class containing the HasRole method
            yourClass._user = null; // Set _user to null

            // Act
            bool result = yourClass.HasRole(new Role(1)); // Pass any role for testing

            // Assert
            Assert.False(result); // Ensure method returns false if user is null
        }
        [Fact]
        public void HasRole_IfUserIsAuthorizedWithRoleObject_ReturnsTrue()
        {
            // Arrange
            YourClass yourClass = new YourClass(); // Assuming YourClass is the class containing the HasRole method
            yourClass._user = new User(); // Assuming User class and its constructor
            int authorizedRoleValue = 1; // Example authorized role value
            yourClass._user.AddAuthorizedRole(authorizedRoleValue); // Assuming method to add authorized role

            // Act
            bool result = yourClass.HasRole(new Role(authorizedRoleValue)); // Pass the authorized role

            // Assert
            Assert.True(result); // Ensure method returns true if user is authorized for the role
        }
        [Fact]
        public void HasRole_IfUserIsNotAuthorizedWithRoleObject_ReturnsFalse()
        {
            // Arrange
            YourClass yourClass = new YourClass(); // Assuming YourClass is the class containing the HasRole method
            yourClass._user = new User(); // Assuming User class and its constructor
            int unauthorizedRoleValue = 2; // Example unauthorized role value

            // Act
            bool result = yourClass.HasRole(new Role(unauthorizedRoleValue)); // Pass the unauthorized role

            // Assert
            Assert.False(result); // Ensure method returns false if user is not authorized for the role
        }
        [Fact]
        public void HasRole_IfUserIsNullWithRoleValue_ReturnsFalse()
        {
            // Arrange
            YourClass yourClass = new YourClass(); // Assuming YourClass is the class containing the HasRole method
            yourClass._user = null; // Set _user to null

            // Act
            bool result = yourClass.HasRole(1); // Pass any role value for testing

            // Assert
            Assert.False(result); // Ensure method returns false if user is null
        }
        [Fact]
        public void HasRole_IfUserIsAuthorizedWithRoleValue_ReturnsTrue()
        {
            // Arrange
            YourClass yourClass = new YourClass(); // Assuming YourClass is the class containing the HasRole method
            yourClass._user = new User(); // Assuming User class and its constructor
            int authorizedRoleValue = 1; // Example authorized role value
            yourClass._user.AddAuthorizedRole(authorizedRoleValue); // Assuming method to add authorized role

            // Act
            bool result = yourClass.HasRole(authorizedRoleValue); // Pass the authorized role value

            // Assert
            Assert.True(result); // Ensure method returns true if user is authorized for the role
        }
        [Fact]
        public void HasRole_WithRoleValue_ReturnsFalseIfUserIsNotAuthorized()
        {
            // Arrange
            YourClass yourClass = new YourClass(); // Assuming YourClass is the class containing the HasRole method
            yourClass._user = new User(); // Assuming User class and its constructor
            int unauthorizedRoleValue = 2; // Example unauthorized role value

            // Act
            bool result = yourClass.HasRole(unauthorizedRoleValue); // Pass the unauthorized role value

            // Assert
            Assert.False(result); // Ensure method returns false if user is not authorized for the role
        }
    }
}