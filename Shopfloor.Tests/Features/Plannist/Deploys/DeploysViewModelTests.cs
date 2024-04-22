using NSubstitute;
using Shopfloor.Features.Plannist;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Plannist.Deploys
{
    public class DeploysViewModelTests
    {


        public DeploysViewModelTests()
        {

        }

        private DeploysViewModel CreateViewModel()
        {
            return new DeploysViewModel();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var viewModel = this.CreateViewModel();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
