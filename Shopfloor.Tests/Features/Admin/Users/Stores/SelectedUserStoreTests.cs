using NSubstitute;
using Shopfloor.Features.Admin.Users.Stores;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Users.Stores
{
    public class SelectedUserStoreTests
    {


        public SelectedUserStoreTests()
        {

        }

        private SelectedUserStore CreateSelectedUserStore()
        {
            return new SelectedUserStore();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var selectedUserStore = this.CreateSelectedUserStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
