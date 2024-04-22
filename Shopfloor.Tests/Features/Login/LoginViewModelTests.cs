using NSubstitute;
using Shopfloor.Features.Login;
using Shopfloor.Models.UserModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Stores;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Login
{
    public class LoginViewModelTests
    {
        private NavigationService subNavigationService;
        private CurrentUserStore subCurrentUserStore;
        private IUserProvider subUserProvider;

        public LoginViewModelTests()
        {
            this.subNavigationService = Substitute.For<NavigationService>();
            this.subCurrentUserStore = Substitute.For<CurrentUserStore>();
            this.subUserProvider = Substitute.For<IUserProvider>();
        }

        private LoginViewModel CreateViewModel()
        {
            return new LoginViewModel(
                this.subNavigationService,
                this.subCurrentUserStore,
                this.subUserProvider);
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
    }
}
