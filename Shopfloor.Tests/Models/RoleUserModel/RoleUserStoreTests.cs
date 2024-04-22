using NSubstitute;
using Shopfloor.Models.RoleUserModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.RoleUserModel
{
    public class RoleUserStoreTests
    {
        private RoleIUserProvider subRoleIUserProvider;

        public RoleUserStoreTests()
        {
            this.subRoleIUserProvider = Substitute.For<RoleIUserProvider>();
        }

        private RoleUserStore CreateRoleUserStore()
        {
            return new RoleUserStore(
                this.subRoleIUserProvider);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var roleUserStore = this.CreateRoleUserStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
