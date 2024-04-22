using NSubstitute;
using Shopfloor.Services.NotificationServices;
using System;
using Xunit;

namespace Shopfloor.Tests.Services.NotificationServices
{
    public class NotifierSetupTests
    {


        public NotifierSetupTests()
        {

        }

        private NotifierSetup CreateNotifierSetup()
        {
            return new NotifierSetup();
        }

        [Fact]
        public void GetSetup_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var notifierSetup = this.CreateNotifierSetup();

            // Act
            var result = notifierSetup.GetSetup();

            // Assert
            Assert.True(false);
        }
    }
}
