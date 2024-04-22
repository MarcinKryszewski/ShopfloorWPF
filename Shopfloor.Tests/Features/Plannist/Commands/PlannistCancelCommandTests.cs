using NSubstitute;
using Shopfloor.Features.Plannist.Commands;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Plannist.Commands
{
    public class PlannistCancelCommandTests
    {


        public PlannistCancelCommandTests()
        {

        }

        private PlannistCancelCommand CreatePlannistCancelCommand()
        {
            return new PlannistCancelCommand();
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var plannistCancelCommand = this.CreatePlannistCancelCommand();
            object? parameter = null;

            // Act
            plannistCancelCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
