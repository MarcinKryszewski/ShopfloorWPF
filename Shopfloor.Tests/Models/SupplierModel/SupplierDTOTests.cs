using NSubstitute;
using Shopfloor.Models.SupplierModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.SupplierModel
{
    public class SupplierDTOTests
    {


        public SupplierDTOTests()
        {

        }

        private SupplierDTO CreateSupplierDTO()
        {
            return new SupplierDTO();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var supplierDTO = this.CreateSupplierDTO();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
