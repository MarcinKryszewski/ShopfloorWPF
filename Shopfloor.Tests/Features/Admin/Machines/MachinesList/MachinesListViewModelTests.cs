using NSubstitute;
using Shopfloor.Features.Admin.Machines;
using Shopfloor.Models.MachineModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Machines.MachinesList
{
    public class MachinesListViewModelTests
    {
        private MachineStore subMachineStore;
        private MachineProvider subMachineProvider;

        public MachinesListViewModelTests()
        {
            this.subMachineStore = Substitute.For<MachineStore>();
            this.subMachineProvider = Substitute.For<MachineProvider>();
        }

        private MachinesListViewModel CreateViewModel()
        {
            return new MachinesListViewModel(
                this.subMachineStore,
                this.subMachineProvider);
        }

        [Fact]
        public void AddToList_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var viewModel = this.CreateViewModel();
            Machine machine = null;

            // Act
            viewModel.AddToList(
                machine);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void UpdateList_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var viewModel = this.CreateViewModel();

            // Act
            viewModel.UpdateList();

            // Assert
            Assert.True(false);
        }
    }
}
