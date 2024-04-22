using NSubstitute;
using Shopfloor.Models.ErrandModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandModel
{
    public class ErrandDisplayTests
    {
        private Errand subErrand;

        public ErrandDisplayTests()
        {
            this.subErrand = Substitute.For<Errand>();
        }

        private ErrandDisplay CreateErrandDisplay()
        {
            return new ErrandDisplay(
                this.subErrand);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var errandDisplay = this.CreateErrandDisplay();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
