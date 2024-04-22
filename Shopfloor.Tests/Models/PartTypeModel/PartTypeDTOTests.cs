using NSubstitute;
using Shopfloor.Models.PartTypeModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.PartTypeModel
{
    public class PartTypeDTOTests
    {


        public PartTypeDTOTests()
        {

        }

        private PartTypeDTO CreatePartTypeDTO()
        {
            return new PartTypeDTO();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var partTypeDTO = this.CreatePartTypeDTO();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
