using NSubstitute;
using Shopfloor.Hosts.Features.Login;
using System;
using Xunit;

namespace Shopfloor.Tests.Hosts.Features.Login
{
    public class LoginHostTests
    {


        public LoginHostTests()
        {

        }

        private LoginHost CreateLoginHost()
        {
            return new LoginHost();
        }

        [Fact]
        public void Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var loginHost = this.CreateLoginHost();
            IServiceCollection services = null;

            // Act
            loginHost.Get(
                services);

            // Assert
            Assert.True(false);
        }
    }
}
