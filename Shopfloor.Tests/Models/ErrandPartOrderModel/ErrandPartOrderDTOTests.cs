using NSubstitute;
using Shopfloor.Models.ErrandPartOrderModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandPartOrderModel
{
    public class ErrandPartOrderDTOTests
    {


        public ErrandPartOrderDTOTests()
        {

        }

        private ErrandPartOrderDTO CreateErrandPartOrderDTO()
        {
            return new ErrandPartOrderDTO();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var errandPartOrderDTO = this.CreateErrandPartOrderDTO();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
