using NSubstitute;
using Shopfloor.Models.ErrandModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandModel
{
    public class ErrandTests
    {


        public ErrandTests()
        {

        }

        private Errand CreateErrand()
        {
            return new Errand();
        }

        [Fact]
        public void AddStatus_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errand = this.CreateErrand();
            ErrandStatus status = null;

            // Act
            errand.AddStatus(
                status);

            // Assert
            Assert.True(false);
        }
    }
}
