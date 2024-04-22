using NSubstitute;
using Shopfloor.Models.SupplierModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.SupplierModel
{
    public class SupplierTests
    {


        public SupplierTests()
        {

        }

        private Supplier CreateSupplier()
        {
            return new Supplier(
                TODO,
                TODO,
                TODO);
        }

        [Fact]
        public void Equals_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var supplier = this.CreateSupplier();
            Supplier? other = null;

            // Act
            var result = supplier.Equals(
                other);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void Equals_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var supplier = this.CreateSupplier();
            object? obj = null;

            // Act
            var result = supplier.Equals(
                obj);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void GetHashCode_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var supplier = this.CreateSupplier();

            // Act
            var result = supplier.GetHashCode();

            // Assert
            Assert.True(false);
        }
    }
}
