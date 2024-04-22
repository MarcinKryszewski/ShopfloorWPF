using NSubstitute;
using Shopfloor.Features.Mechanic.Errands;
using Shopfloor.Features.Mechanic.Errands.Commands;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Stores;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Mechanic.Errands.Commands
{
    public class ErrandEditCommandTests
    {
        private ErrandEditViewModel subErrandEditViewModel;
        private CurrentUserStore subCurrentUserStore;
        private SelectedErrandStore subSelectedErrandStore;
        private ErrandProvider subErrandProvider;
        private ErrandPartProvider subErrandPartProvider;
        private ErrandStatusProvider subErrandStatusProvider;
        private ErrandPartStore subErrandPartStore;
        private ErrandPartStatusProvider subErrandPartStatusProvider;

        public ErrandEditCommandTests()
        {
            this.subErrandEditViewModel = Substitute.For<ErrandEditViewModel>();
            this.subCurrentUserStore = Substitute.For<CurrentUserStore>();
            this.subSelectedErrandStore = Substitute.For<SelectedErrandStore>();
            this.subErrandProvider = Substitute.For<ErrandProvider>();
            this.subErrandPartProvider = Substitute.For<ErrandPartProvider>();
            this.subErrandStatusProvider = Substitute.For<ErrandStatusProvider>();
            this.subErrandPartStore = Substitute.For<ErrandPartStore>();
            this.subErrandPartStatusProvider = Substitute.For<ErrandPartStatusProvider>();
        }

        private ErrandEditCommand CreateErrandEditCommand()
        {
            return new ErrandEditCommand(
                this.subErrandEditViewModel,
                this.subCurrentUserStore,
                this.subSelectedErrandStore,
                this.subErrandProvider,
                this.subErrandPartProvider,
                this.subErrandStatusProvider,
                this.subErrandPartStore,
                this.subErrandPartStatusProvider);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandEditCommand = this.CreateErrandEditCommand();
            object? parameter = null;

            // Act
            errandEditCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
