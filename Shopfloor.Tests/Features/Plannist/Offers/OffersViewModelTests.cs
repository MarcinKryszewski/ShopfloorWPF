using NSubstitute;
using Shopfloor.Features.Plannist;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Services.NavigationServices;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Plannist.Offers
{
    public class OffersViewModelTests
    {
        private SelectedRequestStore subSelectedRequestStore;
        private ErrandPartStore subErrandPartStore;
        private NavigationService subNavigationService;

        public OffersViewModelTests()
        {
            this.subSelectedRequestStore = Substitute.For<SelectedRequestStore>();
            this.subErrandPartStore = Substitute.For<ErrandPartStore>();
            this.subNavigationService = Substitute.For<NavigationService>();
        }

        private OffersViewModel CreateViewModel()
        {
            return new OffersViewModel(
                this.subSelectedRequestStore,
                this.subErrandPartStore,
                this.subNavigationService);
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
