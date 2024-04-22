using NSubstitute;
using Shopfloor.Features.Mechanic.Errands.Commands;
using Shopfloor.Features.Mechanic.Errands.Interfaces;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Mechanic.Errands.Commands
{
    public class PrioritySetCommandTests
    {
        private IErrandPriority subErrandPriority;

        public PrioritySetCommandTests()
        {
            this.subErrandPriority = Substitute.For<IErrandPriority>();
        }

        private PrioritySetCommand CreatePrioritySetCommand()
        {
            return new PrioritySetCommand(
                this.subErrandPriority);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var prioritySetCommand = this.CreatePrioritySetCommand();
            object? parameter = null;

            // Act
            prioritySetCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
