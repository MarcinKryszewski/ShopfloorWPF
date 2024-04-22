using NSubstitute;
using Shopfloor.Models.RoleModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.RoleModel
{
    public class RoleTests
    {


        public RoleTests()
        {

        }

        private Role CreateRole()
        {
            return new Role(
                TODO,
                TODO,
                TODO);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var role = this.CreateRole();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
