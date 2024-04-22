using NSubstitute;
using Shopfloor.Features.Mechanic.Errands;
using Shopfloor.Features.Mechanic.Errands.Commands;
using Shopfloor.Features.Mechanic.Errands.Stores;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Mechanic.Errands.Commands
{
    public class ErrandAddPartCommandTests
    {
        private ErrandPartsListViewModel subErrandPartsListViewModel;
        private SelectedErrandStore subSelectedErrandStore;

        public ErrandAddPartCommandTests()
        {
            this.subErrandPartsListViewModel = Substitute.For<ErrandPartsListViewModel>();
            this.subSelectedErrandStore = Substitute.For<SelectedErrandStore>();
        }

        private ErrandAddPartCommand CreateErrandAddPartCommand()
        {
            return new ErrandAddPartCommand(
                this.subErrandPartsListViewModel,
                this.subSelectedErrandStore);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandAddPartCommand = this.CreateErrandAddPartCommand();
            object? parameter = null;

            // Act
            errandAddPartCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
