using NSubstitute;
using Shopfloor.Features.Admin.Parts;
using Shopfloor.Features.Admin.Parts.Commands;
using Shopfloor.Models.PartModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Parts.Commands
{
    public class PartAddCommandTests
    {
        private PartsAddViewModel subPartsAddViewModel;
        private PartProvider subPartProvider;

        public PartAddCommandTests()
        {
            this.subPartsAddViewModel = Substitute.For<PartsAddViewModel>();
            this.subPartProvider = Substitute.For<PartProvider>();
        }

        private PartAddCommand CreatePartAddCommand()
        {
            return new PartAddCommand(
                this.subPartsAddViewModel,
                this.subPartProvider);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var partAddCommand = this.CreatePartAddCommand();
            object? parameter = null;

            // Act
            partAddCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
