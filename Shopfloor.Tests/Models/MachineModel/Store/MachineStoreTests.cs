using NSubstitute;
using Shopfloor.Models.MachineModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.MachineModel.Store
{
    public class MachineStoreTests
    {
        private MachineProvider subMachineProvider;

        public MachineStoreTests()
        {
            this.subMachineProvider = Substitute.For<MachineProvider>();
        }

        private MachineStore CreateMachineStore()
        {
            return new MachineStore(
                this.subMachineProvider);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var machineStore = this.CreateMachineStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
