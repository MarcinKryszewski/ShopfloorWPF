using NSubstitute;
using Shopfloor.Models.ErrandModel.Store;
using Shopfloor.Models.ErrandModel.Store.Combine;
using Shopfloor.Models.MachineModel;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandModel.Store.Combine
{
    public class ErrandToMachineTests
    {
        private MachineStore subMachineStore;
        private ErrandStore subErrandStore;

        public ErrandToMachineTests()
        {
            this.subMachineStore = Substitute.For<MachineStore>();
            this.subErrandStore = Substitute.For<ErrandStore>();
        }

        private ErrandToMachine CreateErrandToMachine()
        {
            return new ErrandToMachine(
                this.subMachineStore,
                this.subErrandStore);
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandToMachine = this.CreateErrandToMachine();

            // Act
            await errandToMachine.Combine();

            // Assert
            Assert.True(false);
        }
    }
}
