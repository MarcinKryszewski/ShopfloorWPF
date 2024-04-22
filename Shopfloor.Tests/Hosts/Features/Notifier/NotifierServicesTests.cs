using NSubstitute;
using Shopfloor.Hosts.Features;
using System;
using Xunit;

namespace Shopfloor.Tests.Hosts.Features.Notifier
{
    public class NotifierServicesTests
    {


        public NotifierServicesTests()
        {

        }

        private NotifierServices CreateNotifierServices()
        {
            return new NotifierServices();
        }

        [Fact]
        public void Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var notifierServices = this.CreateNotifierServices();
            IServiceCollection services = null;

            // Act
            notifierServices.Get(
                services);

            // Assert
            Assert.True(false);
        }
    }
}
