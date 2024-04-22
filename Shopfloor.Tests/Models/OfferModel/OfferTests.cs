using NSubstitute;
using Shopfloor.Models.OfferModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.OfferModel
{
    public class OfferTests
    {


        public OfferTests()
        {

        }

        private Offer CreateOffer()
        {
            return new Offer();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var offer = this.CreateOffer();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
