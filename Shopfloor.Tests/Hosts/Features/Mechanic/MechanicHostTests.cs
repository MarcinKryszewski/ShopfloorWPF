using NSubstitute;
using Shopfloor.Hosts.Features.Mechanic;
using System;
using Xunit;

namespace Shopfloor.Tests.Hosts.Features.Mechanic
{
    public class MechanicHostTests
    {


        public MechanicHostTests()
        {

        }

        private MechanicHost CreateMechanicHost()
        {
            return new MechanicHost();
        }

        [Fact]
        public void Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var mechanicHost = this.CreateMechanicHost();
            IServiceCollection services = null;

            // Act
            mechanicHost.Get(
                services);

            // Assert
            Assert.True(false);
        }
    }
}
