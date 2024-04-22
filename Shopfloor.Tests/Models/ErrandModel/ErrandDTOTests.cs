using NSubstitute;
using Shopfloor.Models.ErrandModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandModel
{
    public class ErrandDTOTests
    {


        public ErrandDTOTests()
        {

        }

        private ErrandDTO CreateErrandDTO()
        {
            return new ErrandDTO();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var errandDTO = this.CreateErrandDTO();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
