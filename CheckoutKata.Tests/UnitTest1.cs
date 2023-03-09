using NUnit.Framework;
using System.Collections.Generic;

namespace CheckoutKata.Tests
{
    [TestFixture]
    public class CheckoutTests
    {
        private Dictionary<char, decimal> _itemPrices;

        [SetUp]
        public void SetUp()
        {
            // Set up the item prices
            _itemPrices = new Dictionary<char, decimal>()
            {
                {'A', 10m},
                {'B', 15m},
                {'C', 40m},
                {'D', 55m}
            };
        }

        [Test]
        public void CheckoutItems_AddsItemToBasket()
        {
            // Arrange
            var checkout = new Checkout(_itemPrices);

            // Act
            checkout.CheckoutItems('A');
            checkout.CheckoutItems('B');
            checkout.CheckoutItems('C');
            checkout.CheckoutItems('D');

            // Assert
            Assert.That(checkout.Items, Is.EqualTo(new Dictionary<char, int>()
            {
                {'A', 1},
                {'B', 1},
                {'C', 1},
                {'D', 1}
            }));
        }

        [Test]
        public void Total_ReturnsTotalCostOfItems()
        {
            // Arrange
            var checkout = new Checkout(_itemPrices);

            // Act
            checkout.CheckoutItems('A');
            checkout.CheckoutItems('B');
            checkout.CheckoutItems('C');
            checkout.CheckoutItems('D');
            var total = checkout.Total();

            // Assert
            Assert.That(total, Is.EqualTo(120m));
        }

        [Test]
        public void CheckoutItems_AppliesPromotionWhen3LotsOfItemBAdded()
        {
            // Arrange
            var checkout = new Checkout(_itemPrices);

            // Act
            checkout.CheckoutItems('B');
            checkout.CheckoutItems('B');
            checkout.CheckoutItems('B');
            checkout.CheckoutItems('A');
            var total = checkout.Total();

            // Assert
            Assert.That(total, Is.EqualTo(50m));
        }

        [Test]
        public void CheckoutItems_AppliesPromotionWhen2LotsOfItemDAdded()
        {
            // Arrange
            var checkout = new Checkout(_itemPrices);

            // Act
            checkout.CheckoutItems('D');
            checkout.CheckoutItems('D');
            checkout.CheckoutItems('B');
            checkout.CheckoutItems('A');
            var total = checkout.Total();

            // Assert
            Assert.That(total, Is.EqualTo(-29.75m));
        }
    }
}