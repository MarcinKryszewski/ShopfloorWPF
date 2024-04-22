using NSubstitute;
using Shopfloor.Models.ErrandPartOrderModel.Store.Combine;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandPartOrderModel.Store.Combine
{
    public class ErrandPartOrderCombinerTests
    {


        public ErrandPartOrderCombinerTests()
        {

        }

        private ErrandPartOrderCombiner CreateErrandPartOrderCombiner()
        {
            return new ErrandPartOrderCombiner();
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandPartOrderCombiner = this.CreateErrandPartOrderCombiner();
            bool shouldForce = false;

            // Act
            await errandPartOrderCombiner.Combine(
                shouldForce);

            // Assert
            Assert.True(false);
        }
    }
}
