using NSubstitute;
using Shopfloor.Models.ErrandModel.Store;
using Shopfloor.Models.ErrandModel.Store.Combine;
using Shopfloor.Models.UserModel;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandModel.Store.Combine
{
    public class ErrandToUserTests
    {
        private UserStore subUserStore;
        private ErrandStore subErrandStore;

        public ErrandToUserTests()
        {
            this.subUserStore = Substitute.For<UserStore>();
            this.subErrandStore = Substitute.For<ErrandStore>();
        }

        private ErrandToUser CreateErrandToUser()
        {
            return new ErrandToUser(
                this.subUserStore,
                this.subErrandStore);
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandToUser = this.CreateErrandToUser();

            // Act
            await errandToUser.Combine();

            // Assert
            Assert.True(false);
        }
    }
}
