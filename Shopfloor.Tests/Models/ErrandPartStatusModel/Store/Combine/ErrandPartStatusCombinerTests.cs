using NSubstitute;
using Shopfloor.Models.ErrandPartStatusModel.Store.Combine;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandPartStatusModel.Store.Combine
{
    public class ErrandPartStatusCombinerTests
    {


        public ErrandPartStatusCombinerTests()
        {

        }

        private ErrandPartStatusCombiner CreateErrandPartStatusCombiner()
        {
            return new ErrandPartStatusCombiner();
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandPartStatusCombiner = this.CreateErrandPartStatusCombiner();
            bool shouldForce = false;

            // Act
            await errandPartStatusCombiner.Combine(
                shouldForce);

            // Assert
            Assert.True(false);
        }
    }
}
