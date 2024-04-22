using NSubstitute;
using Shopfloor.Models.ErrandStatusModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandStatusModel
{
    public class ErrandStatusTests
    {


        public ErrandStatusTests()
        {

        }

        private ErrandStatus CreateErrandStatus()
        {
            return new ErrandStatus();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var errandStatus = this.CreateErrandStatus();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
