using NSubstitute;
using Shopfloor.Models.ErrandPartModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandPartModel
{
    public class ErrandPartDTOTests
    {


        public ErrandPartDTOTests()
        {

        }

        private ErrandPartDTO CreateErrandPartDTO()
        {
            return new ErrandPartDTO();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var errandPartDTO = this.CreateErrandPartDTO();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
