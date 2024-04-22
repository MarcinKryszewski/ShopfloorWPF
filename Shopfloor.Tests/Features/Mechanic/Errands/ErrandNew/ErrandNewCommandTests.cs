using NSubstitute;
using Shopfloor.Features.Mechanic.Errands;
using Shopfloor.Features.Mechanic.Errands.ErrandNew;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Stores;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Mechanic.Errands.ErrandNew
{
    public class ErrandNewCommandTests
    {
        private ErrandNewViewModel subErrandNewViewModel;
        private CurrentUserStore subCurrentUserStore;
        private SelectedErrandStore subSelectedErrandStore;

        public ErrandNewCommandTests()
        {
            this.subErrandNewViewModel = Substitute.For<ErrandNewViewModel>();
            this.subCurrentUserStore = Substitute.For<CurrentUserStore>();
            this.subSelectedErrandStore = Substitute.For<SelectedErrandStore>();
        }

        private ErrandNewCommand CreateErrandNewCommand()
        {
            return new ErrandNewCommand(
                this.subErrandNewViewModel,
                this.subCurrentUserStore,
                this.subSelectedErrandStore);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandNewCommand = this.CreateErrandNewCommand();
            object? parameter = null;

            // Act
            errandNewCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
