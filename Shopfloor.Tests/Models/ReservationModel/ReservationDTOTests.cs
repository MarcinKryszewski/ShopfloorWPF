using NSubstitute;
using Shopfloor.Models.ReservationModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ReservationModel
{
    public class ReservationDTOTests
    {


        public ReservationDTOTests()
        {

        }

        private ReservationDTO CreateReservationDTO()
        {
            return new ReservationDTO();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var reservationDTO = this.CreateReservationDTO();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
