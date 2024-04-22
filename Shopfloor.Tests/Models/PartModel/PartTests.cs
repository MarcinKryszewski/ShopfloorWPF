using NSubstitute;
using Shopfloor.Models.PartModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.PartModel
{
    public class PartTests
    {


        public PartTests()
        {

        }

        private Part CreatePart()
        {
            return new Part(
                TODO,
                TODO,
                TODO,
                TODO,
                TODO,
                TODO,
                TODO,
                TODO,
                TODO);
        }

        [Fact]
        public void SetType_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var part = this.CreatePart();
            PartType? type = null;

            // Act
            part.SetType(
                type);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void SetProducer_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var part = this.CreatePart();
            Supplier? producer = null;

            // Act
            part.SetProducer(
                producer);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void SetSupplier_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var part = this.CreatePart();
            Supplier? supplier = null;

            // Act
            part.SetSupplier(
                supplier);

            // Assert
            Assert.True(false);
        }
    }
}
