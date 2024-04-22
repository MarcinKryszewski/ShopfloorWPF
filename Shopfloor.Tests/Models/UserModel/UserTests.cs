using NSubstitute;
using Shopfloor.Models.UserModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.UserModel
{
    public class UserTests
    {


        public UserTests()
        {

        }

        private User CreateUser()
        {
            return new User(
                TODO,
                TODO,
                TODO,
                TODO,
                TODO,
                TODO);
        }

        [Fact]
        public void SetActive_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var user = this.CreateUser();
            bool isActive = false;

            // Act
            user.SetActive(
                isActive);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void AddRole_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var user = this.CreateUser();
            Role role = null;

            // Act
            user.AddRole(
                role);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void ClearRoles_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var user = this.CreateUser();

            // Act
            user.ClearRoles();

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void GetRoles_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var user = this.CreateUser();

            // Act
            var result = user.GetRoles();

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void IsAuthorized_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var user = this.CreateUser();
            int roleValue = 0;

            // Act
            var result = user.IsAuthorized(
                roleValue);

            // Assert
            Assert.True(false);
        }
    }
}
