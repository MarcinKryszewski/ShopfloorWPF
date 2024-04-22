using NSubstitute;
using Shopfloor.Layout.Content;
using Shopfloor.Layout.MainWindow;
using Shopfloor.Layout.SidePanel;
using System;
using Xunit;

namespace Shopfloor.Tests.Layout.MainWindow
{
    public class MainWindowViewModelTests
    {
        private SidePanelViewModel subSidePanelViewModel;
        private ContentViewModel subContentViewModel;

        public MainWindowViewModelTests()
        {
            this.subSidePanelViewModel = Substitute.For<SidePanelViewModel>();
            this.subContentViewModel = Substitute.For<ContentViewModel>();
        }

        private MainWindowViewModel CreateViewModel()
        {
            return new MainWindowViewModel(
                this.subSidePanelViewModel,
                this.subContentViewModel);
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
