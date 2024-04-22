using Shopfloor.Utilities.CustomList;
using Shopfloor.Utilities.CustomList.CustomListCommands;
using System;
using Xunit;

namespace Shopfloor.Tests.Utilities.CustomList.CustomListCommands
{
    public class PreviousPageCommandTests
    {
        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var previousPageCommand = new PreviousPageCommand(TODO);
            object? parameter = null;

            // Act
            previousPageCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
