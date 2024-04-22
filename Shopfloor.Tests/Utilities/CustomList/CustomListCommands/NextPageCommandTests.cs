using NSubstitute;
using Shopfloor.Utilities.CustomList;
using Shopfloor.Utilities.CustomList.CustomListCommands;
using System;
using Xunit;

namespace Shopfloor.Tests.Utilities.CustomList.CustomListCommands
{
    public class NextPageCommandTests
    {
        private SearchableModelList subSearchableModelList;

        public NextPageCommandTests()
        {
            this.subSearchableModelList = Substitute.For<SearchableModelList>();
        }

        private NextPageCommand CreateNextPageCommand()
        {
            return new NextPageCommand(
                this.subSearchableModelList);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var nextPageCommand = this.CreateNextPageCommand();
            object? parameter = null;

            // Act
            nextPageCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
