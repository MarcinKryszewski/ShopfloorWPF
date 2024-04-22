using NSubstitute;
using Shopfloor.Models.OrderModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.OrderModel
{
    public class OrderTests
    {


        public OrderTests()
        {

        }

        private Order CreateOrder()
        {
            return new Order();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var order = this.CreateOrder();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
