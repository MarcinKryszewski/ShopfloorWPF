using NSubstitute;
using Shopfloor.Features.Admin.Users;
using Shopfloor.Interfaces;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.RoleUserModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Services.NavigationServices;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Users.UsersAdd
{
    public class UsersAddViewModelTests
    {
        private NavigationService subNavigationService;
        private IUserProvider subUserProvider;
        private RoleIUserProvider subRoleIUserProvider;
        private IProvider<Role> subProvider;

        public UsersAddViewModelTests()
        {
            this.subNavigationService = Substitute.For<NavigationService>();
            this.subUserProvider = Substitute.For<IUserProvider>();
            this.subRoleIUserProvider = Substitute.For<RoleIUserProvider>();
            this.subProvider = Substitute.For<IProvider<Role>>();
        }

        private UsersAddViewModel CreateViewModel()
        {
            return new UsersAddViewModel(
                this.subNavigationService,
                this.subUserProvider,
                this.subRoleIUserProvider,
                this.subProvider);
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
        public void UpdateRoles_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var viewModel = this.CreateViewModel();

            // Act
            viewModel.UpdateRoles();

            // Assert
            Assert.True(false);
        }
    }
}
