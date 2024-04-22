using NSubstitute;
using Shopfloor.Features.Admin.Machines;
using Shopfloor.Features.Admin.Machines.Commands;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Machines.Commands
{
    public class CleanFormCommandTests
    {
        private MachinesListViewModel subMachinesListViewModel;

        public CleanFormCommandTests()
        {
            this.subMachinesListViewModel = Substitute.For<MachinesListViewModel>();
        }

        private CleanFormCommand CreateCleanFormCommand()
        {
            return new CleanFormCommand(
                this.subMachinesListViewModel);
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
