using NSubstitute;
using Shopfloor.Features.Plannist.Commands;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Plannist.Commands
{
    public class PlannistDetailsCommandTests
    {


        public PlannistDetailsCommandTests()
        {

        }

        private PlannistDetailsCommand CreatePlannistDetailsCommand()
        {
            return new PlannistDetailsCommand();
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var plannistDetailsCommand = this.CreatePlannistDetailsCommand();
            object? parameter = null;

            // Act
            plannistDetailsCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
