using NSubstitute;
using Shopfloor.Services.NotificationServices;
using System;
using Xunit;

namespace Shopfloor.Tests.Services.NotificationServices
{
    public class NotifierCommandTests
    {
        private INotifier subNotifier;

        public NotifierCommandTests()
        {
            this.subNotifier = Substitute.For<INotifier>();
        }

        private NotifierCommand CreateNotifierCommand()
        {
            return new NotifierCommand(
                this.subNotifier,
                TODO,
                TODO);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var notifierCommand = this.CreateNotifierCommand();
            object? parameter = null;

            // Act
            notifierCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
