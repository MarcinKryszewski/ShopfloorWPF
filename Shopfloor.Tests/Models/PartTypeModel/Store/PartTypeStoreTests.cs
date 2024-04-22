using NSubstitute;
using Shopfloor.Models.PartTypeModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.PartTypeModel.Store
{
    public class PartTypeStoreTests
    {
        private PartTypeProvider subPartTypeProvider;

        public PartTypeStoreTests()
        {
            this.subPartTypeProvider = Substitute.For<PartTypeProvider>();
        }

        private PartTypeStore CreatePartTypeStore()
        {
            return new PartTypeStore(
                this.subPartTypeProvider);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var partTypeStore = this.CreatePartTypeStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
