using NSubstitute;
using Shopfloor.Layout.Content;
using Shopfloor.Layout.TopPanel;
using Shopfloor.Shared.Stores;
using System;
using Xunit;

namespace Shopfloor.Tests.Layout.Content
{
    public class ContentViewModelTests
    {
        private TopPanelViewModel subTopPanelViewModel;
        private INavigationStore subNavigationStore;

        public ContentViewModelTests()
        {
            this.subTopPanelViewModel = Substitute.For<TopPanelViewModel>();
            this.subNavigationStore = Substitute.For<INavigationStore>();
        }

        private ContentViewModel CreateViewModel()
        {
            return new ContentViewModel(
                this.subTopPanelViewModel,
                this.subNavigationStore);
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
