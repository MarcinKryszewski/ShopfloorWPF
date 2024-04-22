using NSubstitute;
using Shopfloor.Models.ErrandTypeModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandTypeModel
{
    public class ErrandTypeDTOTests
    {


        public ErrandTypeDTOTests()
        {

        }

        private ErrandTypeDTO CreateErrandTypeDTO()
        {
            return new ErrandTypeDTO();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var errandTypeDTO = this.CreateErrandTypeDTO();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
