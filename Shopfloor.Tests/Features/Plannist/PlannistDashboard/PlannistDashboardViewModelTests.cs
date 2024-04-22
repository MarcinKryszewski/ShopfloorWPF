using NSubstitute;
using Shopfloor.Features.Plannist;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Plannist.PlannistDashboard
{
    public class PlannistDashboardViewModelTests
    {


        public PlannistDashboardViewModelTests()
        {

        }

        private PlannistDashboardViewModel CreateViewModel()
        {
            return new PlannistDashboardViewModel();
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
