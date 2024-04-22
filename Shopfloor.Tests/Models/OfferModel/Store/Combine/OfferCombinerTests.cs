using NSubstitute;
using Shopfloor.Models.OfferModel.Store.Combine;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.OfferModel.Store.Combine
{
    public class OfferCombinerTests
    {


        public OfferCombinerTests()
        {

        }

        private OfferCombiner CreateOfferCombiner()
        {
            return new OfferCombiner();
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var offerCombiner = this.CreateOfferCombiner();
            bool shouldForce = false;

            // Act
            await offerCombiner.Combine(
                shouldForce);

            // Assert
            Assert.True(false);
        }
    }
}
