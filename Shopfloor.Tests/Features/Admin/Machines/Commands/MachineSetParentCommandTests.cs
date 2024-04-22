using NSubstitute;
using Shopfloor.Features.Admin.Machines;
using Shopfloor.Features.Admin.Machines.Commands;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Machines.Commands
{
    public class MachineSetParentCommandTests
    {
        private MachinesListViewModel subMachinesListViewModel;

        public MachineSetParentCommandTests()
        {
            this.subMachinesListViewModel = Substitute.For<MachinesListViewModel>();
        }

        private MachineSetParentCommand CreateMachineSetParentCommand()
        {
            return new MachineSetParentCommand(
                this.subMachinesListViewModel);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var machineSetParentCommand = this.CreateMachineSetParentCommand();
            object? parameter = null;

            // Act
            machineSetParentCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
