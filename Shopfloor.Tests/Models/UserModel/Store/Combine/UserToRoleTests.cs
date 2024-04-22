using NSubstitute;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.RoleUserModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Models.UserModel.Store.Combine;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.UserModel.Store.Combine
{
    public class UserToRoleTests
    {
        private UserStore subUserStore;
        private RoleStore subRoleStore;
        private RoleUserStore subRoleUserStore;

        public UserToRoleTests()
        {
            this.subUserStore = Substitute.For<UserStore>();
            this.subRoleStore = Substitute.For<RoleStore>();
            this.subRoleUserStore = Substitute.For<RoleUserStore>();
        }

        private UserToRole CreateUserToRole()
        {
            return new UserToRole(
                this.subUserStore,
                this.subRoleStore,
                this.subRoleUserStore);
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var userToRole = this.CreateUserToRole();

            // Act
            await userToRole.Combine();

            // Assert
            Assert.True(false);
        }
    }
}
