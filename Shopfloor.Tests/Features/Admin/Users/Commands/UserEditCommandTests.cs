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
    public class UserEditCommandTests
    {
        private UsersEditViewModel subUsersEditViewModel;
        private IUserProvider subUserProvider;
        private IRoleIUserProvider subRoleIUserProvider;
        private RolesStore subRolesStore;

        public UserEditCommandTests()
        {
            this.subUsersEditViewModel = Substitute.For<UsersEditViewModel>();
            this.subUserProvider = Substitute.For<IUserProvider>();
            this.subRoleIUserProvider = Substitute.For<IRoleIUserProvider>();
            this.subRolesStore = Substitute.For<RolesStore>();
        }

        private UserEditCommand CreateUserEditCommand()
        {
            return new UserEditCommand(
                this.subUsersEditViewModel,
                this.subUserProvider,
                this.subRoleIUserProvider,
                this.subRolesStore,
                TODO,
                TODO,
                TODO);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var userEditCommand = this.CreateUserEditCommand();
            object? parameter = null;

            // Act
            userEditCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
