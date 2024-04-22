using NSubstitute;
using Shopfloor.Features.Admin.PartTypes;
using Shopfloor.Features.Admin.PartTypes.Commands;
using Shopfloor.Models.PartTypeModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.PartTypes.Commands
{
    public class PartTypeEditCommandTests
    {
        private PartTypesListViewModel subPartTypesListViewModel;
        private PartTypeProvider subPartTypeProvider;

        public PartTypeEditCommandTests()
        {
            this.subPartTypesListViewModel = Substitute.For<PartTypesListViewModel>();
            this.subPartTypeProvider = Substitute.For<PartTypeProvider>();
        }

        private PartTypeEditCommand CreatePartTypeEditCommand()
        {
            return new PartTypeEditCommand(
                this.subPartTypesListViewModel,
                this.subPartTypeProvider);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var partTypeEditCommand = this.CreatePartTypeEditCommand();
            object? parameter = null;

            // Act
            partTypeEditCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
