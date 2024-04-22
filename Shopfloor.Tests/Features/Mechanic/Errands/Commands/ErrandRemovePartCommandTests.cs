using NSubstitute;
using Shopfloor.Features.Mechanic.Errands;
using Shopfloor.Features.Mechanic.Errands.Commands;
using Shopfloor.Features.Mechanic.Errands.Stores;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Mechanic.Errands.Commands
{
    public class ErrandRemovePartCommandTests
    {
        private ErrandPartsListViewModel subErrandPartsListViewModel;
        private SelectedErrandStore subSelectedErrandStore;

        public ErrandRemovePartCommandTests()
        {
            this.subErrandPartsListViewModel = Substitute.For<ErrandPartsListViewModel>();
            this.subSelectedErrandStore = Substitute.For<SelectedErrandStore>();
        }

        private ErrandRemovePartCommand CreateErrandRemovePartCommand()
        {
            return new ErrandRemovePartCommand(
                this.subErrandPartsListViewModel,
                this.subSelectedErrandStore);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandRemovePartCommand = this.CreateErrandRemovePartCommand();
            object? parameter = null;

            // Act
            errandRemovePartCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
