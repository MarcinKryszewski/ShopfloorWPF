using NSubstitute;
using Shopfloor.Features.Admin.Users.Stores;
using Shopfloor.Models.RoleModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Users.Stores
{
    public class RoleValueTests
    {
        private Role subRole;

        public RoleValueTests()
        {
            this.subRole = Substitute.For<Role>();
        }

        private RoleValue CreateRoleValue()
        {
            return new RoleValue(
                this.subRole,
                TODO);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var roleValue = this.CreateRoleValue();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
