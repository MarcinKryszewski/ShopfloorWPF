using NSubstitute;
using Shopfloor.Features.Mechanic.Requests.Commands;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Mechanic.Requests.Commands
{
    public class RequestDetailsCommandTests
    {


        public RequestDetailsCommandTests()
        {

        }

        private RequestDetailsCommand CreateRequestDetailsCommand()
        {
            return new RequestDetailsCommand();
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var requestDetailsCommand = this.CreateRequestDetailsCommand();
            object? parameter = null;

            // Act
            requestDetailsCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
