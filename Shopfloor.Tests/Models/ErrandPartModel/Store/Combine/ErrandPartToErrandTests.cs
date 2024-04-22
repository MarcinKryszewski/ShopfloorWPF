using NSubstitute;
using Shopfloor.Models.ErrandModel.Store;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.ErrandPartModel.Store.Combine;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandPartModel.Store.Combine
{
    public class ErrandPartToErrandTests
    {
        private ErrandStore subErrandStore;
        private ErrandPartStore subErrandPartStore;

        public ErrandPartToErrandTests()
        {
            this.subErrandStore = Substitute.For<ErrandStore>();
            this.subErrandPartStore = Substitute.For<ErrandPartStore>();
        }

        private ErrandPartToErrand CreateErrandPartToErrand()
        {
            return new ErrandPartToErrand(
                this.subErrandStore,
                this.subErrandPartStore);
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandPartToErrand = this.CreateErrandPartToErrand();

            // Act
            await errandPartToErrand.Combine();

            // Assert
            Assert.True(false);
        }
    }
}
