using NSubstitute;
using Shopfloor.Features.Mechanic.Requests.Stores;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Mechanic.Requests.Stores
{
    public class SelectedRequestStoreTests
    {


        public SelectedRequestStoreTests()
        {

        }

        private SelectedRequestStore CreateSelectedRequestStore()
        {
            return new SelectedRequestStore();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var selectedRequestStore = this.CreateSelectedRequestStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
