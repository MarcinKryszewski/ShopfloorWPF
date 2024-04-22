using NSubstitute;
using Shopfloor.Models.OrderModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.OrderModel
{
    public class OrderDTOTests
    {


        public OrderDTOTests()
        {

        }

        private OrderDTO CreateOrderDTO()
        {
            return new OrderDTO();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var orderDTO = this.CreateOrderDTO();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
