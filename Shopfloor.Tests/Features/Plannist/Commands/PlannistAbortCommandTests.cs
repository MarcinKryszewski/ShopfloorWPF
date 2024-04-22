using NSubstitute;
using Shopfloor.Features.Plannist.Commands;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Models.ErrandPartStatusModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Plannist.Commands
{
    public class PlannistAbortCommandTests
    {
        private SelectedRequestStore subSelectedRequestStore;
        private ErrandPartStatusProvider subErrandPartStatusProvider;

        public PlannistAbortCommandTests()
        {
            this.subSelectedRequestStore = Substitute.For<SelectedRequestStore>();
            this.subErrandPartStatusProvider = Substitute.For<ErrandPartStatusProvider>();
        }

        private PlannistAbortCommand CreatePlannistAbortCommand()
        {
            return new PlannistAbortCommand(
                this.subSelectedRequestStore,
                this.subErrandPartStatusProvider);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var plannistAbortCommand = this.CreatePlannistAbortCommand();
            object? parameter = null;

            // Act
            plannistAbortCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
