using NSubstitute;
using Shopfloor.Features.Mechanic.Errands;
using Shopfloor.Models.ErrandModel.Store;
using Shopfloor.Models.ErrandModel.Store.Combine;
using Shopfloor.Models.ErrandPartModel.Store.Combine;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Stores;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Mechanic.Errands.ErrandsList
{
    public class ErrandsListViewModelTests
    {
        private NavigationService subNavigationService;
        private CurrentUserStore subCurrentUserStore;
        private ErrandStore subErrandStore;
        private ErrandCombiner subErrandCombiner;
        private ErrandPartCombiner subErrandPartCombiner;

        public ErrandsListViewModelTests()
        {
            this.subNavigationService = Substitute.For<NavigationService>();
            this.subCurrentUserStore = Substitute.For<CurrentUserStore>();
            this.subErrandStore = Substitute.For<ErrandStore>();
            this.subErrandCombiner = Substitute.For<ErrandCombiner>();
            this.subErrandPartCombiner = Substitute.For<ErrandPartCombiner>();
        }

        private ErrandsListViewModel CreateViewModel()
        {
            return new ErrandsListViewModel(
                this.subNavigationService,
                this.subCurrentUserStore,
                this.subErrandStore,
                this.subErrandCombiner,
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
