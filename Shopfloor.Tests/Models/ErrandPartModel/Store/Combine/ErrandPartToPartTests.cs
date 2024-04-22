using NSubstitute;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.ErrandPartModel.Store.Combine;
using Shopfloor.Models.PartModel;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandPartModel.Store.Combine
{
    public class ErrandPartToPartTests
    {
        private PartStore subPartStore;
        private ErrandPartStore subErrandPartStore;

        public ErrandPartToPartTests()
        {
            this.subPartStore = Substitute.For<PartStore>();
            this.subErrandPartStore = Substitute.For<ErrandPartStore>();
        }

        private ErrandPartToPart CreateErrandPartToPart()
        {
            return new ErrandPartToPart(
                this.subPartStore,
                this.subErrandPartStore);
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandPartToPart = this.CreateErrandPartToPart();

            // Act
            await errandPartToPart.Combine();

            // Assert
            Assert.True(false);
        }
    }
}
