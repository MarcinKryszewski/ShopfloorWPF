using NSubstitute;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.ErrandPartModel.Store.Combine;
using Shopfloor.Models.ErrandPartStatusModel;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandPartModel.Store.Combine
{
    public class ErrandPartToErrandPartStatusTests
    {
        private ErrandPartStatusStore subErrandPartStatusStore;
        private ErrandPartStore subErrandPartStore;

        public ErrandPartToErrandPartStatusTests()
        {
            this.subErrandPartStatusStore = Substitute.For<ErrandPartStatusStore>();
            this.subErrandPartStore = Substitute.For<ErrandPartStore>();
        }

        private ErrandPartToErrandPartStatus CreateErrandPartToErrandPartStatus()
        {
            return new ErrandPartToErrandPartStatus(
                this.subErrandPartStatusStore,
                this.subErrandPartStore);
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandPartToErrandPartStatus = this.CreateErrandPartToErrandPartStatus();

            // Act
            await errandPartToErrandPartStatus.Combine();

            // Assert
            Assert.True(false);
        }
    }
}
