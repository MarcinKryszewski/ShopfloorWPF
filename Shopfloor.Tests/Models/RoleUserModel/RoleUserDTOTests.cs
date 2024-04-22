using NSubstitute;
using Shopfloor.Models.RoleUserModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.RoleUserModel
{
    public class RoleUserDTOTests
    {


        public RoleUserDTOTests()
        {

        }

        private RoleUserDTO CreateRoleUserDTO()
        {
            return new RoleUserDTO();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var roleUserDTO = this.CreateRoleUserDTO();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
