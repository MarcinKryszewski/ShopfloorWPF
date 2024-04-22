using NSubstitute;
using Shopfloor.Features.Mechanic.Errands;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandModel.Store;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Stores;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Features.Mechanic.Errands.ErrandEdit
{
    public class ErrandEditViewModelTests
    {
        private NavigationService subNavigationService;
        private ErrandPartsListViewModel subErrandPartsListViewModel;
        private SelectedErrandStore subSelectedErrandStore;
        private CurrentUserStore subCurrentUserStore;
        private ErrandStore subErrandStore;
        private MachineStore subMachineStore;
        private UserStore subUserStore;
        private ErrandTypeStore subErrandTypeStore;
        private ErrandPartStore subErrandPartStore;
        private PartStore subPartStore;
        private ErrandProvider subErrandProvider;
        private ErrandPartProvider subErrandPartProvider;
        private ErrandStatusProvider subErrandStatusProvider;
        private ErrandPartStatusProvider subErrandPartStatusProvider;

        public ErrandEditViewModelTests()
        {
            this.subNavigationService = Substitute.For<NavigationService>();
            this.subErrandPartsListViewModel = Substitute.For<ErrandPartsListViewModel>();
            this.subSelectedErrandStore = Substitute.For<SelectedErrandStore>();
            this.subCurrentUserStore = Substitute.For<CurrentUserStore>();
            this.subErrandStore = Substitute.For<ErrandStore>();
            this.subMachineStore = Substitute.For<MachineStore>();
            this.subUserStore = Substitute.For<UserStore>();
            this.subErrandTypeStore = Substitute.For<ErrandTypeStore>();
            this.subErrandPartStore = Substitute.For<ErrandPartStore>();
            this.subPartStore = Substitute.For<PartStore>();
            this.subErrandProvider = Substitute.For<ErrandProvider>();
            this.subErrandPartProvider = Substitute.For<ErrandPartProvider>();
            this.subErrandStatusProvider = Substitute.For<ErrandStatusProvider>();
            this.subErrandPartStatusProvider = Substitute.For<ErrandPartStatusProvider>();
        }

        private ErrandEditViewModel CreateViewModel()
        {
            return new ErrandEditViewModel(
                this.subNavigationService,
                this.subErrandPartsListViewModel,
                this.subSelectedErrandStore,
                this.subCurrentUserStore,
                this.subErrandStore,
                this.subMachineStore,
                this.subUserStore,
                this.subErrandTypeStore,
                this.subErrandPartStore,
                this.subPartStore,
                this.subErrandProvider,
                this.subErrandPartProvider,
                this.subErrandStatusProvider,
                this.subErrandPartStatusProvider);
        }

        [Fact]
        public async Task RefreshData_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var viewModel = this.CreateViewModel();

            // Act
            await viewModel.RefreshData();

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void SetupParts_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var viewModel = this.CreateViewModel();
            ErrandPartStore errandPartStore = null;

            // Act
            viewModel.SetupParts(
                errandPartStore);

            // Assert
            Assert.True(false);
        }
    }
}
