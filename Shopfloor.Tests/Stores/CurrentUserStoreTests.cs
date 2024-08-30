using Shopfloor.Interfaces;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.RoleUserModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Services;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Stores;

namespace Shopfloor.Tests.Stores
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
            _sut = new(
                _roleProvider,
                _roleUserProvider,
                _notifier,
                _auth);
        }
        [Fact]
        public void Login_WhenUsernameExists_ShouldSetUser()
        {
            // Arrange
            string username = "test";
            User user = new()
            {
                Username = username,
            };

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
            User user = new()
            {
                Username = username,
            };
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
            User user = new()
            {
                Username = username,
            };
            _auth.Login(username).Returns(user);
            _sut.Login(username);

            // Act
            _sut.Logout();
            User? result = _sut.User;

            // Assert
            result.Should().NotBe(user);
        }
        [Fact]
        public void SetUserRoles_WithRolesAndRoleUsers_RolesAddedToUser()
        {
            // Arrange
            string username = "test";
            User user = new()
            {
                Username = username,
                Id = 1
            };
            IEnumerable<Role> roles = [new Role(1, "test", 5)];
            IEnumerable<RoleUser> roleUsers = [new RoleUser() { RoleId = 1, UserId = 1 }];

            _auth.Login(username).Returns(user);
            _roleProvider.GetAll().Returns(roles);
            _roleUserProvider.GetAllForUser(1).Returns(roleUsers);

            // Act
            _sut.Login(username);
            int rolesAmount = _sut.User!.GetRoles().Count;
            bool result = rolesAmount == roleUsers.Count();

            // Assert
            result.Should().BeTrue();
            rolesAmount.Should().BeGreaterThan(0);
        }

        [Fact]
        public void SetUserRoles_RoleNotFound_NoRoleAdded()
        {
            // Arrange
            string username = "test";
            User user = new()
            {
                Username = username
            };
            IEnumerable<Role> roles = [new Role(2, "test", 5)];
            IEnumerable<RoleUser> roleUsers = [new RoleUser() { RoleId = 1, UserId = 1 }];

            _auth.Login(username).Returns(user);
            _roleProvider.GetAll().Returns(roles);
            _roleUserProvider.GetAllForUser(1).Returns(roleUsers);
            _sut.Login(username);

            // Act
            _sut.Login(username);
            int rolesAmount = _sut.User!.GetRoles().Count;
            bool result = rolesAmount != roleUsers.Count();

            // Assert
            result.Should().BeTrue();
            rolesAmount.Should().Be(0);
        }
    }
}