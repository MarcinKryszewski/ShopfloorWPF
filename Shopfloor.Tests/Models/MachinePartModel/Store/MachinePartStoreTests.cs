using NSubstitute;
using Shopfloor.Models.MachinePartModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.MachinePartModel.Store
{
    public class MachinePartStoreTests
    {
        private MachinePartProvider subMachinePartProvider;

        public MachinePartStoreTests()
        {
            this.subMachinePartProvider = Substitute.For<MachinePartProvider>();
        }

        private MachinePartStore CreateMachinePartStore()
        {
            return new MachinePartStore(
                this.subMachinePartProvider);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var machinePartStore = this.CreateMachinePartStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
