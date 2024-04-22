using NSubstitute;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.ErrandPartModel.Store.Combine;
using Shopfloor.Models.UserModel;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandPartModel.Store.Combine
{
    public class ErrandPartToUserTests
    {
        private UserStore subUserStore;
        private ErrandPartStore subErrandPartStore;

        public ErrandPartToUserTests()
        {
            this.subUserStore = Substitute.For<UserStore>();
            this.subErrandPartStore = Substitute.For<ErrandPartStore>();
        }

        private ErrandPartToUser CreateErrandPartToUser()
        {
            return new ErrandPartToUser(
                this.subUserStore,
                this.subErrandPartStore);
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandPartToUser = this.CreateErrandPartToUser();

            // Act
            await errandPartToUser.Combine();

            // Assert
            Assert.True(false);
        }
    }
}
