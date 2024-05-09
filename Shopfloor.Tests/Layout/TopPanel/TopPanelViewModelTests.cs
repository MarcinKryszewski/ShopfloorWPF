using Shopfloor.Layout.TopPanel;
using Shopfloor.Models.UserModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Stores;

namespace Shopfloor.Tests.Layout.TopPanel
{
    public class TopPanelViewModelTests
    {
        private readonly INavigationService _navigationService;
        private readonly ICurrentUserStore _userStore;
        private const string defaultUserImagePath = "pack://application:,,,/Shopfloor;component/Resources/userDefault.png";
        public TopPanelViewModelTests()
        {
            _userStore = Substitute.For<ICurrentUserStore>();
            _navigationService = Substitute.For<INavigationService>();
        }
        [Fact]
        public void UserImagePath_WhenUserIsNull_ReturnsDefaultImagePath()
        {
            // Arrange
            TopPanelViewModel sut = new(_navigationService, _userStore);
            // Act
            _userStore.User.ReturnsNull();
            string result = sut.UserImagePath;
            // Assert
            result.Should().Be(defaultUserImagePath);
        }
        [Fact]
        public void UserImagePath_WhenUserIsNotNull_ReturnsUserImagePath()
        {
            // Arrange
            string testImagePath = "test";
            User user = Substitute.For<User>();
            TopPanelViewModel sut = new(_navigationService, _userStore);
            // Act
            user.Image.Returns(testImagePath);
            _userStore.User.Returns(user);
            string result = sut.UserImagePath;
            // Assert
            result.Should().Be(testImagePath);
        }
        [Fact]
        public void Username_WhenUserIsLoggedIn_ReturnsWelcomeMessageWithUserName()
        {

        }
        [Fact]
        public void Username_WhenUserIsNotLoggedIn_ReturnsLoginMessage()
        {

        }
        [Fact]
        public void IsLoggedIn_WhenDefault_ReturnsFalse()
        {

        }
        [Fact]
        public void IsLoggedIn_AfterSuccessfullLogin_ReturnsTrue()
        {

        }
    }
}
