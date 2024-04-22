using NSubstitute;
using Shopfloor.Models.ReservationModel.Store.Combine;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.ReservationModel.Store.Combine
{
    public class ReservationCombinerTests
    {


        public ReservationCombinerTests()
        {

        }

        private ReservationCombiner CreateReservationCombiner()
        {
            return new ReservationCombiner();
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var reservationCombiner = this.CreateReservationCombiner();
            bool shouldForce = false;

            // Act
            await reservationCombiner.Combine(
                shouldForce);

            // Assert
            Assert.True(false);
        }
    }
}
