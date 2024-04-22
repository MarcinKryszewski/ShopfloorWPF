using NSubstitute;
using Shopfloor.Features.Manager.OrdersToApprove;
using Shopfloor.Features.Manager.Stores;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Services.NavigationServices;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Manager.OrdersToApprove
{
    public class OrdersToApproveViewModelTests
    {
        private NavigationService subNavigationService;
        private SelectedRequestStore subSelectedRequestStore;
        private ErrandPartStore subErrandPartStore;

        public OrdersToApproveViewModelTests()
        {
            this.subNavigationService = Substitute.For<NavigationService>();
            this.subSelectedRequestStore = Substitute.For<SelectedRequestStore>();
            this.subErrandPartStore = Substitute.For<ErrandPartStore>();
        }

        private OrdersToApproveViewModel CreateViewModel()
        {
            return new OrdersToApproveViewModel(
                this.subNavigationService,
                this.subSelectedRequestStore,
                this.subErrandPartStore);
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
