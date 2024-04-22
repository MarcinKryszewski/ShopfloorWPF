using NSubstitute;
using Shopfloor.Models.ErrandPartOfferModel;
using System;
using Xunit;

namespace Shopfloor.Tests.Models.ErrandPartOfferModel.Store
{
    public class ErrandPartOfferStoreTests
    {
        private ErrandPartOfferProvider subErrandPartOfferProvider;

        public ErrandPartOfferStoreTests()
        {
            this.subErrandPartOfferProvider = Substitute.For<ErrandPartOfferProvider>();
        }

        private ErrandPartOfferStore CreateErrandPartOfferStore()
        {
            return new ErrandPartOfferStore(
                this.subErrandPartOfferProvider);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var errandPartOfferStore = this.CreateErrandPartOfferStore();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
