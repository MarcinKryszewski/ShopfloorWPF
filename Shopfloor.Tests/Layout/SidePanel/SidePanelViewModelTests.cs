using NSubstitute;
using Shopfloor.Layout.SidePanel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Stores;
using System;
using Xunit;

namespace Shopfloor.Tests.Layout.SidePanel
{
    public class SidePanelViewModelTests
    {
        private INavigationService subNavigationService;
        private CurrentUserStore subCurrentUserStore;
        private IServiceProvider subServiceProvider;

        public SidePanelViewModelTests()
        {
            this.subNavigationService = Substitute.For<INavigationService>();
            this.subCurrentUserStore = Substitute.For<CurrentUserStore>();
            this.subServiceProvider = Substitute.For<IServiceProvider>();
        }

        private SidePanelViewModel CreateViewModel()
        {
            return new SidePanelViewModel(
                this.subNavigationService,
                this.subCurrentUserStore,
                this.subServiceProvider);
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
