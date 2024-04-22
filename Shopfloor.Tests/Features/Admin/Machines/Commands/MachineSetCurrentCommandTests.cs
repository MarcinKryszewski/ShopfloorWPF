using NSubstitute;
using Shopfloor.Features.Admin.Machines;
using Shopfloor.Features.Admin.Machines.Commands;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Machines.Commands
{
    public class MachineSetCurrentCommandTests
    {
        private MachinesListViewModel subMachinesListViewModel;

        public MachineSetCurrentCommandTests()
        {
            this.subMachinesListViewModel = Substitute.For<MachinesListViewModel>();
        }

        private MachineSetCurrentCommand CreateMachineSetCurrentCommand()
        {
            return new MachineSetCurrentCommand(
                this.subMachinesListViewModel);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var machineSetCurrentCommand = this.CreateMachineSetCurrentCommand();
            object? parameter = null;

            // Act
            machineSetCurrentCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
