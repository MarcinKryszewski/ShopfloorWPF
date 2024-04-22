using NSubstitute;
using Shopfloor.Models.ErrandPartOrderModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandPartOrderModel
{
    public class ErrandPartOrderTests
    {


        public ErrandPartOrderTests()
        {

        }

        private ErrandPartOrder CreateErrandPartOrder()
        {
            return new ErrandPartOrder();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var errandPartOrder = this.CreateErrandPartOrder();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
