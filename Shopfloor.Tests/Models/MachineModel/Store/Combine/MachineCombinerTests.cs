using NSubstitute;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.MachineModel.Store;
using Shopfloor.Models.MachineModel.Store.Combine;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.MachineModel.Store.Combine
{
    public class MachineCombinerTests
    {
        private MachineStore subMachineStore;
        private MachineToMachine subMachineToMachine;

        public MachineCombinerTests()
        {
            this.subMachineStore = Substitute.For<MachineStore>();
            this.subMachineToMachine = Substitute.For<MachineToMachine>();
        }

        private MachineCombiner CreateMachineCombiner()
        {
            return new MachineCombiner(
                this.subMachineStore,
                this.subMachineToMachine);
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var machineCombiner = this.CreateMachineCombiner();
            bool shouldForce = false;

            // Act
            await machineCombiner.Combine(
                shouldForce);

            // Assert
            Assert.True(false);
        }
    }
}
