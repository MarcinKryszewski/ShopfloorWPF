using NSubstitute;
using Shopfloor.Features.Mechanic.Errands.Stores;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Mechanic.Errands.Stores
{
    public class SelectedErrandStoreTests
    {


        public SelectedErrandStoreTests()
        {

        }

        private SelectedErrandStore CreateSelectedErrandStore()
        {
            return new SelectedErrandStore();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var selectedErrandStore = this.CreateSelectedErrandStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
