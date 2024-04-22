using NSubstitute;
using Shopfloor.Features.Mechanic.Errands;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.PartModel;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Features.Mechanic.Errands.ErrandPartsList
{
    public class ErrandPartsListViewModelTests
    {
        private SelectedErrandStore subSelectedErrandStore;
        private PartStore subPartStore;
        private ErrandPartStore subErrandPartStore;

        public ErrandPartsListViewModelTests()
        {
            this.subSelectedErrandStore = Substitute.For<SelectedErrandStore>();
            this.subPartStore = Substitute.For<PartStore>();
            this.subErrandPartStore = Substitute.For<ErrandPartStore>();
        }

        private ErrandPartsListViewModel CreateViewModel()
        {
            return new ErrandPartsListViewModel(
                this.subSelectedErrandStore,
                this.subPartStore,
                this.subErrandPartStore);
        }

        [Fact]
        public async Task FillLists_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var viewModel = this.CreateViewModel();
            PartStore partsStore = null;
            ErrandPartStore errandPartStore = null;

            // Act
            await viewModel.FillLists(
                partsStore,
                errandPartStore);

            // Assert
            Assert.True(false);
        }
    }
}
