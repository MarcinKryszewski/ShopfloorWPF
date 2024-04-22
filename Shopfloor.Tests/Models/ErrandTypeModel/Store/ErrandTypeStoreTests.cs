using NSubstitute;
using Shopfloor.Models.ErrandTypeModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandTypeModel.Store
{
    public class ErrandTypeStoreTests
    {
        private ErrandTypeProvider subErrandTypeProvider;

        public ErrandTypeStoreTests()
        {
            this.subErrandTypeProvider = Substitute.For<ErrandTypeProvider>();
        }

        private ErrandTypeStore CreateErrandTypeStore()
        {
            return new ErrandTypeStore(
                this.subErrandTypeProvider);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var errandTypeStore = this.CreateErrandTypeStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
