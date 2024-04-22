using NSubstitute;
using Shopfloor.Layout.TopPanel.Commands;
using Shopfloor.Shared.Commands;
using Shopfloor.Stores;
using System;
using Xunit;

namespace Shopfloor.Tests.Layout.TopPanel.Commands
{
    public class LogoutCommandTests
    {
        private CurrentUserStore subCurrentUserStore;
        private RelayCommand subRelayCommand;

        public LogoutCommandTests()
        {
            this.subCurrentUserStore = Substitute.For<CurrentUserStore>();
            this.subRelayCommand = Substitute.For<RelayCommand>();
        }

        private LogoutCommand CreateLogoutCommand()
        {
            return new LogoutCommand(
                this.subCurrentUserStore,
                this.subRelayCommand);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var logoutCommand = this.CreateLogoutCommand();
            object? parameter = null;

            // Act
            logoutCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
