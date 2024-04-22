using NSubstitute;
using Shopfloor.Features.Manager.OrderApprove;
using Shopfloor.Features.Manager.Stores;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Stores;
using System;
using ToastNotifications;
using Xunit;

namespace Shopfloor.Tests.Features.Manager.OrderApprove
{
    public class OrderApproveViewModelTests
    {
        private SelectedRequestStore subSelectedRequestStore;
        private NavigationService subNavigationService;
        private ErrandPartStatusStore subErrandPartStatusStore;
        private Notifier subNotifier;
        private SelectedRequestStore subSelectedRequestStore;
        private CurrentUserStore subCurrentUserStore;
        private ErrandPartStatusProvider subErrandPartStatusProvider;

        public OrderApproveViewModelTests()
        {
            this.subSelectedRequestStore = Substitute.For<SelectedRequestStore>();
            this.subNavigationService = Substitute.For<NavigationService>();
            this.subErrandPartStatusStore = Substitute.For<ErrandPartStatusStore>();
            this.subNotifier = Substitute.For<Notifier>();
            this.subSelectedRequestStore = Substitute.For<SelectedRequestStore>();
            this.subCurrentUserStore = Substitute.For<CurrentUserStore>();
            this.subErrandPartStatusProvider = Substitute.For<ErrandPartStatusProvider>();
        }

        private OrderApproveViewModel CreateViewModel()
        {
            return new OrderApproveViewModel(
                this.subSelectedRequestStore,
                this.subNavigationService,
                this.subErrandPartStatusStore,
                this.subNotifier,
                this.subSelectedRequestStore,
                this.subCurrentUserStore,
                this.subErrandPartStatusProvider);
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
