using NSubstitute;
using Shopfloor.Features.Mechanic.Requests;
using System;
using Xunit;

namespace Shopfloor.Tests.Features.Mechanic.Requests.RequestsEdit
{
    public class RequestsEditViewModelTests
    {


        public RequestsEditViewModelTests()
        {

        }

        private RequestsEditViewModel CreateViewModel()
        {
            return new RequestsEditViewModel();
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
