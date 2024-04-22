using NSubstitute;
using Shopfloor.Models.ErrandPartStatusModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandPartStatusModel
{
    public class ErrandPartStatusDTOTests
    {


        public ErrandPartStatusDTOTests()
        {

        }

        private ErrandPartStatusDTO CreateErrandPartStatusDTO()
        {
            return new ErrandPartStatusDTO();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var errandPartStatusDTO = this.CreateErrandPartStatusDTO();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
