using NSubstitute;
using Shopfloor.Features.Mechanic.Errands;
using Shopfloor.Features.Mechanic.Errands.Commands;
using Shopfloor.Features.Mechanic.Errands.Stores;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Mechanic.Errands.Commands
{
    public class ErrandSetCommandTests
    {
        private ErrandsListViewModel subErrandsListViewModel;
        private SelectedErrandStore subSelectedErrandStore;

        public ErrandSetCommandTests()
        {
            this.subErrandsListViewModel = Substitute.For<ErrandsListViewModel>();
            this.subSelectedErrandStore = Substitute.For<SelectedErrandStore>();
        }

        private ErrandSetCommand CreateErrandSetCommand()
        {
            return new ErrandSetCommand(
                this.subErrandsListViewModel,
                this.subSelectedErrandStore);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandSetCommand = this.CreateErrandSetCommand();
            object? parameter = null;

            // Act
            errandSetCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
