using NSubstitute;
using Shopfloor.Models.ErrandStatusModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandStatusModel
{
    public class ErrandStatusDTOTests
    {


        public ErrandStatusDTOTests()
        {

        }

        private ErrandStatusDTO CreateErrandStatusDTO()
        {
            return new ErrandStatusDTO();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var errandStatusDTO = this.CreateErrandStatusDTO();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
