using NSubstitute;
using Shopfloor.Models.ErrandPartOfferModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandPartOfferModel
{
    public class ErrandPartOfferTests
    {


        public ErrandPartOfferTests()
        {

        }

        private ErrandPartOffer CreateErrandPartOffer()
        {
            return new ErrandPartOffer();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var errandPartOffer = this.CreateErrandPartOffer();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
