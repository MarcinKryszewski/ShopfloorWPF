using NSubstitute;
using Shopfloor.Features.Mechanic.PartsStock;
using Shopfloor.Models.PartModel;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Features.Mechanic.PartsStock.PartsStockList
{
    public class PartsStockListViewModelTests
    {
        private PartProvider subPartProvider;

        public PartsStockListViewModelTests()
        {
            this.subPartProvider = Substitute.For<PartProvider>();
        }

        private PartsStockListViewModel CreateViewModel()
        {
            return new PartsStockListViewModel(
                this.subPartProvider);
        }

        [Fact]
        public async Task LoadData_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var viewModel = this.CreateViewModel();
            List parts = null;

            // Act
            await viewModel.LoadData(
                parts);

            // Assert
            Assert.True(false);
        }
    }
}
