using NSubstitute;
using Shopfloor.Features.Admin.Parts;
using Shopfloor.Features.Admin.Parts.Stores;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Services.NavigationServices;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Parts.PartsEdit
{
    public class PartsEditViewModelTests
    {
        private NavigationService subNavigationService;
        private SelectedPartStore subSelectedPartStore;
        private PartTypeStore subPartTypeStore;
        private SuppliersStore subSuppliersStore;
        private PartStore subPartStore;
        private PartProvider subPartProvider;

        public PartsEditViewModelTests()
        {
            this.subNavigationService = Substitute.For<NavigationService>();
            this.subSelectedPartStore = Substitute.For<SelectedPartStore>();
            this.subPartTypeStore = Substitute.For<PartTypeStore>();
            this.subSuppliersStore = Substitute.For<SuppliersStore>();
            this.subPartStore = Substitute.For<PartStore>();
            this.subPartProvider = Substitute.For<PartProvider>();
        }

        private PartsEditViewModel CreateViewModel()
        {
            return new PartsEditViewModel(
                this.subNavigationService,
                this.subSelectedPartStore,
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

        [Fact]
        public void SetupForm_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var viewModel = this.CreateViewModel();

            // Act
            viewModel.SetupForm();

            // Assert
            Assert.True(false);
        }
    }
}
