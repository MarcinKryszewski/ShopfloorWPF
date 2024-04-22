using NSubstitute;
using Shopfloor.Hosts.Features.Admin;
using System;
using Xunit;

namespace Shopfloor.Tests.Hosts.Features.Admin
{
    public class AdminHostTests
    {


        public AdminHostTests()
        {

        }

        private AdminHost CreateAdminHost()
        {
            return new AdminHost();
        }

        [Fact]
        public void Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var adminHost = this.CreateAdminHost();
            IServiceCollection services = null;

            // Act
            adminHost.Get(
                services);

            // Assert
            Assert.True(false);
        }
    }
}
