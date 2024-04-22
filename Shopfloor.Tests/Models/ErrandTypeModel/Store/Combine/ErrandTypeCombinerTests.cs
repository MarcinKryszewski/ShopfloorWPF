using NSubstitute;
using Shopfloor.Models.ErrandTypeModel.Store.Combine;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandTypeModel.Store.Combine
{
    public class ErrandTypeCombinerTests
    {


        public ErrandTypeCombinerTests()
        {

        }

        private ErrandTypeCombiner CreateErrandTypeCombiner()
        {
            return new ErrandTypeCombiner();
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandTypeCombiner = this.CreateErrandTypeCombiner();
            bool shouldForce = false;

            // Act
            await errandTypeCombiner.Combine(
                shouldForce);

            // Assert
            Assert.True(false);
        }
    }
}
