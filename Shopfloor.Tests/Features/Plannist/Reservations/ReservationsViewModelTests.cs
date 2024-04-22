using NSubstitute;
using Shopfloor.Features.Plannist;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Plannist.Reservations
{
    public class ReservationsViewModelTests
    {


        public ReservationsViewModelTests()
        {

        }

        private ReservationsViewModel CreateViewModel()
        {
            return new ReservationsViewModel();
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
