using NSubstitute;
using Shopfloor.Models.RoleUserModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.RoleUserModel
{
    public class RoleUserTests
    {


        public RoleUserTests()
        {

        }

        private RoleUser CreateRoleUser()
        {
            return new RoleUser();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var roleUser = this.CreateRoleUser();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
