using NSubstitute;
using Shopfloor.Models.ErrandPartStatusModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandPartStatusModel.Store
{
    public class ErrandPartStatusStoreTests
    {
        private ErrandPartStatusProvider subErrandPartStatusProvider;

        public ErrandPartStatusStoreTests()
        {
            this.subErrandPartStatusProvider = Substitute.For<ErrandPartStatusProvider>();
        }

        private ErrandPartStatusStore CreateErrandPartStatusStore()
        {
            return new ErrandPartStatusStore(
                this.subErrandPartStatusProvider);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var errandPartStatusStore = this.CreateErrandPartStatusStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
