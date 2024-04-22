using NSubstitute;
using Shopfloor.Features.Admin.Parts;
using Shopfloor.Features.Admin.Parts.Commands;
using Shopfloor.Models.PartModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Parts.Commands
{
    public class PartEditCommandTests
    {
        private PartsEditViewModel subPartsEditViewModel;
        private PartProvider subPartProvider;

        public PartEditCommandTests()
        {
            this.subPartsEditViewModel = Substitute.For<PartsEditViewModel>();
            this.subPartProvider = Substitute.For<PartProvider>();
        }

        private PartEditCommand CreatePartEditCommand()
        {
            return new PartEditCommand(
                this.subPartsEditViewModel,
                this.subPartProvider);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var partEditCommand = this.CreatePartEditCommand();
            object? parameter = null;

            // Act
            partEditCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
