using FluentAssertions;
using Shopfloor.Database.SQLite;
using Shopfloor.Utilities;

namespace Shopfloor.Tests.Database.Initializers
{
    public class SqliteInitCommandsTests
    {
        [Fact]
        public void InitCommands_ShouldContainAllSQLCommands()
        {
            // Arrange
            SqliteInitCommands sqlCommands = new();
            int amountOfConstants = ConstantVariablesCounter.CountConstantVariablesOfClass<SqliteInitCommands>();
            // Act
            bool result = sqlCommands.InitCommands.Count == amountOfConstants;
            // Assert
            result.Should().BeTrue();
        }
    }
}