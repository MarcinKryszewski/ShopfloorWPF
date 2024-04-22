using NSubstitute;
using Shopfloor.Features.Plannist;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Stores;
using System;
using ToastNotifications;
using Xunit;

namespace Shopfloor.Tests.Features.Plannist.Offers.AddOffer
{
    public class AddOfferViewModelTests
    {
        private NavigationService subNavigationService;
        private SelectedRequestStore subSelectedRequestStore;
        private ErrandPartStore subErrandPartStore;
        private SelectedRequestStore subSelectedRequestStore;
        private AddOfferViewModel subAddOfferViewModel;
        private CurrentUserStore subCurrentUserStore;
        private ErrandPartProvider subErrandPartProvider;
        private ErrandPartStatusProvider subErrandPartStatusProvider;
        private ErrandPartStatusStore subErrandPartStatusStore;
        private Notifier subNotifier;

        public AddOfferViewModelTests()
        {
            this.subNavigationService = Substitute.For<NavigationService>();
            this.subSelectedRequestStore = Substitute.For<SelectedRequestStore>();
            this.subErrandPartStore = Substitute.For<ErrandPartStore>();
            this.subSelectedRequestStore = Substitute.For<SelectedRequestStore>();
            this.subAddOfferViewModel = Substitute.For<AddOfferViewModel>();
            this.subCurrentUserStore = Substitute.For<CurrentUserStore>();
            this.subErrandPartProvider = Substitute.For<ErrandPartProvider>();
            this.subErrandPartStatusProvider = Substitute.For<ErrandPartStatusProvider>();
            this.subErrandPartStatusStore = Substitute.For<ErrandPartStatusStore>();
            this.subNotifier = Substitute.For<Notifier>();
        }

        private AddOfferViewModel CreateViewModel()
        {
            return new AddOfferViewModel(
                this.subNavigationService,
                this.subSelectedRequestStore,
                this.subErrandPartStore,
                this.subSelectedRequestStore,
                this.subAddOfferViewModel,
                this.subCurrentUserStore,
                this.subErrandPartProvider,
                this.subErrandPartStatusProvider,
                this.subErrandPartStatusStore,
                this.subNotifier);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var viewModel = this.CreateViewModel();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
