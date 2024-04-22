using NSubstitute;
using Shopfloor.Features.Admin.Suppliers;
using Shopfloor.Features.Admin.Suppliers.Commands;
using Shopfloor.Models.SupplierModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Suppliers.Commands
{
    public class SupplierAddCommandTests
    {
        private SuppliersListViewModel subSuppliersListViewModel;
        private SupplierProvider subSupplierProvider;

        public SupplierAddCommandTests()
        {
            this.subSuppliersListViewModel = Substitute.For<SuppliersListViewModel>();
            this.subSupplierProvider = Substitute.For<SupplierProvider>();
        }

        private SupplierAddCommand CreateSupplierAddCommand()
        {
            return new SupplierAddCommand(
                this.subSuppliersListViewModel,
                this.subSupplierProvider);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var supplierAddCommand = this.CreateSupplierAddCommand();
            object? parameter = null;

            // Act
            supplierAddCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
