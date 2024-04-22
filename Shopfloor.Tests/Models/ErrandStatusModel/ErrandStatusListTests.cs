using NSubstitute;
using Shopfloor.Models.ErrandStatusModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandStatusModel
{
    public class ErrandStatusListTests
    {


        public ErrandStatusListTests()
        {

        }

        private ErrandStatusList CreateErrandStatusList()
        {
            return new ErrandStatusList();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var errandStatusList = this.CreateErrandStatusList();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
