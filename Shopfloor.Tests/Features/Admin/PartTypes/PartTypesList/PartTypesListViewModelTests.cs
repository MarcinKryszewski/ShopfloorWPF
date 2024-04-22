using NSubstitute;
using Shopfloor.Features.Admin.PartTypes;
using Shopfloor.Models.PartTypeModel;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.PartTypes.PartTypesList
{
    public class PartTypesListViewModelTests
    {
        private PartTypeProvider subPartTypeProvider;
        private PartTypeStore subPartTypeStore;

        public PartTypesListViewModelTests()
        {
            this.subPartTypeProvider = Substitute.For<PartTypeProvider>();
            this.subPartTypeStore = Substitute.For<PartTypeStore>();
        }

        private PartTypesListViewModel CreateViewModel()
        {
            return new PartTypesListViewModel(
                this.subPartTypeProvider,
                this.subPartTypeStore);
        }

        [Fact]
        public void AddError_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var viewModel = this.CreateViewModel();
            string propertyName = null;
            string errorMassage = null;

            // Act
            viewModel.AddError(
                propertyName,
                errorMassage);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void CleanForm_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var viewModel = this.CreateViewModel();

            // Act
            viewModel.CleanForm();

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void ClearErrors_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var viewModel = this.CreateViewModel();
            string? propertyName = null;

            // Act
            viewModel.ClearErrors(
                propertyName);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void GetErrors_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var viewModel = this.CreateViewModel();
            string? propertyName = null;

            // Act
            var result = viewModel.GetErrors(
                propertyName);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task LoadData_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var viewModel = this.CreateViewModel();

            // Act
            await viewModel.LoadData();

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void ReloadData_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var viewModel = this.CreateViewModel();

            // Act
            viewModel.ReloadData();

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task UpdateData_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var viewModel = this.CreateViewModel();

            // Act
            await viewModel.UpdateData();

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task UpdateData_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var viewModel = this.CreateViewModel();
            PartType partTypeToRemove = null;

            // Act
            await viewModel.UpdateData(
                partTypeToRemove);

            // Assert
            Assert.True(false);
        }
    }
}
