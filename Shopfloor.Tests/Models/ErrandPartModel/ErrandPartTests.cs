using NSubstitute;
using Shopfloor.Models.ErrandPartModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandPartModel
{
    public class ErrandPartTests
    {


        public ErrandPartTests()
        {

        }

        private ErrandPart CreateErrandPart()
        {
            return new ErrandPart();
        }

        [Fact]
        public void SetPrice_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandPart = this.CreateErrandPart();
            double price = 0;
            double? amount = null;

            // Act
            errandPart.SetPrice(
                price,
                amount);

            // Assert
            Assert.True(false);
        }
    }
}
