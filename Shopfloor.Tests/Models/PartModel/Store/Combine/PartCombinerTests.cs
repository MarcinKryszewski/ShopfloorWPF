using NSubstitute;
using Shopfloor.Models.PartModel.Store.Combine;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.PartModel.Store.Combine
{
    public class PartCombinerTests
    {
        private PartToPartType subPartToPartType;

        public PartCombinerTests()
        {
            this.subPartToPartType = Substitute.For<PartToPartType>();
        }

        private PartCombiner CreatePartCombiner()
        {
            return new PartCombiner(
                this.subPartToPartType);
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var partCombiner = this.CreatePartCombiner();
            bool shouldForce = false;

            // Act
            await partCombiner.Combine(
                shouldForce);

            // Assert
            Assert.True(false);
        }
    }
}
