using NSubstitute;
using Shopfloor.Services.NotificationServices;
using System;
using Xunit;

namespace Shopfloor.Tests.Services.NotificationServices
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
        public void TestMethod1()
        {
            // Arrange
            var notifierServices = this.CreateNotifierServices();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
