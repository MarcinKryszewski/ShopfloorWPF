using NSubstitute;
using Shopfloor.Features.Admin.Machines.Stores;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Machines.Stores
{
    public class SelectedMachineStoreTests
    {


        public SelectedMachineStoreTests()
        {

        }

        private SelectedMachineStore CreateSelectedMachineStore()
        {
            return new SelectedMachineStore();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var selectedMachineStore = this.CreateSelectedMachineStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
