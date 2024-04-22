using NSubstitute;
using Shopfloor.Models.RoleModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.RoleModel
{
    public class RoleStoreTests
    {
        private RoleProvider subRoleProvider;

        public RoleStoreTests()
        {
            this.subRoleProvider = Substitute.For<RoleProvider>();
        }

        private RoleStore CreateRoleStore()
        {
            return new RoleStore(
                this.subRoleProvider);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var roleStore = this.CreateRoleStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
