using NSubstitute;
using Shopfloor.Models.ErrandStatusModel.Store.Combine;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandStatusModel.Store.Combine
{
    public class ErrandStatusCombinerTests
    {


        public ErrandStatusCombinerTests()
        {

        }

        private ErrandStatusCombiner CreateErrandStatusCombiner()
        {
            return new ErrandStatusCombiner();
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandStatusCombiner = this.CreateErrandStatusCombiner();
            bool shouldForce = false;

            // Act
            await errandStatusCombiner.Combine(
                shouldForce);

            // Assert
            Assert.True(false);
        }
    }
}
