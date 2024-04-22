using NSubstitute;
using Shopfloor.Models.ErrandTypeModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandTypeModel
{
    public class ErrandTypeTests
    {


        public ErrandTypeTests()
        {

        }

        private ErrandType CreateErrandType()
        {
            return new ErrandType();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var errandType = this.CreateErrandType();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
