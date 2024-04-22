using NSubstitute;
using Shopfloor.Models.OrderModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.OrderModel.Store
{
    public class OrderStoreTests
    {
        private OrderProvider subOrderProvider;

        public OrderStoreTests()
        {
            this.subOrderProvider = Substitute.For<OrderProvider>();
        }

        private OrderStore CreateOrderStore()
        {
            return new OrderStore(
                this.subOrderProvider);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var orderStore = this.CreateOrderStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
