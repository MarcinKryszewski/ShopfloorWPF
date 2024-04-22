using NSubstitute;
using Shopfloor.Features.Plannist.Commands;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Services.NotificationServices;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Plannist.Commands
{
    public class PlannistConfirmCommandTests
    {
        private SelectedRequestStore subSelectedRequestStore;
        private INotifier subNotifier;
        private ErrandPartStatusProvider subErrandPartStatusProvider;

        public PlannistConfirmCommandTests()
        {
            this.subSelectedRequestStore = Substitute.For<SelectedRequestStore>();
            this.subNotifier = Substitute.For<INotifier>();
            this.subErrandPartStatusProvider = Substitute.For<ErrandPartStatusProvider>();
        }

        private PlannistConfirmCommand CreatePlannistConfirmCommand()
        {
            return new PlannistConfirmCommand(
                this.subSelectedRequestStore,
                this.subNotifier,
                this.subErrandPartStatusProvider);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var plannistConfirmCommand = this.CreatePlannistConfirmCommand();
            object? parameter = null;

            // Act
            plannistConfirmCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
