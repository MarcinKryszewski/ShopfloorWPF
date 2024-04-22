using NSubstitute;
using Shopfloor.Features.Admin.PartTypes;
using Shopfloor.Features.Admin.PartTypes.Commands;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.PartTypes.Commands
{
    public class CleanFormCommandTests
    {
        private PartTypesListViewModel subPartTypesListViewModel;

        public CleanFormCommandTests()
        {
            this.subPartTypesListViewModel = Substitute.For<PartTypesListViewModel>();
        }

        private CleanFormCommand CreateCleanFormCommand()
        {
            return new CleanFormCommand(
                this.subPartTypesListViewModel);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var cleanFormCommand = this.CreateCleanFormCommand();
            object? parameter = null;

            // Act
            cleanFormCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
