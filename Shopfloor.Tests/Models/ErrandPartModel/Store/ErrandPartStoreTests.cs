using NSubstitute;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Store;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandPartModel.Store
{
    public class ErrandPartStoreTests
    {
        private ErrandPartProvider subErrandPartProvider;

        public ErrandPartStoreTests()
        {
            this.subErrandPartProvider = Substitute.For<ErrandPartProvider>();
        }

        private ErrandPartStore CreateErrandPartStore()
        {
            return new ErrandPartStore(
                this.subErrandPartProvider);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var errandPartStore = this.CreateErrandPartStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
