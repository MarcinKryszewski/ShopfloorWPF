using NSubstitute;
using Shopfloor.Features.Manager.ManagerDashboard;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Manager.ManagerDashboard
{
    public class ManagerDashboardViewModelTests
    {


        public ManagerDashboardViewModelTests()
        {

        }

        private ManagerDashboardViewModel CreateViewModel()
        {
            return new ManagerDashboardViewModel();
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
