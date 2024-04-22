using NSubstitute;
using Shopfloor.Features.Admin.Suppliers;
using Shopfloor.Features.Admin.Suppliers.Commands;
using Shopfloor.Models.SupplierModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Admin.Suppliers.Commands
{
    public class SupplierEditCommandTests
    {
        private SuppliersListViewModel subSuppliersListViewModel;
        private SupplierProvider subSupplierProvider;

        public SupplierEditCommandTests()
        {
            this.subSuppliersListViewModel = Substitute.For<SuppliersListViewModel>();
            this.subSupplierProvider = Substitute.For<SupplierProvider>();
        }

        private SupplierEditCommand CreateSupplierEditCommand()
        {
            return new SupplierEditCommand(
                this.subSuppliersListViewModel,
                this.subSupplierProvider);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var supplierEditCommand = this.CreateSupplierEditCommand();
            object? parameter = null;

            // Act
            supplierEditCommand.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
