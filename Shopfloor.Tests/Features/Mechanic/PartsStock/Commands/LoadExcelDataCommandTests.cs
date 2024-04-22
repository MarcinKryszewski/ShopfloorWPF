using NSubstitute;
using Shopfloor.Features.Mechanic.PartsStock;
using Shopfloor.Features.Plannist.PlannistDashboard.Commands;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopfloor.Tests.Features.Mechanic.PartsStock.Commands
{
    public class LoadExcelDataCommandTests
    {
        private PartsStockListViewModel subPartsStockListViewModel;

        public LoadExcelDataCommandTests()
        {
            this.subPartsStockListViewModel = Substitute.For<PartsStockListViewModel>();
        }

        private LoadExcelDataCommand CreateLoadExcelDataCommand()
        {
            return new LoadExcelDataCommand(
                this.subPartsStockListViewModel);
        }

        [Fact]
        public async Task ExecuteAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var loadExcelDataCommand = this.CreateLoadExcelDataCommand();
            object? parameter = null;

            // Act
            await loadExcelDataCommand.ExecuteAsync(
                parameter);

            // Assert
            Assert.True(false);
        }
    }
}
