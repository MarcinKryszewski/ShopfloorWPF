using NSubstitute;
using Shopfloor.Features.Admin.Users;
using Shopfloor.Features.Admin.Users.Stores;
using Shopfloor.Models.UserModel;
using Shopfloor.Services.NavigationServices;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Users.UsersList
{
    public class UsersListViewModelTests
    {
        private NavigationService subNavigationService;
        private IUserProvider subUserProvider;
        private SelectedUserStore subSelectedUserStore;

        public UsersListViewModelTests()
        {
            this.subNavigationService = Substitute.For<NavigationService>();
            this.subUserProvider = Substitute.For<IUserProvider>();
            this.subSelectedUserStore = Substitute.For<SelectedUserStore>();
        }

        private UsersListViewModel CreateViewModel()
        {
            return new UsersListViewModel(
                this.subNavigationService,
                this.subUserProvider,
                this.subSelectedUserStore);
        }

        [Fact]
        public async Task LoadData_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var viewModel = this.CreateViewModel();
            IUserProvider provider = null;

            // Act
            await viewModel.LoadData(
                provider);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void UpdateUsers_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var viewModel = this.CreateViewModel();

            // Act
            var result = viewModel.UpdateUsers();

            // Assert
            Assert.True(false);
        }
    }
}
