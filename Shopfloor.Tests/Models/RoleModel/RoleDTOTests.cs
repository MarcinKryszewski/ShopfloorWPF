using NSubstitute;
using Shopfloor.Models.RoleModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.RoleModel
{
    public class RoleDTOTests
    {


        public RoleDTOTests()
        {

        }

        private RoleDTO CreateRoleDTO()
        {
            return new RoleDTO();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var roleDTO = this.CreateRoleDTO();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
