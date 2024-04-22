using NSubstitute;
using Shopfloor.Features.Plannist;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.ErrandPartModel.Store.Combine;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Services.NotificationServices;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Plannist.PlannistPartsList
{
    public class PlannistPartsListViewModelTests
    {
        private INotifier subNotifier;
        private SelectedRequestStore subSelectedRequestStore;
        private ErrandPartStatusProvider subErrandPartStatusProvider;
        private ErrandPartStore subErrandPartStore;
        private ErrandPartCombiner subErrandPartCombiner;

        public PlannistPartsListViewModelTests()
        {
            this.subNotifier = Substitute.For<INotifier>();
            this.subSelectedRequestStore = Substitute.For<SelectedRequestStore>();
            this.subErrandPartStatusProvider = Substitute.For<ErrandPartStatusProvider>();
            this.subErrandPartStore = Substitute.For<ErrandPartStore>();
            this.subErrandPartCombiner = Substitute.For<ErrandPartCombiner>();
        }

        private PlannistPartsListViewModel CreateViewModel()
        {
            return new PlannistPartsListViewModel(
                this.subNotifier,
                this.subSelectedRequestStore,
                this.subErrandPartStatusProvider,
                this.subErrandPartStore,
                this.subErrandPartCombiner);
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
