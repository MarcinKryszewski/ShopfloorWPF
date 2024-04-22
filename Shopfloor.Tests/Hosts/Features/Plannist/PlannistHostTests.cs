using NSubstitute;
using Shopfloor.Hosts.Features.Plannist;
using System;
using Xunit;

namespace Shopfloor.Tests.Hosts.Features.Plannist
{
    public class PlannistHostTests
    {


        public PlannistHostTests()
        {

        }

        private PlannistHost CreatePlannistHost()
        {
            return new PlannistHost();
        }

        [Fact]
        public void Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var plannistHost = this.CreatePlannistHost();
            IServiceCollection services = null;

            // Act
            plannistHost.Get(
                services);

            // Assert
            Assert.True(false);
        }
    }
}
