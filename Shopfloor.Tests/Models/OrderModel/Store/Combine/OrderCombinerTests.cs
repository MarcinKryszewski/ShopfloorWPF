using NSubstitute;
using Shopfloor.Models.OrderModel.Store.Combine;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.OrderModel.Store.Combine
{
    public class OrderCombinerTests
    {


        public OrderCombinerTests()
        {

        }

        private OrderCombiner CreateOrderCombiner()
        {
            return new OrderCombiner();
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var orderCombiner = this.CreateOrderCombiner();
            bool shouldForce = false;

            // Act
            await orderCombiner.Combine(
                shouldForce);

            // Assert
            Assert.True(false);
        }
    }
}
