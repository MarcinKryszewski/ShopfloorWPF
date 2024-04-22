using NSubstitute;
using Shopfloor.Features.Mechanic.Requests.Commands;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Mechanic.Requests.Commands
{
    public class RequestEditCommandTests
    {


        public RequestEditCommandTests()
        {

        }

        private RequestEditCommand CreateRequestEditCommand()
        {
            return new RequestEditCommand();
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var requestEditCommand = this.CreateRequestEditCommand();
            object? parameter = null;

            // Act
            requestEditCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
