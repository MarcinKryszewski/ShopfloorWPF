using NSubstitute;
using Shopfloor.Features.Plannist;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Models.ErrandPartModel.Store;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Plannist.PartsOrders
{
    public class PartsOrdersViewModelTests
    {
        private SelectedRequestStore subSelectedRequestStore;
        private ErrandPartStore subErrandPartStore;

        public PartsOrdersViewModelTests()
        {
            this.subSelectedRequestStore = Substitute.For<SelectedRequestStore>();
            this.subErrandPartStore = Substitute.For<ErrandPartStore>();
        }

        private PartsOrdersViewModel CreateViewModel()
        {
            return new PartsOrdersViewModel(
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
