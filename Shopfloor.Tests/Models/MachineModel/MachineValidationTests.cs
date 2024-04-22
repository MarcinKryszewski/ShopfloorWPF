using NSubstitute;
using Shopfloor.Interfaces;
using Shopfloor.Models.MachineModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.MachineModel
{
    public class MachineValidationTests
    {
        private IInputForm<Machine> subInputForm;

        public MachineValidationTests()
        {
            this.subInputForm = Substitute.For<IInputForm<Machine>>();
        }

        private MachineValidation CreateMachineValidation()
        {
            return new MachineValidation(
                this.subInputForm);
        }

        [Fact]
        public void ValidateName_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var machineValidation = this.CreateMachineValidation();
            string value = null;
            string propertyName = null;

            // Act
            machineValidation.ValidateName(
                value,
                propertyName);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void ValidateParent_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var machineValidation = this.CreateMachineValidation();
            int? value = null;
            string propertyName = null;
            int? machineId = null;

            // Act
            machineValidation.ValidateParent(
                value,
                propertyName,
                machineId);

            // Assert
            Assert.True(false);
        }
    }
}
