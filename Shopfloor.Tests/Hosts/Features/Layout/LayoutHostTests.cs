using NSubstitute;
using Shopfloor.Hosts.Features.Layout;
using System;
using Xunit;

namespace Shopfloor.Tests.Hosts.Features.Layout
{
    public class LayoutHostTests
    {


        public LayoutHostTests()
        {

        }

        private LayoutHost CreateLayoutHost()
        {
            return new LayoutHost();
        }

        [Fact]
        public void Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var layoutHost = this.CreateLayoutHost();
            IServiceCollection services = null;

            // Act
            layoutHost.Get(
                services);

            // Assert
            Assert.True(false);
        }
    }
}
