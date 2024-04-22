using NSubstitute;
using Shopfloor.Models.UserModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.UserModel
{
    public class UserDTOTests
    {


        public UserDTOTests()
        {

        }

        private UserDTO CreateUserDTO()
        {
            return new UserDTO();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var userDTO = this.CreateUserDTO();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
