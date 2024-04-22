using NSubstitute;
using Shopfloor.Models.ErrandModel.Store;
using Shopfloor.Models.ErrandModel.Store.Combine;
using Shopfloor.Models.ErrandStatusModel;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandModel.Store.Combine
{
    public class ErrandToErrandStatusTests
    {
        private ErrandStatusStore subErrandStatusStore;
        private ErrandStore subErrandStore;

        public ErrandToErrandStatusTests()
        {
            this.subErrandStatusStore = Substitute.For<ErrandStatusStore>();
            this.subErrandStore = Substitute.For<ErrandStore>();
        }

        private ErrandToErrandStatus CreateErrandToErrandStatus()
        {
            return new ErrandToErrandStatus(
                this.subErrandStatusStore,
                this.subErrandStore);
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandToErrandStatus = this.CreateErrandToErrandStatus();

            // Act
            await errandToErrandStatus.Combine();

            // Assert
            Assert.True(false);
        }
    }
}
