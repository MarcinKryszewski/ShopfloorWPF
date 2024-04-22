using NSubstitute;
using Shopfloor.Features.Admin.Users;
using Shopfloor.Features.Admin.Users.Stores;
using Shopfloor.Features.Admin.UsersList.Commands;
using Shopfloor.Models.RoleUserModel;
using Shopfloor.Models.UserModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Users.Commands
{
    public class UserAddCommandTests
    {
        private UsersAddViewModel subUsersAddViewModel;
        private RolesStore subRolesStore;
        private IUserProvider subUserProvider;
        private IRoleIUserProvider subRoleIUserProvider;

        public UserAddCommandTests()
        {
            this.subUsersAddViewModel = Substitute.For<UsersAddViewModel>();
            this.subRolesStore = Substitute.For<RolesStore>();
            this.subUserProvider = Substitute.For<IUserProvider>();
            this.subRoleIUserProvider = Substitute.For<IRoleIUserProvider>();
        }

        private UserAddCommand CreateUserAddCommand()
        {
            return new UserAddCommand(
                this.subUsersAddViewModel,
                this.subRolesStore,
                this.subUserProvider,
                this.subRoleIUserProvider);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var userAddCommand = this.CreateUserAddCommand();
            object? parameter = null;

            // Act
            userAddCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
