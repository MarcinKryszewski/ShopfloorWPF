using NSubstitute;
using Shopfloor.Features.Admin.Suppliers;
using Shopfloor.Features.Admin.Suppliers.Commands;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Suppliers.Commands
{
    public class CleanFormCommandTests
    {
        private SuppliersListViewModel subSuppliersListViewModel;

        public CleanFormCommandTests()
        {
            this.subSuppliersListViewModel = Substitute.For<SuppliersListViewModel>();
        }

        private CleanFormCommand CreateCleanFormCommand()
        {
            return new CleanFormCommand(
                this.subSuppliersListViewModel);
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
