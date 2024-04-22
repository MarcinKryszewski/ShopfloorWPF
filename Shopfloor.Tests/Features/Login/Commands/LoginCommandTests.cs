using NSubstitute;
using Shopfloor.Features.Login;
using Shopfloor.Features.Login.Commands;
using Shopfloor.Models.UserModel;
using Shopfloor.Stores;
using System;
using System.Windows.Input;
using Xunit;

namespace Shopfloor.Tests.Features.Login.Commands
{
    public class LoginCommandTests
    {
        private IUserProvider subUserProvider;
        private CurrentUserStore subCurrentUserStore;
        private LoginViewModel subLoginViewModel;
        private ICommand subCommand;

        public LoginCommandTests()
        {
            this.subUserProvider = Substitute.For<IUserProvider>();
            this.subCurrentUserStore = Substitute.For<CurrentUserStore>();
            this.subLoginViewModel = Substitute.For<LoginViewModel>();
            this.subCommand = Substitute.For<ICommand>();
        }

        private LoginCommand CreateLoginCommand()
        {
            return new LoginCommand(
                this.subUserProvider,
                this.subCurrentUserStore,
                this.subLoginViewModel,
                this.subCommand);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var loginCommand = this.CreateLoginCommand();
            object? parameter = null;

            // Act
            loginCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
