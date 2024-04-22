using NSubstitute;
using Shopfloor.Features.Mechanic.Errands;
using Shopfloor.Features.Mechanic.Errands.Commands;
using Shopfloor.Features.Mechanic.Errands.Interfaces;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Mechanic.Errands.Commands
{
    public class ErrandsShowPartsListTests
    {
        private IPartsList subPartsList;
        private ErrandPartsListViewModel subErrandPartsListViewModel;

        public ErrandsShowPartsListTests()
        {
            this.subPartsList = Substitute.For<IPartsList>();
            this.subErrandPartsListViewModel = Substitute.For<ErrandPartsListViewModel>();
        }

        private ErrandsShowPartsList CreateErrandsShowPartsList()
        {
            return new ErrandsShowPartsList(
                this.subPartsList,
                this.subErrandPartsListViewModel);
        }

        [Fact]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var errandsShowPartsList = this.CreateErrandsShowPartsList();
            object? parameter = null;

            // Act
            errandsShowPartsList.Execute(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
