using NSubstitute;
using Shopfloor.Hosts.Features;
using System;
using Xunit;

namespace Shopfloor.Tests.Hosts.Features
{
    public class FeaturesHostTests
    {


        public FeaturesHostTests()
        {

        }

        private FeaturesHost CreateFeaturesHost()
        {
            return new FeaturesHost();
        }

        [Fact]
        public void Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var featuresHost = this.CreateFeaturesHost();
            IServiceCollection services = null;

            // Act
            featuresHost.Get(
                services);

            // Assert
            Assert.True(false);
        }
    }
}
