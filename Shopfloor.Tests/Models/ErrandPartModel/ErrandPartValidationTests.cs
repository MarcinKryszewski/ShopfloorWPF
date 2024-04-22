using NSubstitute;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandPartModel
{
    public class ErrandPartValidationTests
    {
        private IInputForm<ErrandPart> subInputForm;

        public ErrandPartValidationTests()
        {
            this.subInputForm = Substitute.For<IInputForm<ErrandPart>>();
        }

        private ErrandPartValidation CreateErrandPartValidation()
        {
            return new ErrandPartValidation(
                this.subInputForm);
        }

        [Fact]
        public void ValidateAmount_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandPartValidation = this.CreateErrandPartValidation();
            string propertyName = null;
            double? value = null;

            // Act
            errandPartValidation.ValidateAmount(
                propertyName,
                value);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void ValidateObjectAmount_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandPartValidation = this.CreateErrandPartValidation();
            ErrandPart errandPart = null;
            double? value = null;

            // Act
            errandPartValidation.ValidateObjectAmount(
                errandPart,
                value);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void ValidatePrice_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandPartValidation = this.CreateErrandPartValidation();
            string propertyName = null;
            double? value = null;

            // Act
            errandPartValidation.ValidatePrice(
                propertyName,
                value);

            // Assert
            Assert.True(false);
        }
    }
}
