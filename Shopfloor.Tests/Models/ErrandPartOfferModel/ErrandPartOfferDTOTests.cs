using NSubstitute;
using Shopfloor.Models.ErrandPartOfferModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandPartOfferModel
{
    public class ErrandPartOfferDTOTests
    {


        public ErrandPartOfferDTOTests()
        {

        }

        private ErrandPartOfferDTO CreateErrandPartOfferDTO()
        {
            return new ErrandPartOfferDTO();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var errandPartOfferDTO = this.CreateErrandPartOfferDTO();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
