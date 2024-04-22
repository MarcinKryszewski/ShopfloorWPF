using NSubstitute;
using Shopfloor.Features.Manager.Commands;
using Shopfloor.Features.Manager.OrderApprove;
using Shopfloor.Features.Manager.Stores;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Stores;
using System;
using ToastNotifications;
using Xunit;

namespace Shopfloor.Tests.Features.Manager.Commands
{
    public class ApproveOrderCommandTests
    {
        private NavigationService subNavigationService;
        private ErrandPartStatusStore subErrandPartStatusStore;
        private Notifier subNotifier;
        private SelectedRequestStore subSelectedRequestStore;
        private OrderApproveViewModel subOrderApproveViewModel;
        private CurrentUserStore subCurrentUserStore;
        private ErrandPartStatusProvider subErrandPartStatusProvider;

        public ApproveOrderCommandTests()
        {
            this.subNavigationService = Substitute.For<NavigationService>();
            this.subErrandPartStatusStore = Substitute.For<ErrandPartStatusStore>();
            this.subNotifier = Substitute.For<Notifier>();
            this.subSelectedRequestStore = Substitute.For<SelectedRequestStore>();
            this.subOrderApproveViewModel = Substitute.For<OrderApproveViewModel>();
            this.subCurrentUserStore = Substitute.For<CurrentUserStore>();
            this.subErrandPartStatusProvider = Substitute.For<ErrandPartStatusProvider>();
        }

        private ApproveOrderCommand CreateApproveOrderCommand()
        {
            return new ApproveOrderCommand(
                this.subNavigationService,
                this.subErrandPartStatusStore,
                this.subNotifier,
                this.subSelectedRequestStore,
                this.subOrderApproveViewModel,
                this.subCurrentUserStore,
                this.subErrandPartStatusProvider);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var approveOrderCommand = this.CreateApproveOrderCommand();
            object? parameter = null;

            // Act
            approveOrderCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
