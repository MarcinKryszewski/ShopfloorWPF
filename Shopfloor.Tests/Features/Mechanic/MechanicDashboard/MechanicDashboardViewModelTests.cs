using NSubstitute;
using Shopfloor.Features.Mechanic;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Mechanic.MechanicDashboard
{
    public class MechanicDashboardViewModelTests
    {


        public MechanicDashboardViewModelTests()
        {

        }

        private MechanicDashboardViewModel CreateViewModel()
        {
            return new MechanicDashboardViewModel();
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
