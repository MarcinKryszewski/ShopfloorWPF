using NSubstitute;
using Shopfloor.Models.PartModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.PartModel.Store
{
    public class PartStoreTests
    {
        private PartProvider subPartProvider;

        public PartStoreTests()
        {
            this.subPartProvider = Substitute.For<PartProvider>();
        }

        private PartStore CreatePartStore()
        {
            return new PartStore(
                this.subPartProvider);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var partStore = this.CreatePartStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
