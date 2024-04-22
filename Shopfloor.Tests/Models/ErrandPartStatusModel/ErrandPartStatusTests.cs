using NSubstitute;
using Shopfloor.Models.ErrandPartStatusModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandPartStatusModel
{
    public class ErrandPartStatusTests
    {


        public ErrandPartStatusTests()
        {

        }

        private ErrandPartStatus CreateErrandPartStatus()
        {
            return new ErrandPartStatus();
        }

        [Fact]
        public void SetStatus_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandPartStatus = this.CreateErrandPartStatus();
            int id = 0;

            // Act
            errandPartStatus.SetStatus(
                id);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void Confirm_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandPartStatus = this.CreateErrandPartStatus();

            // Act
            errandPartStatus.Confirm();

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void Abort_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandPartStatus = this.CreateErrandPartStatus();

            // Act
            errandPartStatus.Abort();

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void SetStatus_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var errandPartStatus = this.CreateErrandPartStatus();
            string name = null;

            // Act
            errandPartStatus.SetStatus(
                name);

            // Assert
            Assert.True(false);
        }
    }
}
