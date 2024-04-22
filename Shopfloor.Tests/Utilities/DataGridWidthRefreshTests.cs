using Shopfloor.Utilities;
using System;
using Xunit;

namespace Shopfloor.Tests.Utilities
{
    public class DataGridWidthRefreshTests
    {
        [Fact]
        public void RefreshWidth_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var dataGridWidthRefresh = new DataGridWidthRefresh();
            object sender = null;
            DataTransferEventArgs e = null;
            int starColumn = 0;

            // Act
            dataGridWidthRefresh.RefreshWidth(
                sender,
                e,
                starColumn);

            // Assert
            Assert.True(false);
        }
    }
}
