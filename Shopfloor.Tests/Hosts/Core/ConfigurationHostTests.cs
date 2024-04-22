using NSubstitute;
using Shopfloor.Hosts.Core;
using System;
using Xunit;

namespace Shopfloor.Tests.Hosts.Core
{
    public class ConfigurationHostTests
    {


        public ConfigurationHostTests()
        {

        }

        private ConfigurationHost CreateConfigurationHost()
        {
            return new ConfigurationHost();
        }

        [Fact]
        public void Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var configurationHost = this.CreateConfigurationHost();
            IServiceCollection services = null;

            // Act
            configurationHost.Get(
                services);

            // Assert
            Assert.True(false);
        }
    }
}
