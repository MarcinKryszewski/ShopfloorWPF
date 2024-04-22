using NSubstitute;
using Shopfloor.Models.OfferModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.OfferModel
{
    public class OfferDTOTests
    {


        public OfferDTOTests()
        {

        }

        private OfferDTO CreateOfferDTO()
        {
            return new OfferDTO();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var offerDTO = this.CreateOfferDTO();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
