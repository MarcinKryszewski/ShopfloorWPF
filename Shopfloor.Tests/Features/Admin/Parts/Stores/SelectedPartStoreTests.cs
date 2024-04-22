using NSubstitute;
using Shopfloor.Features.Admin.Parts.Stores;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Parts.Stores
{
    public class SelectedPartStoreTests
    {


        public SelectedPartStoreTests()
        {

        }

        private SelectedPartStore CreateSelectedPartStore()
        {
            return new SelectedPartStore();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var selectedPartStore = this.CreateSelectedPartStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
