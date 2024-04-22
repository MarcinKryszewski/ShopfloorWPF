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
using Shopfloor.Models.UserModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Stores;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Mechanic.Errands.ErrandNew
{
    public class ErrandNewViewModelTests
    {
        private ErrandPartsListViewModel subErrandPartsListViewModel;
        private NavigationService subNavigationService;
        private SelectedErrandStore subSelectedErrandStore;
        private CurrentUserStore subCurrentUserStore;
        private MachineStore subMachineStore;
        private ErrandTypeStore subErrandTypeStore;
        private UserStore subUserStore;
        private ErrandProvider subErrandProvider;
        private ErrandPartProvider subErrandPartProvider;
        private ErrandStatusProvider subErrandStatusProvider;
        private ErrandPartStatusProvider subErrandPartStatusProvider;
        private ErrandPartStatusStore subErrandPartStatusStore;
        private ErrandPartStore subErrandPartStore;
        private ErrandStatusStore subErrandStatusStore;
        private ErrandStore subErrandStore;

        public ErrandNewViewModelTests()
        {
            this.subErrandPartsListViewModel = Substitute.For<ErrandPartsListViewModel>();
            this.subNavigationService = Substitute.For<NavigationService>();
            this.subSelectedErrandStore = Substitute.For<SelectedErrandStore>();
            this.subCurrentUserStore = Substitute.For<CurrentUserStore>();
            this.subMachineStore = Substitute.For<MachineStore>();
            this.subErrandTypeStore = Substitute.For<ErrandTypeStore>();
            this.subUserStore = Substitute.For<UserStore>();
            this.subErrandProvider = Substitute.For<ErrandProvider>();
            this.subErrandPartProvider = Substitute.For<ErrandPartProvider>();
            this.subErrandStatusProvider = Substitute.For<ErrandStatusProvider>();
            this.subErrandPartStatusProvider = Substitute.For<ErrandPartStatusProvider>();
            this.subErrandPartStatusStore = Substitute.For<ErrandPartStatusStore>();
            this.subErrandPartStore = Substitute.For<ErrandPartStore>();
            this.subErrandStatusStore = Substitute.For<ErrandStatusStore>();
            this.subErrandStore = Substitute.For<ErrandStore>();
        }

        private ErrandNewViewModel CreateViewModel()
        {
            return new ErrandNewViewModel(
                this.subErrandPartsListViewModel,
                this.subNavigationService,
                this.subSelectedErrandStore,
                this.subCurrentUserStore,
                this.subMachineStore,
                this.subErrandTypeStore,
                this.subUserStore,
                this.subErrandProvider,
                this.subErrandPartProvider,
                this.subErrandStatusProvider,
                this.subErrandPartStatusProvider,
                this.subErrandPartStatusStore,
                this.subErrandPartStore,
                this.subErrandStatusStore,
                this.subErrandStore);
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
