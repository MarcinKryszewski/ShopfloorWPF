using NSubstitute;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.PartModel.Store.Combine;
using Shopfloor.Models.PartTypeModel;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.PartModel.Store.Combine
{
    public class PartToPartTypeTests
    {
        private PartTypeStore subPartTypeStore;
        private PartStore subPartStore;

        public PartToPartTypeTests()
        {
            this.subPartTypeStore = Substitute.For<PartTypeStore>();
            this.subPartStore = Substitute.For<PartStore>();
        }

        private PartToPartType CreatePartToPartType()
        {
            return new PartToPartType(
                this.subPartTypeStore,
                this.subPartStore);
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var partToPartType = this.CreatePartToPartType();

            // Act
            await partToPartType.Combine();

            // Assert
            Assert.True(false);
        }
    }
}
