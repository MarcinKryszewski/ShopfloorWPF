using NSubstitute;
using Shopfloor.Models.UserModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.UserModel.Store
{
    public class UserStoreTests
    {
        private IUserProvider subUserProvider;

        public UserStoreTests()
        {
            this.subUserProvider = Substitute.For<IUserProvider>();
        }

        private UserStore CreateUserStore()
        {
            return new UserStore(
                this.subUserProvider);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var userStore = this.CreateUserStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
