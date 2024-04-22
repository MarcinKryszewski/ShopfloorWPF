using NSubstitute;
using Shopfloor.Models.ReservationModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ReservationModel
{
    public class ReservationTests
    {


        public ReservationTests()
        {

        }

        private Reservation CreateReservation()
        {
            return new Reservation();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var reservation = this.CreateReservation();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
