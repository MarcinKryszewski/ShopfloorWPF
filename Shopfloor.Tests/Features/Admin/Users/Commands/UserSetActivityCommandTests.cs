using NSubstitute;
using Shopfloor.Features.Admin.Users;
using Shopfloor.Features.Admin.UsersList.Commands;
using Shopfloor.Models.UserModel;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Users.Commands
{
    public class UserSetActivityCommandTests
    {
        private UsersListViewModel subUsersListViewModel;
        private IUserProvider subUserProvider;

        public UserSetActivityCommandTests()
        {
            this.subUsersListViewModel = Substitute.For<UsersListViewModel>();
            this.subUserProvider = Substitute.For<IUserProvider>();
        }

        private UserSetActivityCommand CreateUserSetActivityCommand()
        {
            return new UserSetActivityCommand(
                this.subUsersListViewModel,
                this.subUserProvider);
        }

        [Fact]
        public async Task ExecuteAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var userSetActivityCommand = this.CreateUserSetActivityCommand();
            object? parameter = null;

            // Act
            await userSetActivityCommand.ExecuteAsync(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
