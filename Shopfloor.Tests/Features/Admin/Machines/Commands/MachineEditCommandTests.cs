using NSubstitute;
using Shopfloor.Features.Admin.Machines;
using Shopfloor.Features.Admin.Machines.Commands;
using Shopfloor.Models.MachineModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Machines.Commands
{
    public class MachineEditCommandTests
    {
        private MachinesListViewModel subMachinesListViewModel;
        private MachineProvider subMachineProvider;

        public MachineEditCommandTests()
        {
            this.subMachinesListViewModel = Substitute.For<MachinesListViewModel>();
            this.subMachineProvider = Substitute.For<MachineProvider>();
        }

        private MachineEditCommand CreateMachineEditCommand()
        {
            return new MachineEditCommand(
                this.subMachinesListViewModel,
                this.subMachineProvider);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var machineEditCommand = this.CreateMachineEditCommand();
            object? parameter = null;

            // Act
            machineEditCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
