using NSubstitute;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;
using Xunit;

namespace Shopfloor.Tests.Services.NavigationServices
{
    public class NavigationServiceTests
    {
        private INavigationStore subNavigationStore;
        private Func<Type, ViewModelBase> subFunc;

        public NavigationServiceTests()
        {
            this.subNavigationStore = Substitute.For<INavigationStore>();
            this.subFunc = Substitute.For<Func<Type, ViewModelBase>>();
        }

        private NavigationService CreateService()
        {
            return new NavigationService(
                this.subNavigationStore,
                this.subFunc);
        }

        [Fact]
        public void NavigateTo_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            service.NavigateTo();

            // Assert
            Assert.True(false);
        }
    }
}
