using NSubstitute;
using Shopfloor.Models.MachineModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.MachineModel
{
    public class MachineDTOTests
    {


        public MachineDTOTests()
        {

        }

        private MachineDTO CreateMachineDTO()
        {
            return new MachineDTO();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var machineDTO = this.CreateMachineDTO();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
