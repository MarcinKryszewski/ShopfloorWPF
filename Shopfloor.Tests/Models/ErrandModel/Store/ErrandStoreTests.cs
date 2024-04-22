using NSubstitute;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandModel.Store;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandModel.Store
{
    public class ErrandStoreTests
    {
        private ErrandProvider subErrandProvider;

        public ErrandStoreTests()
        {
            this.subErrandProvider = Substitute.For<ErrandProvider>();
        }

        private ErrandStore CreateErrandStore()
        {
            return new ErrandStore(
                this.subErrandProvider);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var errandStore = this.CreateErrandStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
