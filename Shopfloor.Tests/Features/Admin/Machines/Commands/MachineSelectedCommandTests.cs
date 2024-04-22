using NSubstitute;
using Shopfloor.Features.Admin.Machines.Commands;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Machines.Commands
{
    public class MachineSelectedCommandTests
    {


        public MachineSelectedCommandTests()
        {

        }

        private MachineSelectedCommand CreateMachineSelectedCommand()
        {
            return new MachineSelectedCommand();
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var machineSelectedCommand = this.CreateMachineSelectedCommand();
            object? parameter = null;

            // Act
            machineSelectedCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
