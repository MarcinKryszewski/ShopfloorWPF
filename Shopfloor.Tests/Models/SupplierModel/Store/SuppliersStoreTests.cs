using NSubstitute;
using Shopfloor.Models.SupplierModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.SupplierModel.Store
{
    public class SuppliersStoreTests
    {
        private SupplierProvider subSupplierProvider;

        public SuppliersStoreTests()
        {
            this.subSupplierProvider = Substitute.For<SupplierProvider>();
        }

        private SuppliersStore CreateSuppliersStore()
        {
            return new SuppliersStore(
                this.subSupplierProvider);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var suppliersStore = this.CreateSuppliersStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
