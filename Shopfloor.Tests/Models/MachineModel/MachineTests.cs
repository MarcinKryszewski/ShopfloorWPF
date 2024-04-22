using NSubstitute;
using Shopfloor.Models.MachineModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.MachineModel
{
    public class MachineTests
    {


        public MachineTests()
        {

        }

        private Machine CreateMachine()
        {
            return new Machine();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var machine = this.CreateMachine();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
