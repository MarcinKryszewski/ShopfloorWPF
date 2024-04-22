using NSubstitute;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandModel
{
    public class ErrandValidationTests
    {
        private IInputForm<Errand> subInputForm;

        public ErrandValidationTests()
        {
            this.subInputForm = Substitute.For<IInputForm<Errand>>();
        }

        private ErrandValidation CreateErrandValidation()
        {
            return new ErrandValidation(
                this.subInputForm);
        }

        [Fact]
        public void ValidateType_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandValidation = this.CreateErrandValidation();
            string propertyName = null;
            ErrandType? type = null;

            // Act
            errandValidation.ValidateType(
                propertyName,
                type);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void ValidateMachine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandValidation = this.CreateErrandValidation();
            string propertyName = null;
            Machine? machine = null;

            // Act
            errandValidation.ValidateMachine(
                propertyName,
                machine);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void ValidateDescription_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandValidation = this.CreateErrandValidation();
            string propertyName = null;
            string? value = null;

            // Act
            errandValidation.ValidateDescription(
                propertyName,
                value);

            // Assert
            Assert.True(false);
        }
    }
}
