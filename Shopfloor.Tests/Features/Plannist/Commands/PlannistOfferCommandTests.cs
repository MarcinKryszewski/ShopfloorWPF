using NSubstitute;
using Shopfloor.Features.Plannist.Commands;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Plannist.Commands
{
    public class PlannistOfferCommandTests
    {


        public PlannistOfferCommandTests()
        {

        }

        private PlannistOfferCommand CreatePlannistOfferCommand()
        {
            return new PlannistOfferCommand();
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var plannistOfferCommand = this.CreatePlannistOfferCommand();
            object? parameter = null;

            // Act
            plannistOfferCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
