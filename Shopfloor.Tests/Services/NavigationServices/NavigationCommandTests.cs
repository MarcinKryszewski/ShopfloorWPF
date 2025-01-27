using System.Windows.Input;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Tests.Services.NavigationServices
{
    public class NavigationCommandTests
    {
        private readonly NavigationCommand<TestViewModel> _navigationCommand;
        private readonly INavigationService _navigationService;
        public NavigationCommandTests()
        {
            _navigationService = Substitute.For<INavigationService>();
            _navigationCommand = new NavigationCommand<TestViewModel>(_navigationService);
        }

        [Fact]
        public void Navigate_Command_Should_Be_Executable()
        {
            // Arrange
            var command = _navigationCommand.Navigate();
            // Act
            var canExecute = command.CanExecute(null);
            // Assert
            canExecute.ShouldBeTrue();
        }
        [Fact]
        public void Navigate_Command_Should_Invoke_NavigationService()
        {
            // Arrange
            var command = _navigationCommand.Navigate();
            // Act
            command.Execute(null);
            // Assert
            _navigationService.Received(1).NavigateTo<TestViewModel>();
        }
        [Fact]
        public void Navigate_Should_Return_ICommand_Instance()
        {
            // Act
            var command = _navigationCommand.Navigate();
            // Assert
            command.ShouldNotBeNull();
            command.ShouldBeAssignableTo<ICommand>();
        }
    }

    internal class TestViewModel : ViewModelBase
    {
        // This class is used as a test double for T
    }
}