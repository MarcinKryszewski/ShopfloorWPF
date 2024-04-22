using NSubstitute;
using Shopfloor.Models.RoleModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.RoleModel
{
    public class RolesTests
    {


        public RolesTests()
        {

        }

        private Roles CreateRoles()
        {
            return new Roles();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var roles = this.CreateRoles();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
