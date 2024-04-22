using NSubstitute;
using Shopfloor.Hosts.Core;
using System;
using Xunit;

namespace Shopfloor.Tests.Hosts.Core
{
    public class NavigationHostTests
    {


        public NavigationHostTests()
        {

        }

        private NavigationHost CreateNavigationHost()
        {
            return new NavigationHost();
        }

        [Fact]
        public void Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var navigationHost = this.CreateNavigationHost();
            IServiceCollection services = null;

            // Act
            navigationHost.Get(
                services);

            // Assert
            Assert.True(false);
        }
    }
}
