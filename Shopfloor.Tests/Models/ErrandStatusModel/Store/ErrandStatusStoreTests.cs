using NSubstitute;
using Shopfloor.Models.ErrandStatusModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandStatusModel.Store
{
    public class ErrandStatusStoreTests
    {
        private ErrandStatusProvider subErrandStatusProvider;

        public ErrandStatusStoreTests()
        {
            this.subErrandStatusProvider = Substitute.For<ErrandStatusProvider>();
        }

        private ErrandStatusStore CreateErrandStatusStore()
        {
            return new ErrandStatusStore(
                this.subErrandStatusProvider);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var errandStatusStore = this.CreateErrandStatusStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
