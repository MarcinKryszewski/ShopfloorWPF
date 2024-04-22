using NSubstitute;
using Shopfloor.Features.Mechanic.Requests;
using Shopfloor.Features.Mechanic.Requests.Stores;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Services.NavigationServices;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Mechanic.Requests.RequestsDetails
{
    public class RequestsDetailsViewModelTests
    {
        private SelectedRequestStore subSelectedRequestStore;
        private ErrandPartStore subErrandPartStore;
        private NavigationService subNavigationService;

        public RequestsDetailsViewModelTests()
        {
            this.subSelectedRequestStore = Substitute.For<SelectedRequestStore>();
            this.subErrandPartStore = Substitute.For<ErrandPartStore>();
            this.subNavigationService = Substitute.For<NavigationService>();
        }

        private RequestsDetailsViewModel CreateViewModel()
        {
            return new RequestsDetailsViewModel(
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
