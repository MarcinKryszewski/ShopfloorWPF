using NSubstitute;
using Shopfloor.Hosts.Features.Manager;
using System;
using Xunit;

namespace Shopfloor.Tests.Hosts.Features.Manager
{
    public class ManagerHostTests
    {


        public ManagerHostTests()
        {

        }

        private ManagerHost CreateManagerHost()
        {
            return new ManagerHost();
        }

        [Fact]
        public void Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var managerHost = this.CreateManagerHost();
            IServiceCollection services = null;

            // Act
            managerHost.Get(
                services);

            // Assert
            Assert.True(false);
        }
    }
}
