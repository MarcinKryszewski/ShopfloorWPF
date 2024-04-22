using NSubstitute;
using Shopfloor.Models.ErrandModel.Store;
using Shopfloor.Models.ErrandModel.Store.Combine;
using Shopfloor.Models.ErrandTypeModel;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandModel.Store.Combine
{
    public class ErrandToErrandTypeTests
    {
        private ErrandTypeStore subErrandTypeStore;
        private ErrandStore subErrandStore;

        public ErrandToErrandTypeTests()
        {
            this.subErrandTypeStore = Substitute.For<ErrandTypeStore>();
            this.subErrandStore = Substitute.For<ErrandStore>();
        }

        private ErrandToErrandType CreateErrandToErrandType()
        {
            return new ErrandToErrandType(
                this.subErrandTypeStore,
                this.subErrandStore);
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandToErrandType = this.CreateErrandToErrandType();

            // Act
            await errandToErrandType.Combine();

            // Assert
            Assert.True(false);
        }
    }
}
