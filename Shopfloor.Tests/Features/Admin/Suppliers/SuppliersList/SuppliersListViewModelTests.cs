using NSubstitute;
using Shopfloor.Features.Admin.Suppliers;
using Shopfloor.Models.SupplierModel;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Suppliers.SuppliersList
{
    public class SuppliersListViewModelTests
    {
        private SupplierProvider subSupplierProvider;
        private SuppliersStore subSuppliersStore;

        public SuppliersListViewModelTests()
        {
            this.subSupplierProvider = Substitute.For<SupplierProvider>();
            this.subSuppliersStore = Substitute.For<SuppliersStore>();
        }

        private SuppliersListViewModel CreateViewModel()
        {
            return new SuppliersListViewModel(
                this.subSupplierProvider,
                this.subSuppliersStore);
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
            Supplier supplierToRemove = null;

            // Act
            await viewModel.UpdateData(
                supplierToRemove);

            // Assert
            Assert.True(false);
        }
    }
}
