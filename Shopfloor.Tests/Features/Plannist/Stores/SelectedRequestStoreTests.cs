using NSubstitute;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Plannist.Stores
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
