using NSubstitute;
using Shopfloor.Models.PartTypeModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.PartTypeModel
{
    public class PartTypeTests
    {


        public PartTypeTests()
        {

        }

        private PartType CreatePartType()
        {
            return new PartType(
                TODO,
                TODO);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var partType = this.CreatePartType();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
