using NSubstitute;
using Shopfloor.Features.Admin.Parts.Commands;
using Shopfloor.Interfaces;
using Shopfloor.Models.PartModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Parts.Commands
{
    public class PartCleanFormCommandTests
    {
        private IInputForm<Part> subInputForm;

        public PartCleanFormCommandTests()
        {
            this.subInputForm = Substitute.For<IInputForm<Part>>();
        }

        private PartCleanFormCommand CreatePartCleanFormCommand()
        {
            return new PartCleanFormCommand(
                this.subInputForm);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var partCleanFormCommand = this.CreatePartCleanFormCommand();
            object? parameter = null;

            // Act
            partCleanFormCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
