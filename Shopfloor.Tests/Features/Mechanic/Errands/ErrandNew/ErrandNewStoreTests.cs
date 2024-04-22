using NSubstitute;
using Shopfloor.Features.Mechanic.Errands.ErrandNew;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Mechanic.Errands.ErrandNew
{
    public class ErrandNewStoreTests
    {


        public ErrandNewStoreTests()
        {

        }

        private ErrandNewStore CreateErrandNewStore()
        {
            return new ErrandNewStore();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var errandNewStore = this.CreateErrandNewStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
