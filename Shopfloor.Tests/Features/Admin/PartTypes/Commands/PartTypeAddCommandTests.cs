using NSubstitute;
using Shopfloor.Features.Admin.PartTypes;
using Shopfloor.Features.Admin.PartTypes.Commands;
using Shopfloor.Models.PartTypeModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.PartTypes.Commands
{
    public class PartTypeAddCommandTests
    {
        private PartTypesListViewModel subPartTypesListViewModel;
        private PartTypeProvider subPartTypeProvider;

        public PartTypeAddCommandTests()
        {
            this.subPartTypesListViewModel = Substitute.For<PartTypesListViewModel>();
            this.subPartTypeProvider = Substitute.For<PartTypeProvider>();
        }

        private PartTypeAddCommand CreatePartTypeAddCommand()
        {
            return new PartTypeAddCommand(
                this.subPartTypesListViewModel,
                this.subPartTypeProvider);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var partTypeAddCommand = this.CreatePartTypeAddCommand();
            object? parameter = null;

            // Act
            partTypeAddCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
