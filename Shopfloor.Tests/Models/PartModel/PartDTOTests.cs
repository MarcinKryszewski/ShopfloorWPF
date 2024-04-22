using NSubstitute;
using Shopfloor.Models.PartModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.PartModel
{
    public class PartDTOTests
    {


        public PartDTOTests()
        {

        }

        private PartDTO CreatePartDTO()
        {
            return new PartDTO();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var partDTO = this.CreatePartDTO();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
