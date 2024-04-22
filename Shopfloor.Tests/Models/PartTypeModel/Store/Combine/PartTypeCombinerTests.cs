using NSubstitute;
using Shopfloor.Models.PartTypeModel.Store.Combine;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.PartTypeModel.Store.Combine
{
    public class PartTypeCombinerTests
    {


        public PartTypeCombinerTests()
        {

        }

        private PartTypeCombiner CreatePartTypeCombiner()
        {
            return new PartTypeCombiner();
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var partTypeCombiner = this.CreatePartTypeCombiner();
            bool shouldForce = false;

            // Act
            await partTypeCombiner.Combine(
                shouldForce);

            // Assert
            Assert.True(false);
        }
    }
}
