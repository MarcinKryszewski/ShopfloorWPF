using NSubstitute;
using Shopfloor.Models.UserModel.Store.Combine;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.UserModel.Store.Combine
{
    public class UserCombinerTests
    {
        private UserToRole subUserToRole;

        public UserCombinerTests()
        {
            this.subUserToRole = Substitute.For<UserToRole>();
        }

        private UserCombiner CreateUserCombiner()
        {
            return new UserCombiner(
                this.subUserToRole);
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var userCombiner = this.CreateUserCombiner();
            bool shouldForce = false;

            // Act
            await userCombiner.Combine(
                shouldForce);

            // Assert
            Assert.True(false);
        }
    }
}
