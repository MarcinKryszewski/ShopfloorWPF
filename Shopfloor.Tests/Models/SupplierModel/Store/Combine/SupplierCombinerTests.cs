using NSubstitute;
using Shopfloor.Models.SupplierModel.Store.Combine;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Models.SupplierModel.Store.Combine
{
    public class SupplierCombinerTests
    {


        public SupplierCombinerTests()
        {

        }

        private SupplierCombiner CreateSupplierCombiner()
        {
            return new SupplierCombiner();
        }

        [Fact]
        public async Task Combine_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var supplierCombiner = this.CreateSupplierCombiner();
            bool shouldForce = false;

            // Act
            await supplierCombiner.Combine(
                shouldForce);

            // Assert
            Assert.True(false);
        }
    }
}
