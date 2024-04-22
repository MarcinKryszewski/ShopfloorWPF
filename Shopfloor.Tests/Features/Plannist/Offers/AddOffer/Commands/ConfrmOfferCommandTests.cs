using NSubstitute;
using Shopfloor.Features.Plannist;
using Shopfloor.Features.Plannist.Offers.AddOffer;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Stores;
using System;
using ToastNotifications;
using Xunit;

namespace Shopfloor.Tests.Features.Plannist.Offers.AddOffer.Commands
{
    public class ConfrmOfferCommandTests
    {
        private SelectedRequestStore subSelectedRequestStore;
        private AddOfferViewModel subAddOfferViewModel;
        private CurrentUserStore subCurrentUserStore;
        private ErrandPartProvider subErrandPartProvider;
        private ErrandPartStatusProvider subErrandPartStatusProvider;
        private ErrandPartStatusStore subErrandPartStatusStore;
        private Notifier subNotifier;

        public ConfrmOfferCommandTests()
        {
            this.subSelectedRequestStore = Substitute.For<SelectedRequestStore>();
            this.subAddOfferViewModel = Substitute.For<AddOfferViewModel>();
            this.subCurrentUserStore = Substitute.For<CurrentUserStore>();
            this.subErrandPartProvider = Substitute.For<ErrandPartProvider>();
            this.subErrandPartStatusProvider = Substitute.For<ErrandPartStatusProvider>();
            this.subErrandPartStatusStore = Substitute.For<ErrandPartStatusStore>();
            this.subNotifier = Substitute.For<Notifier>();
        }

        private ConfrmOfferCommand CreateConfrmOfferCommand()
        {
            return new ConfrmOfferCommand(
                this.subSelectedRequestStore,
                this.subAddOfferViewModel,
                this.subCurrentUserStore,
                this.subErrandPartProvider,
                this.subErrandPartStatusProvider,
                this.subErrandPartStatusStore,
                this.subNotifier);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var confrmOfferCommand = this.CreateConfrmOfferCommand();
            object? parameter = null;

            // Act
            confrmOfferCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
