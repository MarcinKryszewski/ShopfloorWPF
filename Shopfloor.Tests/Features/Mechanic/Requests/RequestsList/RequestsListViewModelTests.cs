using NSubstitute;
using Shopfloor.Features.Mechanic.Requests;
using Shopfloor.Features.Mechanic.Requests.Stores;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.ErrandPartModel.Store.Combine;
using Shopfloor.Stores;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Mechanic.Requests.RequestsList
{
    public class RequestsListViewModelTests
    {
        private CurrentUserStore subCurrentUserStore;
        private SelectedRequestStore subSelectedRequestStore;
        private ErrandPartCombiner subErrandPartCombiner;
        private ErrandPartStore subErrandPartStore;

        public RequestsListViewModelTests()
        {
            this.subCurrentUserStore = Substitute.For<CurrentUserStore>();
            this.subSelectedRequestStore = Substitute.For<SelectedRequestStore>();
            this.subErrandPartCombiner = Substitute.For<ErrandPartCombiner>();
            this.subErrandPartStore = Substitute.For<ErrandPartStore>();
        }

        private RequestsListViewModel CreateViewModel()
        {
            return new RequestsListViewModel(
                this.subCurrentUserStore,
                this.subSelectedRequestStore,
                this.subErrandPartCombiner,
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
