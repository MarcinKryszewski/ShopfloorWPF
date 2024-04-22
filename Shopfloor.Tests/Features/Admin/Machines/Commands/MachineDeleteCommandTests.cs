using NSubstitute;
using Shopfloor.Features.Admin.Machines.Commands;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Machines.Commands
{
    public class MachineDeleteCommandTests
    {


        public MachineDeleteCommandTests()
        {

        }

        private MachineDeleteCommand CreateMachineDeleteCommand()
        {
            return new MachineDeleteCommand();
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var machineDeleteCommand = this.CreateMachineDeleteCommand();
            object? parameter = null;

            // Act
            machineDeleteCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
