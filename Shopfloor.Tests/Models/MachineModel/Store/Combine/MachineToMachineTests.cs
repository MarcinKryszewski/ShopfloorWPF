using NSubstitute;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.MachineModel.Store.Combine;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.MachineModel.Store.Combine
{
    public class MachineToMachineTests
    {
        private MachineStore subMachineStore;

        public MachineToMachineTests()
        {
            this.subMachineStore = Substitute.For<MachineStore>();
        }

        private MachineToMachine CreateMachineToMachine()
        {
            return new MachineToMachine(
                this.subMachineStore);
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var machineToMachine = this.CreateMachineToMachine();

            // Act
            await machineToMachine.Combine();

            // Assert
            Assert.True(false);
        }
    }
}
