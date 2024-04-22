using NSubstitute;
using Shopfloor.Hosts;
using System;
using Xunit;

namespace Shopfloor.Tests.Hosts
{
    public class AppHostTests
    {


        public AppHostTests()
        {

        }

        private AppHost CreateAppHost()
        {
            return new AppHost();
        }

        [Fact]
        public void Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var appHost = this.CreateAppHost();

            // Act
            var result = appHost.Get();

            // Assert
            Assert.True(false);
        }
    }
}
