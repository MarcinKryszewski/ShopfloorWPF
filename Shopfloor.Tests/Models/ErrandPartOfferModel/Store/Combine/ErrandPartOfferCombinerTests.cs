using NSubstitute;
using Shopfloor.Models.ErrandPartOfferModel.Store;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandPartOfferModel.Store.Combine
{
    public class ErrandPartOfferCombinerTests
    {


        public ErrandPartOfferCombinerTests()
        {

        }

        private ErrandPartOfferCombiner CreateErrandPartOfferCombiner()
        {
            return new ErrandPartOfferCombiner();
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandPartOfferCombiner = this.CreateErrandPartOfferCombiner();
            bool shouldForce = false;

            // Act
            await errandPartOfferCombiner.Combine(
                shouldForce);

            // Assert
            Assert.True(false);
        }
    }
}
