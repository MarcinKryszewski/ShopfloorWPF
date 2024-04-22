using NSubstitute;
using Shopfloor.Features.Admin.Parts;
using Shopfloor.Features.Admin.Parts.Stores;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Services.NavigationServices;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Parts.PartsList
{
    public class PartsListViewModelTests
    {
        private NavigationService subNavigationService;
        private PartTypeStore subPartTypeStore;
        private SuppliersStore subSuppliersStore;
        private PartStore subPartStore;
        private SelectedPartStore subSelectedPartStore;

        public PartsListViewModelTests()
        {
            this.subNavigationService = Substitute.For<NavigationService>();
            this.subPartTypeStore = Substitute.For<PartTypeStore>();
            this.subSuppliersStore = Substitute.For<SuppliersStore>();
            this.subPartStore = Substitute.For<PartStore>();
            this.subSelectedPartStore = Substitute.For<SelectedPartStore>();
        }

        private PartsListViewModel CreateViewModel()
        {
            return new PartsListViewModel(
                this.subNavigationService,
                this.subPartTypeStore,
                this.subSuppliersStore,
                this.subPartStore,
                this.subSelectedPartStore);
        }

        [Fact]
        public async Task LoadData_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var viewModel = this.CreateViewModel();

            // Act
            await viewModel.LoadData();

            // Assert
            Assert.True(false);
        }
    }
}
