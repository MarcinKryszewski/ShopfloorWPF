using NSubstitute;
using Shopfloor.Models.MachinePartModel.Store.Combine;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.MachinePartModel.Store.Combine
{
    public class MachinePartCombinerTests
    {


        public MachinePartCombinerTests()
        {

        }

        private MachinePartCombiner CreateMachinePartCombiner()
        {
            return new MachinePartCombiner();
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var machinePartCombiner = this.CreateMachinePartCombiner();
            bool shouldForce = false;

            // Act
            await machinePartCombiner.Combine(
                shouldForce);

            // Assert
            Assert.True(false);
        }
    }
}
