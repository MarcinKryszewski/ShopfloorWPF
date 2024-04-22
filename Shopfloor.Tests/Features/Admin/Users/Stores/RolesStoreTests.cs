using NSubstitute;
using Shopfloor.Features.Admin.Users.Stores;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Users.Stores
{
    public class RolesStoreTests
    {


        public RolesStoreTests()
        {

        }

        private RolesStore CreateRolesStore()
        {
            return new RolesStore();
        }

        [Fact]
        public void AddRole_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var rolesStore = this.CreateRolesStore();
            Role role = null;
            bool assigned = false;

            // Act
            rolesStore.AddRole(
                role,
                assigned);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void GetAllAssignedRoles_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var rolesStore = this.CreateRolesStore();

            // Act
            var result = rolesStore.GetAllAssignedRoles();

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void GetAllRevokedRoles_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var rolesStore = this.CreateRolesStore();

            // Act
            var result = rolesStore.GetAllRevokedRoles();

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void ClearRoles_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var rolesStore = this.CreateRolesStore();

            // Act
            rolesStore.ClearRoles();

            // Assert
            Assert.True(false);
        }
    }
}
