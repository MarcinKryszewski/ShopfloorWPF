using NSubstitute;
using Shopfloor.Models.ReservationModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ReservationModel.Store
{
    public class ReservationStoreTests
    {
        private ReservationProvider subReservationProvider;

        public ReservationStoreTests()
        {
            this.subReservationProvider = Substitute.For<ReservationProvider>();
        }

        private ReservationStore CreateReservationStore()
        {
            return new ReservationStore(
                this.subReservationProvider);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var reservationStore = this.CreateReservationStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
