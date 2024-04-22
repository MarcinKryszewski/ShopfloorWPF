using NSubstitute;
using Shopfloor.Models.OfferModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.OfferModel.Store
{
    public class OfferStoreTests
    {
        private OfferProvider subOfferProvider;

        public OfferStoreTests()
        {
            this.subOfferProvider = Substitute.For<OfferProvider>();
        }

        private OfferStore CreateOfferStore()
        {
            return new OfferStore(
                this.subOfferProvider);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var offerStore = this.CreateOfferStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
