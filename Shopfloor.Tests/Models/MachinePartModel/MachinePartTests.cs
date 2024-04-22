using NSubstitute;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.MachinePartModel;
using Shopfloor.Models.PartModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.MachinePartModel
{
    public class MachinePartTests
    {
        private Part subPart;
        private Machine subMachine;

        public MachinePartTests()
        {
            this.subPart = Substitute.For<Part>();
            this.subMachine = Substitute.For<Machine>();
        }

        private MachinePart CreateMachinePart()
        {
            return new MachinePart(
                this.subPart,
                this.subMachine);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var machinePart = this.CreateMachinePart();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
