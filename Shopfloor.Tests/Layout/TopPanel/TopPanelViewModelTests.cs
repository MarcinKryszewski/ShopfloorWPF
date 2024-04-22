using NSubstitute;
using Shopfloor.Layout.TopPanel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Stores;
using System;
using Xunit;

namespace Shopfloor.Tests.Layout.TopPanel
{
    public class TopPanelViewModelTests
    {
        private INavigationService subNavigationService;
        private CurrentUserStore subCurrentUserStore;

        public TopPanelViewModelTests()
        {
            this.subNavigationService = Substitute.For<INavigationService>();
            this.subCurrentUserStore = Substitute.For<CurrentUserStore>();
        }

        private TopPanelViewModel CreateViewModel()
        {
            return new TopPanelViewModel(
                this.subNavigationService,
                this.subCurrentUserStore);
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
