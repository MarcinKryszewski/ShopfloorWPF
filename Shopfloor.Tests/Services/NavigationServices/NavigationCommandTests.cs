using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;
using System.Windows.Input;

namespace Shopfloor.Tests.Services.NavigationServices
{
    public class NavigationCommandTests
    {
        private readonly INavigationService _navigationService;
        private readonly NavigationCommand<TestViewModel> _navigationCommand;

        public NavigationCommandTests()
        {
            _navigationService = Substitute.For<INavigationService>();
            _navigationCommand = new NavigationCommand<TestViewModel>(_navigationService);
        }

        [Fact]
        public void Navigate_Should_Return_ICommand_Instance()
        {
            // Act
            var command = _navigationCommand.Navigate();
            // Assert
            command.Should().NotBeNull();
            command.Should().BeAssignableTo<ICommand>();
        }

        [Fact]
        public void Navigate_Command_Should_Be_Executable()
        {
            // Arrange
            var command = _navigationCommand.Navigate();
            // Act
            var canExecute = command.CanExecute(null);
            // Assert
            canExecute.Should().BeTrue();
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
    }

    internal class TestViewModel : ViewModelBase
    {
        // This class is used as a test double for T
    }
}