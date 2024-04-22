using NSubstitute;
using Shopfloor.Models.UserModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.UserModel
{
    public class UserValidationTests
    {


        public UserValidationTests()
        {

        }

        private UserValidation CreateUserValidation()
        {
            return new UserValidation(
                TODO);
        }

        [Fact]
        public void ValidateAutoLogin_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var userValidation = this.CreateUserValidation();
            User? user = null;
            Dictionary propertyErrors = null;

            // Act
            userValidation.ValidateAutoLogin(
                user,
                propertyErrors);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void ValidateName_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var userValidation = this.CreateUserValidation();
            string value = null;
            string propertyName = null;

            // Act
            userValidation.ValidateName(
                value,
                propertyName);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void ValidateLogin_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var userValidation = this.CreateUserValidation();
            User? user = null;
            IInputForm inputForm = null;

            // Act
            userValidation.ValidateLogin(
                user,
                inputForm);

            // Assert
            Assert.True(false);
        }
    }
}
