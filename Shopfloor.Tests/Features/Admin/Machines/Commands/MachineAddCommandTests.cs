using NSubstitute;
using Shopfloor.Features.Admin.Machines;
using Shopfloor.Features.Admin.Machines.Commands;
using Shopfloor.Models.MachineModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Machines.Commands
{
    public class MachineAddCommandTests
    {
        private MachinesListViewModel subMachinesListViewModel;
        private MachineProvider subMachineProvider;

        public MachineAddCommandTests()
        {
            this.subMachinesListViewModel = Substitute.For<MachinesListViewModel>();
            this.subMachineProvider = Substitute.For<MachineProvider>();
        }

        private MachineAddCommand CreateMachineAddCommand()
        {
            return new MachineAddCommand(
                this.subMachinesListViewModel,
                this.subMachineProvider);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var machineAddCommand = this.CreateMachineAddCommand();
            object? parameter = null;

            // Act
            machineAddCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
