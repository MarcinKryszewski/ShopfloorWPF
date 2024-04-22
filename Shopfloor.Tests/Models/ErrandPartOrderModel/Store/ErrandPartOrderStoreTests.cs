using NSubstitute;
using Shopfloor.Models.ErrandPartOrderModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandPartOrderModel.Store
{
    public class ErrandPartOrderStoreTests
    {
        private ErrandPartOrderProvider subErrandPartOrderProvider;

        public ErrandPartOrderStoreTests()
        {
            this.subErrandPartOrderProvider = Substitute.For<ErrandPartOrderProvider>();
        }

        private ErrandPartOrderStore CreateErrandPartOrderStore()
        {
            return new ErrandPartOrderStore(
                this.subErrandPartOrderProvider);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var errandPartOrderStore = this.CreateErrandPartOrderStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
