using NSubstitute;
using Shopfloor.Models.MachinePartModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.MachinePartModel
{
    public class MachinePartDTOTests
    {


        public MachinePartDTOTests()
        {

        }

        private MachinePartDTO CreateMachinePartDTO()
        {
            return new MachinePartDTO();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var machinePartDTO = this.CreateMachinePartDTO();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
