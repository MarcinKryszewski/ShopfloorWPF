using Shopfloor.Features.WorkInProgressFeature;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Tests.Services.NavigationServices
{
    public class NavigationServiceTests
    {
        [Fact]
        public void NavigateTo_SetsCurrentViewModelInNavigationStore()
        {
            // Arrange
            Func<Type, ViewModelBase> viewModelFactory = Substitute.For<Func<Type, ViewModelBase>>();
            INavigationStore navigationStore = Substitute.For<INavigationStore>();
            NavigationService navigationService = new(navigationStore, viewModelFactory);
            WorkInProgressViewModel expectedViewModel = new();
            viewModelFactory.Invoke(typeof(WorkInProgressViewModel)).Returns(expectedViewModel);
            // Act
            navigationService.NavigateTo<WorkInProgressViewModel>();
            // Assert
            navigationStore.CurrentViewModel.Should().Be(expectedViewModel);
        }
    }
}
