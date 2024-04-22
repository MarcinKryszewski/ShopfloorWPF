using NSubstitute;
using Shopfloor.Features.Admin.Parts;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Services.NavigationServices;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Parts.PartsAdd
{
    public class PartsAddViewModelTests
    {
        private NavigationService subNavigationService;
        private PartTypeStore subPartTypeStore;
        private SuppliersStore subSuppliersStore;
        private PartStore subPartStore;
        private PartProvider subPartProvider;

        public PartsAddViewModelTests()
        {
            this.subNavigationService = Substitute.For<NavigationService>();
            this.subPartTypeStore = Substitute.For<PartTypeStore>();
            this.subSuppliersStore = Substitute.For<SuppliersStore>();
            this.subPartStore = Substitute.For<PartStore>();
            this.subPartProvider = Substitute.For<PartProvider>();
        }

        private PartsAddViewModel CreateViewModel()
        {
            return new PartsAddViewModel(
                this.subNavigationService,
                this.subPartTypeStore,
                this.subSuppliersStore,
                this.subPartStore,
                this.subPartProvider);
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
        public void ReloadData_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var viewModel = this.CreateViewModel();

            // Act
            viewModel.ReloadData();

            // Assert
            Assert.True(false);
        }
    }
}
