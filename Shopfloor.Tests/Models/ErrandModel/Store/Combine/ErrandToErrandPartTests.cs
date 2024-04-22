using NSubstitute;
using Shopfloor.Models.ErrandModel.Store;
using Shopfloor.Models.ErrandModel.Store.Combine;
using Shopfloor.Models.ErrandPartModel.Store;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandModel.Store.Combine
{
    public class ErrandToErrandPartTests
    {
        private ErrandPartStore subErrandPartStore;
        private ErrandStore subErrandStore;

        public ErrandToErrandPartTests()
        {
            this.subErrandPartStore = Substitute.For<ErrandPartStore>();
            this.subErrandStore = Substitute.For<ErrandStore>();
        }

        private ErrandToErrandPart CreateErrandToErrandPart()
        {
            return new ErrandToErrandPart(
                this.subErrandPartStore,
                this.subErrandStore);
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandToErrandPart = this.CreateErrandToErrandPart();

            // Act
            await errandToErrandPart.Combine();

            // Assert
            Assert.True(false);
        }
    }
}
